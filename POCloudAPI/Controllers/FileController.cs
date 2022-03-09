using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POCloudAPI.Data;
using POCloudAPI.DTO;
using POCloudAPI.Entities;
using System.Text;

namespace POCloudAPI.Controllers
{
    public class FileController : BaseAPIController
    {
        private DataContext _context;


        public FileController(DataContext context)
        {
            _context = context;

        }
        [HttpPost("uploadfile")]
        public async Task<ActionResult<string>> UploadFile(APIFileDTO fileDTO)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Username.ToLower() == fileDTO.Username.ToLower());
            if (user == null) return BadRequest("User attempting to upload file doesn't exist.");
            bool ok = (await VerifyUserIdentityBool(new UserDTO {Username = fileDTO.Username, Token = fileDTO.Token }));

            var apiFile = new APIFile
            {
                FullNameOfFile = fileDTO.FileName,
                FileStreamData = Encoding.ASCII.GetBytes(fileDTO.FileAsBase64),
                created = DateTime.Now,
                LastModified = DateTime.Now,
                User = user

            };

            _context.Files.Add(apiFile);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("verifyuseridentitybool")]
        public async Task<bool> VerifyUserIdentityBool(UserDTO udto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Username.ToLower() == udto.Username.ToLower());
            if (user == null) return false;
            if (user.CurrentToken == null) return false;
            if (udto.Token == null) return false;
            if (udto.Token == user.CurrentToken) return true;
            return false;

        }
        [HttpPost("uploadfileTest")]
        public async Task<ActionResult<string>> UploadFileTest(IFormFile file)
        {
            return Ok(file);
        }


    }
}
