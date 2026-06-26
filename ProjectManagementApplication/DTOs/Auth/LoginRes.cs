
namespace ProjectManagementApplication.DTOs.Auth
{
    public record LoginRes(
     string name, string email, string userName
     , string phone, string token, List<string> roles,
     string refreshToken);
}
