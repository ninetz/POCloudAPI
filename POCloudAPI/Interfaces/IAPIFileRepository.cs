using POCloudAPI.Entities;

namespace POCloudAPI.Interfaces
{
    public interface IAPIFileRepository
    {
        Task<APIFile> GetAPIFileAsync(string filename);
        Task<bool> AddAPIFileAsync(APIFile file);
    }
}
