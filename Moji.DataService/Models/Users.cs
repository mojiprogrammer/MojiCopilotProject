namespace Moji.DataService.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public bool IsVerified { get; set; }
        public bool IsLocked { get; set; }
        public int LoginAttemptCount { get; set; }
        public DateTime? LastLoginAttemptTime { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public DateTime? PasswordChangedAt { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
