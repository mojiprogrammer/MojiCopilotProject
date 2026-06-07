using DanaCopilot.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Persistence.Configurations
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> b)
        {
            b.ToTable("Messages", "core");

            b.HasKey(x => x.Id);

            b.Property(x => x.Content)
                .IsRequired();

            b.Property(x => x.ConfidenceScore)
                .HasPrecision(5, 2);

            b.HasIndex(x => x.ConversationId);
        }
    }
}
