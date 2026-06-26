
namespace ProjectManagementApplication.DTOs.Auth
{
    public record RegisterReq(
          string name, string email, string userName
         , string phone, string password
         );
}
