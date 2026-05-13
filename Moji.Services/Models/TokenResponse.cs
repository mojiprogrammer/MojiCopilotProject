namespace Moji.Services.Models
{
    public class TokenResponse
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime ExpiryTime { get; set; }
    }
}
