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

        // ================= USERS & ORG =================
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
        }
    }
}
