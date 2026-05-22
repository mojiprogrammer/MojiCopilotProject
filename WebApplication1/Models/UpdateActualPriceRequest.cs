namespace Moji.Controllers.Models
{
    public class UpdateActualPriceRequest
    {
        public string AssetType { get; set; } = string.Empty;
        public DateTime PredictionDate { get; set; }
        public float ActualValue { get; set; }
    }
}
