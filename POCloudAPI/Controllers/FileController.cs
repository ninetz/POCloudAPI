using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POCloudAPI.Data;
using POCloudAPI.DTO;
using POCloudAPI.Entities;
using POCloudAPI.Interfaces;
using System.Collections;
using System.Text;

namespace POCloudAPI.Controllers
{
    public class FileController : BaseAPIController
    {
        private IUnitOfWork _UnitOfWork;


        public FileController(IUnitOfWork UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;

        }
        [HttpPost("uploadfile")]
        public async Task<ActionResult<string>> UploadFile(APIFileDTO fileDTO)
        {
            var user = await _UnitOfWork.APIUserRepository.getUserAsync(fileDTO.Username);
            if (user == null) return BadRequest("User attempting to upload file doesn't exist.");
            if (!await _UnitOfWork.APIUserRepository.VerifyUserIdentity(fileDTO.Username,fileDTO.Token)) return BadRequest("Unable to verify user identity.");

            var apiFile = new APIFile
            {
                FullNameOfFile = fileDTO.FileName,
                FileStreamData = Encoding.UTF8.GetBytes(fileDTO.FileAsBase64),
                created = DateTime.Now,
                LastModified = DateTime.Now,
                User = user

            };

            await _UnitOfWork.APIFileRepository.AddAPIFileAsync(apiFile);
            await _UnitOfWork.PushChanges();
            return Ok();
        }
        [HttpPost("uploadfileTest")]
        public async Task<ActionResult<string>> UploadFileTest(IFormFile file)
        {
            return Ok(file);
        }
        [HttpPost("downloadfile")]
        public async Task<ActionResult<FormFile>> DownloadFile(APIFileDownloadDTO fileDTO)
        {
            var user = await _UnitOfWork.APIUserRepository.getUserAsync(fileDTO.Username);
            if (user == null) return BadRequest("User attempting to upload file doesn't exist.");
            if (!await _UnitOfWork.APIUserRepository.VerifyUserIdentity(fileDTO.Username, fileDTO.Token)) return BadRequest("Unable to verify user identity.");

            var apiFile = await _UnitOfWork.APIFileRepository.GetAPIFileAsync(fileDTO.FileName);
            if (apiFile == null) return BadRequest("File not found.");
            //string fileString = Encoding.UTF8.GetString(apiFile.FileStreamData);
            //var file = Convert.FromBase64String(fileString);
            using var stream = new MemoryStream(apiFile.FileStreamData);
            var formFile = new FormFile(stream, 0, stream.Length, apiFile.FullNameOfFile, apiFile.FullNameOfFile);
            return formFile;
        }
    }
}
