using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Moji.DataService.Models
{
    public class PasswordResetRequest
    {
        [Required]
        [MaxLength(100)]
        public string EmailOrUsername { get; set; }

        [Required]
        [MinLength(8)]
        public string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
    }
}
