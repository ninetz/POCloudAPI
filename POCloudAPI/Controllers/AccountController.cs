using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POCloudAPI.Data;
using POCloudAPI.DTO;
using POCloudAPI.Entities;
using POCloudAPI.JWTokens;
using System.Security.Cryptography;
using System.Text;

namespace POCloudAPI.Controllers
{
    public class AccountController : BaseAPIController
    {
        private DataContext _context;
        private readonly ITokenService _tokenService;

        public AccountController(DataContext context, ITokenService tokenService)
        {
            _context = context;
            this._tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            if (await UserExists(registerDTO.username)) return BadRequest("Username is taken.");
            using var hmac = new HMACSHA512();

            var user = new APIUser
            {
                Username = registerDTO.username,
                Password = registerDTO.password,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.password)),
                PasswordSalt = hmac.Key,
                Created = DateTime.Now,
                LastLogin = DateTime.Now
            };
            await UpdateToken(user);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return new UserDTO { Username = user.Username, Token = user.CurrentToken };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Username.ToLower() == loginDTO.username.ToLower());
            if (user == null) return BadRequest("Invalid username.");
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                {
                    return BadRequest("Wrong password.");
                }
            }
            await UpdateToken(user);
            await updateUserLoginTime(user);
            return new UserDTO { Username = user.Username, Token = user.CurrentToken };
        }
        [HttpPost("changepassword")]
        public async Task<ActionResult<UserDTO>> changepassword(ChangePasswordDTO changePasswordDTO)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Username.ToLower() == changePasswordDTO.Username.ToLower());
            if (user == null) return BadRequest("Invalid user.");
            if (changePasswordDTO.OldPassword != user.Password) { return BadRequest("Wrong password."); };
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var newUser = new APIUser
            {
                Username = changePasswordDTO.Username,
                Password = changePasswordDTO.NewPassword,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(changePasswordDTO.NewPassword)),
                PasswordSalt = hmac.Key
            };
            user.Password = newUser.Password;
            user.PasswordHash = newUser.PasswordHash;
            user.PasswordSalt = newUser.PasswordSalt;
            await UpdateToken(user);
            await _context.SaveChangesAsync();
            await updateUserLoginTime(user);
            return new UserDTO { Username = user.Username, Token = user.CurrentToken };
        }
        [HttpPost("verifyuseridentity")]
        public async Task<ActionResult<UserDTO>> VerifyUserIdentity(UserDTO udto)
        {

            var user = await _context.Users.SingleOrDefaultAsync(x => x.Username.ToLower() == udto.Username.ToLower());
            if (user == null) return Unauthorized("User " + udto.Username + " doesn't exist.");
            if (user.CurrentToken == null) return Unauthorized("User " + udto.Username + " doesn't have a token present in DB.");
            if (udto.Token == null) return Unauthorized("User " + udto.Username + " has no token present.");
            if (udto.Token == user.CurrentToken) return Ok();
            return Unauthorized("Invalid token.");
        }
        private async Task<string> UpdateToken(APIUser user)
        {
            user.CurrentToken = _tokenService.createToken(user);
            await _context.SaveChangesAsync();
            return user.CurrentToken;
        }
        private async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(x => x.Username.ToLower() == username.ToLower());
        }

        private async Task<bool> updateUserLoginTime(APIUser apiUser)
        {
            if (apiUser == null) return false;
            apiUser.LastLogin = DateTime.Now;
            await _context.SaveChangesAsync();
            return true;
        }
    }


}
