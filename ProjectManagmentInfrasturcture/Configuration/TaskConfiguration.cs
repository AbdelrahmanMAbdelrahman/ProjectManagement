using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagementDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 

namespace ProjectManagmentInfrasturcture.Configuration
{
    public class TaskConfiguration : IEntityTypeConfiguration<ProjectManagementDomain.Models.Task>
    {
        public void Configure(EntityTypeBuilder<ProjectManagementDomain.Models.Task> builder)
        {
            builder.HasKey(t=>t.Id);
            builder.Property(t=>t.Title).IsRequired().HasMaxLength(100);
            builder.HasIndex(t=>t.Title)
                .IsUnique();
            builder.Property(t=>t.Description).HasMaxLength(1000);
            builder.Property(t => t.Status).IsRequired();
            builder.Property(t=>t.DueDate).IsRequired();
            builder.Property(t=>t.Priority).IsRequired();
            builder.HasOne(t=>t.Project)
                .WithMany(p=>p.Tasks)
                .HasForeignKey(t=>t.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
             
        }
    }
     
    }
