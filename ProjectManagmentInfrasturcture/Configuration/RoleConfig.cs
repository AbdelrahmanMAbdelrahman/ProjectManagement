using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagementDomain.Models.Auth;
using ProjectManagmentInfrasturcture.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagmentInfrasturcture.Configuration
{
    public class RoleConfig : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder.HasData(new AppRole
            {
                Id = DefaultRoles.Id,
                Name = DefaultRoles.Name,
                NormalizedName = DefaultRoles.Name.ToUpper(),
                ConcurrencyStamp = DefaultRoles.ConcurrencyStamp,
                IsDeleted = DefaultRoles.IsDeleted,
                
            }
                );
        }
    }
}
