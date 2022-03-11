using POCloudAPI.DTO;
using POCloudAPI.Entities;

namespace POCloudAPI.Interfaces
{
    public interface IAPIFileRepository
    {
        Task<APIFile> GetAPIFileAsync(APIFileDownloadDTO ApiFileDTO, APIUser user);
        Task<bool> AddAPIFileAsync(APIFile file);
        Task<IEnumerable<APIFileDTOSimple>> GetAllUserAPIFilesAsync(APIUser user);
    }
}
