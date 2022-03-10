namespace POCloudAPI.DTO
{
    public class MemberDTO
    {
        public string? Username { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public DateTime created { get; set; }
        public DateTime LastLogin { get; set; }

        public ICollection<APIFileDTO>? UserFiles { get; set; }
    }
    }
