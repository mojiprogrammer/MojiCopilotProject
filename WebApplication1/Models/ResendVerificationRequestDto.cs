using System.ComponentModel.DataAnnotations;

namespace Moji.Controllers.Models
{
    public class ResendVerificationRequestDto
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
    }
}
