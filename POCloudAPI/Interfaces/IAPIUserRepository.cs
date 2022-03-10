using POCloudAPI.DTO;
using POCloudAPI.Entities;

namespace POCloudAPI.Interfaces
{
    public interface IAPIUserRepository
    {
        Task<APIUser> getUserAsync(string username);
        Task<IEnumerable<APIUser>> getAllUsersAsync();
        Task<string> UpdateToken(APIUser user);
        Task<bool> UserExists(string username);
        Task<bool> updateUserLoginTime(APIUser apiUser);
        Task<bool> AddUser(APIUser apiUser);
        bool CheckIfPasswordsMatch(byte[] userSalt, byte[] userHash, string inputRawPassword);
        Task<bool> VerifyUserIdentity(string username, string token);
    }
}