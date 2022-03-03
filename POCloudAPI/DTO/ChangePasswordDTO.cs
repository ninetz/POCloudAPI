using System.ComponentModel.DataAnnotations;

namespace POCloudAPI.DTO
{
    public class ChangePasswordDTO
    {

        public string? Username { get; set; }
        public string? OldPassword { get; set; }

        [StringLength(32, MinimumLength = 8)]
        public string? NewPassword { get; set; }
    }
}
