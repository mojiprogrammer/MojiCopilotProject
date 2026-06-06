using DanaCopilot.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> b)
        {
            b.ToTable("Users");

            b.HasKey(x => x.Id);

            b.Property(x => x.Username)
                .HasMaxLength(100)
                .IsRequired();

            b.Property(x => x.Email)
                .HasMaxLength(200);

            b.HasIndex(x => x.Username)
                .IsUnique();

            b.HasOne<Organization>()
                .WithMany()
                .HasForeignKey(x => x.OrganizationId);
        }
    }
}
