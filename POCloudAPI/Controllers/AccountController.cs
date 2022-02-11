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
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.password)),
                passwordSalt = hmac.Key
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return new UserDTO { Username = user.Username, Token = _tokenService.createToken(user)};
        }

        private async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(x => x.Username.ToLower() == username.ToLower());
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Username.ToLower() == loginDTO.username.ToLower());
            if (user == null) return BadRequest("Invalid username.");
            using var hmac = new HMACSHA512(user.passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.passwordHash[i])
                {
                    return BadRequest("Wrong password.");
                }
            }
            return new UserDTO { Username = user.Username, Token = _tokenService.createToken(user) };
        }
    }
}
