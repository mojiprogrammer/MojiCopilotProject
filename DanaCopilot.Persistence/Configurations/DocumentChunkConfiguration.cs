using DanaCopilot.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Persistence.Configurations
{
    public class DocumentChunkConfiguration : IEntityTypeConfiguration<DocumentChunk>
    {
        public void Configure(EntityTypeBuilder<DocumentChunk> b)
        {
            b.ToTable("DocumentChunks");

            b.HasKey(x => x.Id);

            b.Property(x => x.Content)
                .IsRequired();

            b.Property(x => x.ContentHash)
                .HasMaxLength(64);

            b.HasIndex(x => x.DocumentId);
        }
    }
}
