using System;
using System.Collections.Generic;
using System.Text;

namespace Moji.DataService.Models
{
    public class UserVerificationToken
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string TokenHash { get; set; } = string.Empty;
        public string TokenType { get; set; } = string.Empty;
        public DateTime ExpiresTime { get; set; }
        public bool IsUsed { get; set; }
        public DateTime? UsedTime { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
