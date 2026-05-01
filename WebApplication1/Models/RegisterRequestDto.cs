using System.ComponentModel.DataAnnotations;

namespace Moji.Controllers.Models
{
    public class RegisterRequestDto
    {
        [Required]
        [EmailAddress]
        [MaxLength(256)]
        public required string Email { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public required string Username { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(128)]
        public required string Password { get; set; }

        [Required]
        [MaxLength(100)]
        public required string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public required string LastName { get; set; }

        [Phone]
        [MaxLength(20)]
        public string? Phone { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [MaxLength(10)]
        public string LanguageCode { get; set; } = "en";

        [MaxLength(50)]
        public string Timezone { get; set; } = "UTC";
        public string? UserAgent { get; set; }
    }
}
