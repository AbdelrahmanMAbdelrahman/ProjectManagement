
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
