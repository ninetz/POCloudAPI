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
            if (fileDTO == null || fileDTO.FileAsBase64 == null) return BadRequest("No request body");
            if (user == null) return BadRequest("User attempting to upload file doesn't exist.");
            if (!await _UnitOfWork.APIUserRepository.VerifyUserIdentity(fileDTO.Username,fileDTO.Token)) return BadRequest("Unable to verify user identity.");;
            if (fileDTO.FileAsBase64 == "") return BadRequest(string.Empty);
            var apiFile = new APIFile
            {
                FullNameOfFile = fileDTO.FileName,
                ContentType = fileDTO.ContentType,
                FileStreamData = Encoding.Default.GetBytes(fileDTO.FileAsBase64),
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
        [HttpPost("getuserfiles")]
        public async Task<ActionResult<IEnumerable<APIFileDTOSimple>>> GetUserFiles(UserDTO userDTO)
        {
            if (string.IsNullOrEmpty(userDTO.Username)) return BadRequest("Wrong username input.");
            var user = await _UnitOfWork.APIUserRepository.getUserAsync(userDTO.Username);
            if (user == null) return BadRequest("User doesn't exist.");
            return Ok(await _UnitOfWork.APIFileRepository.GetAllUserAPIFilesAsync(user));
        }
        [HttpPost("downloadfile")]
        public async Task<ActionResult<APIDownloadDTO>> DownloadFile(APIFileDownloadDTO fileDTO)
        {
            var user = await _UnitOfWork.APIUserRepository.getUserAsync(fileDTO.Username);
            if (user == null) return BadRequest("User attempting to upload file doesn't exist.");
            if (!await _UnitOfWork.APIUserRepository.VerifyUserIdentity(fileDTO.Username, fileDTO.Token)) return BadRequest("Unable to verify user identity.");

            var apiFile = await _UnitOfWork.APIFileRepository.GetAPIFileAsync(fileDTO,user);
            if (apiFile == null) return BadRequest("File not found.");
            APIDownloadDTO APIFileDTO = new APIDownloadDTO
            {
                FileAsBase64 = Convert.FromBase64String(Encoding.Default.GetString(apiFile.FileStreamData)),
                FileName = apiFile.FullNameOfFile,
                ContentType = apiFile.ContentType
            };
            return APIFileDTO;
        }
    }
}
