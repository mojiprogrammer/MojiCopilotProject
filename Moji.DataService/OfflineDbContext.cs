using Microsoft.EntityFrameworkCore;
using Moji.DataService.Models;

namespace Moji.DataService
{
    public class OfflineDbContext: DbContext
    {
        public OfflineDbContext(DbContextOptions<OfflineDbContext> options)
           : base(options) { }

        public DbSet<GoldPrice> GoldPrices { get; set; }
        public DbSet<CurrencyPrice> CurrencyPrices { get; set; }
        public DbSet<PredictionLog> PredictionLogs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // GoldPrices table
            modelBuilder.Entity<GoldPrice>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Date).IsRequired();
                entity.HasIndex(e => e.Date).IsUnique();
            });

            // CurrencyPrices table
            modelBuilder.Entity<CurrencyPrice>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Date).IsRequired();
                entity.Property(e => e.CurrencyCode).HasMaxLength(10);
                entity.HasIndex(e => new { e.Date, e.CurrencyCode }).IsUnique();
            });

            // PredictionLogs table
            modelBuilder.Entity<PredictionLog>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.AssetType).HasMaxLength(20);
                entity.HasIndex(e => e.PredictionDate);
            });
        }
    }
}

