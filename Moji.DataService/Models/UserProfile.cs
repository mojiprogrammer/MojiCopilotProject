namespace Moji.DataService.Models
{
    public class UserProfile
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string LanguageCode { get; set; } = "en";
        public string Timezone { get; set; } = "UTC";
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
    }
}
