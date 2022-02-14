using System.ComponentModel.DataAnnotations;

namespace POCloudAPI.DTO
{
    public class LoginDTO
    {
        [Required]
        public string? username { get; set; }
        [Required]
        public string? password { get; set; }
    }
}
