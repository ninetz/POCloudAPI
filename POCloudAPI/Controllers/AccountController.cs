using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POCloudAPI.Data;
using POCloudAPI.DTO;
using POCloudAPI.Entities;
using POCloudAPI.Interfaces;
using POCloudAPI.JWTokens;
using System.Security.Cryptography;
using System.Text;

namespace POCloudAPI.Controllers
{
    public class AccountController : BaseAPIController
    {
        private IUnitOfWork _UnitOfWork;

        public AccountController(IUnitOfWork UnitOfWork)
        {
            this._UnitOfWork = UnitOfWork;


        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            
            if (await _UnitOfWork.APIUserRepository.UserExists(registerDTO.Username)) return BadRequest("Username is taken.");
            using var hmac = new HMACSHA512();

            var user = new APIUser
            {
                Username = registerDTO.Username,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
                PasswordSalt = hmac.Key,
                Created = DateTime.Now,
                LastLogin = DateTime.Now
            };
            await _UnitOfWork.APIUserRepository.UpdateToken(user);
            await _UnitOfWork.APIUserRepository.AddUser(user);
            await _UnitOfWork.PushChanges();
            return new UserDTO { Username = user.Username, Token = user.CurrentToken };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var user = await _UnitOfWork.APIUserRepository.getUserAsync(loginDTO.Username);
            if (user == null) return BadRequest("Invalid username.");
            if (!_UnitOfWork.APIUserRepository.CheckIfPasswordsMatch(user.PasswordSalt, user.PasswordHash, loginDTO.Password)) return BadRequest("Invalid password");
            await _UnitOfWork.APIUserRepository.UpdateToken(user);
            await _UnitOfWork.APIUserRepository.updateUserLoginTime(user);
            return new UserDTO { Username = user.Username, Token = user.CurrentToken };
        }
        [HttpPost("changepassword")]
        public async Task<ActionResult<UserDTO>> Changepassword(ChangePasswordDTO changePasswordDTO)
        {
            var user = await _UnitOfWork.APIUserRepository.getUserAsync(changePasswordDTO.Username);
            if (user == null) return BadRequest("Invalid user.");
            if (!_UnitOfWork.APIUserRepository.CheckIfPasswordsMatch(user.PasswordSalt, user.PasswordHash, changePasswordDTO.OldPassword)) return BadRequest("Invalid password");
            using var hmac = new HMACSHA512(user.PasswordSalt);
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(changePasswordDTO.NewPassword));
            user.PasswordSalt = hmac.Key;
            await _UnitOfWork.APIUserRepository.UpdateToken(user);
            await _UnitOfWork.PushChanges();
            await _UnitOfWork.APIUserRepository.updateUserLoginTime(user);
            return new UserDTO { Username = user.Username, Token = user.CurrentToken };
        }
        [HttpPost("verifyuseridentity")]
        public async Task<ActionResult<string>> VerifyUserIdentity(UserDTO udto)
        {

            var user = await _UnitOfWork.APIUserRepository.getUserAsync(udto.Username);
            if (udto.Token == user.CurrentToken) return Ok();
            if (user == null) return BadRequest("User " + udto.Username + " doesn't exist.");
            if (user.CurrentToken == null) return BadRequest("User " + udto.Username + " doesn't have a token present in DB.");
            if (udto.Token == null) return BadRequest("User " + udto.Username + " has no token present.");

            return BadRequest("Invalid token.");
        }


       
    }


}
