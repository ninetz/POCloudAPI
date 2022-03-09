using System.ComponentModel.DataAnnotations;

namespace POCloudAPI.DTO
{
    public class RegisterDTO
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        [StringLength(32, MinimumLength = 8)]
        public string? Password { get; set; }
    }
}
