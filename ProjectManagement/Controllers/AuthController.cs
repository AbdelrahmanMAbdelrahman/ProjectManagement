namespace ProjectManagement.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AuthController(IAuth authService) : ControllerBase
    {
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(RegisterReq req,CancellationToken ct)
        {
           Result result=await authService.SignUp(req, ct);
            return result.IsSuccess ? Ok("Check your email to confirm email") : result.Problem();
        }
        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(LoginReq req, CancellationToken ct) { 
        Result<LoginRes> result =await authService.SignIn(req, ct);
            return result.IsSuccess? Ok(result.Value) : result.Problem();
        }
        
        [HttpGet("Confirm")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] EmailConfirmReq req, CancellationToken ct) {
            Result result = await authService.ConfirmEmail(req,ct);
            return result.IsSuccess?  Ok("Thank you for confirm your email"):result.Problem();  
        }
        [Authorize(Roles ="Admin")]
        [HttpPut("AssignRole")]
        public async Task<IActionResult> AssignRoleAsync(UserRoleReq req, CancellationToken ct)
        {
            var res = await authService.AssignRoleAsync(req, ct);
            return res.IsSuccess ?
                NoContent() :
                res.Problem();
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("CreateRole")]
        public async Task<IActionResult> CreateRole (string role,CancellationToken ct)
        {
            Result<RoleRes> res = await authService.AddRole(role, ct);
            return res.IsSuccess ?
                Ok(res.Value):
                res.Problem();
        }
    }
}
