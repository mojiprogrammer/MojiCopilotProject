using DanaCopilot.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Persistence.Configurations
{
    public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> b)
        {
            b.ToTable("Organizations");

            b.HasKey(x => x.Id);

            b.Property(x => x.Name)
                .HasMaxLength(200)
                .IsRequired();

            b.Property(x => x.Code)
                .HasMaxLength(50)
                .IsRequired();

            b.Property(x => x.IsActive)
                .HasDefaultValue(true);
        }
    }
}
