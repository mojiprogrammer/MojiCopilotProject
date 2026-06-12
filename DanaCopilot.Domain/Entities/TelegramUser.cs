using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DanaCopilot.Domain.Entities
{
    public class TelegramUser
    {
        [Key]
        public int Id { get; set; }

        // Telegram specific fields
        public long TelegramUserId { get; set; }
        public string? ChatId { get; set; }
        public string? Username { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        // Link to your existing user
        public string? AppUserId { get; set; }  // Link to your existing User model
        public string? LinkCode { get; set; }
        public DateTime? LinkCodeExpiry { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastInteraction { get; set; }

        // Navigation property if you have User model
        [ForeignKey("AppUserId")]
        public virtual User? AppUser { get; set; }
    }
}
