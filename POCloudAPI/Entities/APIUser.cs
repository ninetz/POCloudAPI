using Microsoft.AspNetCore.Identity;

namespace POCloudAPI.Entities
{
    public class APIUser
    {
        public long Id { get; set; }
        public string? Username { get; set; }
        
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastLogin { get; set; }
        public string? CurrentToken { get; set; }
        public ICollection<APIFile>? UserFiles { get; set; }

    }
}
