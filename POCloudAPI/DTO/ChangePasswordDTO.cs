namespace POCloudAPI.DTO
{
    public class ChangePasswordDTO
    {

        public string? Username { get; set; }
        public string? OldPassword { get; set; }

        public string? NewPassword { get; set; }
    }
}
