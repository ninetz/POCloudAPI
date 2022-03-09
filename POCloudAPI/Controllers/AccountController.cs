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
            if (await UserExists(registerDTO.Username)) return BadRequest("Username is taken.");
            using var hmac = new HMACSHA512();

            var user = new APIUser
            {
                Username = registerDTO.Username,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
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
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Username.ToLower() == loginDTO.Username.ToLower());
            if (user == null) return BadRequest("Invalid username.");
            if (!CheckIfPasswordsMatch(user.PasswordSalt, user.PasswordHash, loginDTO.Password)) return BadRequest("Invalid password");
            await UpdateToken(user);
            await updateUserLoginTime(user);
            return new UserDTO { Username = user.Username, Token = user.CurrentToken };
        }
        [HttpPost("changepassword")]
        public async Task<ActionResult<UserDTO>> Changepassword(ChangePasswordDTO changePasswordDTO)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Username.ToLower() == changePasswordDTO.Username.ToLower());
            if (user == null) return BadRequest("Invalid user.");
            if (!CheckIfPasswordsMatch(user.PasswordSalt, user.PasswordHash, changePasswordDTO.OldPassword)) return BadRequest("Invalid password");
            using var hmac = new HMACSHA512(user.PasswordSalt);
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(changePasswordDTO.NewPassword));
            user.PasswordSalt = hmac.Key;
            await UpdateToken(user);
            await _context.SaveChangesAsync();
            await updateUserLoginTime(user);
            return new UserDTO { Username = user.Username, Token = user.CurrentToken };
        }
        [HttpPost("verifyuseridentity")]
        public async Task<ActionResult<string>> VerifyUserIdentity(UserDTO udto)
        {

            var user = await _context.Users.SingleOrDefaultAsync(x => x.Username.ToLower() == udto.Username.ToLower());
            if (udto.Token == user.CurrentToken) return Ok();
            if (user == null) return BadRequest("User " + udto.Username + " doesn't exist.");
            if (user.CurrentToken == null) return BadRequest("User " + udto.Username + " doesn't have a token present in DB.");
            if (udto.Token == null) return BadRequest("User " + udto.Username + " has no token present.");

            return BadRequest("Invalid token.");
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
        private bool CheckIfPasswordsMatch(byte[] userSalt,byte[] userHash,string inputRawPassword) {
            using var hmac = new HMACSHA512(userSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(inputRawPassword));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != userHash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }


}
