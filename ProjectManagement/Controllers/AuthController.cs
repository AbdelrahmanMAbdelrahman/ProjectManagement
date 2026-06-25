using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Common.Problem;
using ProjectManagementApplication.Common.Results;
using ProjectManagementApplication.DTOs.Auth;
using ProjectManagementApplication.Interfaces;
using System.Threading.Tasks;


namespace ProjectManagement.Controllers
{
    [ApiController]
    [Route("Api/[Controller]")]
    public class AuthController(IAuth auth) : ControllerBase
    {
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(RegisterReq req,CancellationToken ct)
        {
           Result result=await auth.SignUp(req, ct);
            return result.Success ? Ok("Check your email to confirm email") : result.Problem();
        }
        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(LoginReq req, CancellationToken ct) { 
        Result<LoginRes> result =await auth.SignIn(req, ct);
            return result.Success? Ok(result.Value) : result.Problem();
        }
        
        [HttpGet("Confirm")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] EmailConfirmReq req, CancellationToken ct) {
            Result result = await auth.ConfirmEmail(req,ct);
            return result.Success?  Ok("Thank you for confirm your email"):result.Problem();  
        }
        [Authorize(Roles ="Admin")]
        [HttpPut("AssignRole")]
        public async Task<IActionResult> AssignRoleAsync(UserRoleReq req, CancellationToken ct)
        {
            var res = await auth.AssignRoleAsync(req, ct);
            return res.Success ?
                NoContent() :
                res.Problem();
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("CreateRole")]
        public async Task<IActionResult> CreateRole (string role,CancellationToken ct)
        {
            Result<RoleRes> res = await auth.AddRole(role, ct);
            return res.Success ?
                Ok(res.Value):
                res.Problem();
        }
    }
}
