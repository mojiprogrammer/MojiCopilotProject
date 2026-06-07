using DanaCopilot.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Persistence.Configurations
{
    public class AnswerSourceConfiguration : IEntityTypeConfiguration<AnswerSource>
    {
        public void Configure(EntityTypeBuilder<AnswerSource> b)
        {
            b.ToTable("AnswerSources", "core");

            b.HasKey(x => x.Id);

            b.Property(x => x.SimilarityScore)
                .HasPrecision(5, 2);

            b.HasIndex(x => x.MessageId);
        }
    }
}
