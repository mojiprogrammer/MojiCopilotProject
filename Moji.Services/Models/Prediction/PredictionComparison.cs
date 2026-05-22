namespace Moji.Services.Models
{
    public class PredictionComparison
    {
        public string AssetType { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<IndividualComparison> Comparisons { get; set; } = new();
        public double AverageError { get; set; }
        public float Mape { get; set; }
    }
}
