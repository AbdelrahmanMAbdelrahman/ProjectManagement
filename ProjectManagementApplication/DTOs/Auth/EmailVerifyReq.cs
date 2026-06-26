
namespace ProjectManagementApplication.DTOs.Auth
{
    public record EmailVerifyReq(string mailTo, string mailFrom, string id, string code);

}
