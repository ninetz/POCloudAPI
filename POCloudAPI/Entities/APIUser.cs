namespace POCloudAPI.Entities
{
    public class APIUser
    {
        public long Id { get; set; }
        public string? Username { get; set; }
        
        public string? Password { get; set; }
        public byte[]? passwordHash { get; set; }
        public byte[]? passwordSalt { get; set; }

    }
}
