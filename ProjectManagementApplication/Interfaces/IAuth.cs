namespace ProjectManagementApplication.Interfaces
{
    public interface IAuth
    {
        
        Task<Result> SignUp(RegisterReq req, CancellationToken ct);
        Task<Result> ConfirmEmail(EmailConfirmReq req,CancellationToken ct);
        Task<Result<LoginRes>> SignIn(LoginReq req, CancellationToken ct);
        Task<Result> AssignRoleAsync(UserRoleReq req, CancellationToken ct);
        Task<Result<RoleRes>> AddRole(string role, CancellationToken ct);

        //Task<Result> AssignRolePermission(UserPermReq req, CancellationToken ct);
        
    }
}
