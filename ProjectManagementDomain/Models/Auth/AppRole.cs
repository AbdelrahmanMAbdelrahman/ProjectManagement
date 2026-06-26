
namespace ProjectManagementDomain.Models.Auth
{
    public class AppRole : IdentityRole
    {
        public bool IsDefault { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
