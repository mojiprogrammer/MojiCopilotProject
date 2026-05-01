using System.ComponentModel.DataAnnotations;

namespace Moji.Controllers.Models
{
    public class LoginRequestDto
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }

        public string? DeviceInfo { get; set; }
        public string? UserAgent { get; set; }
    }
}
