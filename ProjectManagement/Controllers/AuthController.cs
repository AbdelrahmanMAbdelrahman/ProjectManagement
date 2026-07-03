using Asp.Versioning;

/// <summary>
/// Provides authentication and authorization endpoints such as registration,
/// login, email confirmation, and role management.
/// </summary>

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AuthController(IAuth authService) : ControllerBase
{
    /// <summary>
    /// Registers a new user account.
    /// </summary>
    /// <param name="req">The registration information.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>
    /// Returns a success message asking the user to confirm their email.
    /// </returns>
    /// <response code="200">Registration completed successfully.</response>
    /// <response code="400">Invalid registration data.</response>
    [ProducesResponseType(typeof(IActionResult),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(IActionResult),StatusCodes.Status409Conflict)]
    [HttpPost("SignUp")]
    public async Task<IActionResult> SignUp(RegisterReq req, CancellationToken ct)
    {
        Result result = await authService.SignUp(req, ct);
        return result.IsSuccess
            ? Ok("Check your email to confirm email")
            : result.Problem();
    }

    /// <summary>
    /// Authenticates a user and returns an access token.
    /// </summary>
    /// <param name="req">User login credentials.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>User information and JWT token.</returns>
    /// <response code="200">Login successful.</response>
    /// <response code="401">Invalid email or password.</response>
    [HttpPost("SignIn")]
    public async Task<IActionResult> SignIn(LoginReq req, CancellationToken ct)
    {
        Result<LoginRes> result = await authService.SignIn(req, ct);
        return result.IsSuccess
            ? Ok(result.Value)
            : result.Problem();
    }

    /// <summary>
    /// Confirms the user's email address.
    /// </summary>
    /// <param name="req">Email confirmation token and email.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>Confirmation result.</returns>
    /// <response code="200">Email confirmed successfully.</response>
    /// <response code="400">Invalid or expired confirmation token.</response>
    [HttpGet("Confirm")]
    public async Task<IActionResult> ConfirmEmail(
        [FromQuery] EmailConfirmReq req,
        CancellationToken ct)
    {
        Result result = await authService.ConfirmEmail(req, ct);

        return result.IsSuccess
            ? Ok("Thank you for confirming your email.")
            : result.Problem();
    }

    /// <summary>
    /// Assigns a role to an existing user.
    /// </summary>
    /// <param name="req">User role assignment request.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>No content if the role is assigned successfully.</returns>
    /// <response code="204">Role assigned successfully.</response>
    /// <response code="400">Invalid request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="403">Admin access required.</response>
    //[Authorize(Roles = "Admin")]
    [HttpPut("AssignRole")]
    public async Task<IActionResult> AssignRoleAsync(UserRoleReq req, CancellationToken ct)
    {
        var res = await authService.AssignRoleAsync(req, ct);

        return res.IsSuccess
            ? NoContent()
            : res.Problem();
    }

    /// <summary>
    /// Creates a new application role.
    /// </summary>
    /// <param name="role">The role name.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>The created role.</returns>
    /// <response code="200">Role created successfully.</response>
    /// <response code="400">Role already exists or is invalid.</response>
    /// <response code="403">Admin access required.</response>
    //[Authorize(Roles = "Admin")]
    [HttpGet("CreateRole")]
    public async Task<IActionResult> CreateRole(string role, CancellationToken ct)
    {
        Result<RoleRes> res = await authService.AddRole(role, ct);

        return res.IsSuccess
            ? Ok(res.Value)
            : res.Problem();
    }
}