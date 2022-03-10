using POCloudAPI.Data;

namespace POCloudAPI.Interfaces
{
    public interface IUnitOfWork
    {
        IAPIUserRepository APIUserRepository { get; }
        IAPIFileRepository APIFileRepository { get; }
        Task<bool> PushChanges();
        bool HasChanges();
    }
}
