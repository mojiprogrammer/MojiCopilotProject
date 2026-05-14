using System.ComponentModel.DataAnnotations;

namespace Moji.Controllers.Models
{
    public class VerifyEmailRequestDto
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(6)]
        public required string VerificationCode { get; set; }

        public string? DeviceInfo { get; set; }
        public string? UserAgent { get; set; }
    }
}
