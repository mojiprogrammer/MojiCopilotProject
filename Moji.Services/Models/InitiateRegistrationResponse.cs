using System;
using System.Collections.Generic;
using System.Text;

namespace Moji.Services.Models
{
    public class InitiateRegistrationResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Email { get; set; }
        public DateTime VerificationCodeExpiry { get; set; }
    }
}
