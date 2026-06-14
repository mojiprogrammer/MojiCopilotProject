using DanaCopilot.Domain;
using DanaCopilot.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DanaCopilot.Persistence
{
    public class DanaAppDbContext : DbContext
    {
        public DanaAppDbContext(DbContextOptions<DanaAppDbContext> options): base(options)
        {
        }

        // ================= Telegram Users =================
        public DbSet<TelegramUser> TelegramUsers { get; set; }
        public DbSet<TelegramMessageLog> TelegramMessageLogs { get; set; }
        public DbSet<TelegramNotificationQueue> TelegramNotificationQueues { get; set; }

        
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<User> Users { get; set; }

        // ================= DOCUMENTS =================
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentChunk> DocumentChunks { get; set; }

        // ================= KNOWLEDGE =================
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<KnowledgeGap> KnowledgeGaps { get; set; }

        // ================= CHAT =================
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<AnswerSource> AnswerSources { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DanaAppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TelegramUser>(entity =>
            {
               // entity.HasIndex(e => e.TelegramUserId).IsUnique();
                entity.HasIndex(e => e.AppUserId).IsUnique().HasFilter("[AppUserId] IS NOT NULL");
                entity.HasIndex(e => e.LinkCode).HasFilter("[LinkCode] IS NOT NULL");
            });

            // TelegramMessageLog configuration
            modelBuilder.Entity<TelegramMessageLog>(entity =>
            {
                entity.HasIndex(e => e.TelegramUserId);
                entity.HasIndex(e => e.Timestamp);
            });

            // TelegramNotificationQueue configuration
            modelBuilder.Entity<TelegramNotificationQueue>(entity =>
            {
                entity.HasIndex(e => new { e.IsSent, e.RetryCount });
            });
        }
    }
}
