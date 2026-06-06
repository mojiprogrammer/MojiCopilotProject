using DanaCopilot.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Persistence.Configurations
{
    public class KnowledgeGapConfiguration : IEntityTypeConfiguration<KnowledgeGap>
    {
        public void Configure(EntityTypeBuilder<KnowledgeGap> b)
        {
            b.ToTable("KnowledgeGaps");

            b.HasKey(x => x.Id);

            b.Property(x => x.Question)
                .IsRequired();

            b.Property(x => x.Status)
                .IsRequired();

            b.HasIndex(x => x.Status);
            b.HasIndex(x => x.OrganizationId);
        }
    }
}
