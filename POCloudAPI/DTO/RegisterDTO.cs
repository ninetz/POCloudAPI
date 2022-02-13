using System.ComponentModel.DataAnnotations;

namespace POCloudAPI.DTO
{
    public class RegisterDTO
    {
        [Required]
        public string? username { get; set; }
        [Required]
        [StringLength(32, MinimumLength = 8)]
        public string? password { get; set; }
    }
}
