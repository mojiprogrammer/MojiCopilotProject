using DanaCopilot.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Persistence.Configurations
{
    public class DocumentConfiguration
     : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            var value = builder.ToTable("core.Documents");

            builder.Property(x => x.Title)
                .HasMaxLength(500)
                .IsRequired();
        }
    }
}
