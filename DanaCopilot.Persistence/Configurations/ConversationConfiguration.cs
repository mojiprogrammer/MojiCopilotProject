using DanaCopilot.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Persistence.Configurations
{
    public class ConversationConfiguration : IEntityTypeConfiguration<Conversation>
    {
        public void Configure(EntityTypeBuilder<Conversation> b)
        {
            b.ToTable("Conversations", "core");

            b.HasKey(x => x.Id);

            b.Property(x => x.Title)
                .HasMaxLength(300);

            b.HasIndex(x => x.UserId);
        }
    }
}
