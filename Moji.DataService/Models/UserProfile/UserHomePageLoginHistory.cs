namespace Moji.DataService.Models
{
    public class UserHomePageLoginHistory
    {

        public int Id { get; set; }
        public int UserId { get; set; }
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
        public string? LoginStatus { get; set; }
        public string? FailureReason { get; set; }
        public string? DeviceType { get; set; }
        public string? BrowserName { get; set; }
        public string? OperatingSystem { get; set; }
        public DateTime LoginTime { get; set; }
        public string? FormattedLoginTime { get; set; }

    }
}
