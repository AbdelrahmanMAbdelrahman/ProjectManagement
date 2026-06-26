
namespace ProjectManagementDomain.Models.Auth
{
    public class AppUser:IdentityUser
    {
        public string? Name { get; set; }

        public List<RefreshToken> RefreshTokens { get; set; } = [];
    }
}
