using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Moji.DataService.Models
{
    [Table("GoldPrices")]
    public class GoldPrice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal PricePerGramIRR { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal PricePerOunceUSD { get; set; }

        [Required]
        [MaxLength(100)]
        public string Source { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation property for predictions
        public virtual ICollection<PredictionLog>? Predictions { get; set; }
    }
}