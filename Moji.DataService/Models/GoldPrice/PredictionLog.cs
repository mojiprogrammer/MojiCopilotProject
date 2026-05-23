using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Moji.DataService.Models
{
    [Table("PredictionLogs")]
    public class PredictionLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        [Column(TypeName = "nvarchar(20)")]
        public string AssetType { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime PredictionDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal PredictedValue { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ActualValue { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public double? ErrorPercentage { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string ModelVersion { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public virtual GoldPrice? GoldPrice { get; set; }
        public virtual CurrencyPrice? CurrencyPrice { get; set; }

        public void CalculateErrorPercentage()
        {
            if (ActualValue.HasValue && ActualValue.Value != 0)
            {
                ErrorPercentage = (double?)(Math.Abs((ActualValue.Value - PredictedValue) / ActualValue.Value) * 100);
            }
        }


        public bool IsAccurate(float thresholdPercentage = 5)
        {
            return ErrorPercentage.HasValue && ErrorPercentage.Value <= thresholdPercentage;
        }
    }
}