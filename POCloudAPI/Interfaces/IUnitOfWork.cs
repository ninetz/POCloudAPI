using POCloudAPI.Data;

namespace POCloudAPI.Interfaces
{
    public interface IUnitOfWork
    {
        IAPIUserRepository APIUserRepository { get; }
        Task<bool> PushChanges();
        bool HasChanges();
    }
}
