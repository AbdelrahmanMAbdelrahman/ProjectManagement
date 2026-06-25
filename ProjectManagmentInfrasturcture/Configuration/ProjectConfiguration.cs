using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagementDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagmentInfrasturcture.Configuration
{
    internal class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.HasIndex(p=>p.Name)
                .IsUnique();
            builder.Property(p => p.Description).HasMaxLength(1000);
            builder.Property(p=>p.CreatedAt).IsRequired();
            builder.HasMany(p => p.Tasks)
                .WithOne(p => p.Project)
                .HasForeignKey(p => p.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
