using System;
using System.Collections.Generic;
using System.Text;

namespace Moji.Services.Models
{
    public class ChangePasswordRequest
    {
        public string? CurrentPassword { get; set; }
        public string? NewPassword { get; set; }
    }
}
