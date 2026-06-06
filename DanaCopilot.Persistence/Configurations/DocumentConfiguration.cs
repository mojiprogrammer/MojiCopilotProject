using DanaCopilot.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Persistence.Configurations
{
    public class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> b)
        {
            b.ToTable("Documents");

            b.HasKey(x => x.Id);

            b.Property(x => x.Title)
                .HasMaxLength(500)
                .IsRequired();

            b.Property(x => x.FilePath)
                .HasMaxLength(1000)
                .IsRequired();

            b.HasIndex(x => x.OrganizationId);
        }
    }
}
