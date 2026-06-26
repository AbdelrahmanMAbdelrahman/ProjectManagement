using Asp.Versioning;

/// <summary>
/// Provides endpoints for managing projects.
/// </summary>
[ApiVersion(1)]
[ApiVersion(2)]
[ApiController]
[Route("api/v{v:apiVersion}/[controller]")]
public class ProjectController(IProject projectService) : ControllerBase
{
    /// <summary>
    /// Creates a new project.
    /// </summary>
    /// <param name="req">The project information.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>The newly created project.</returns>
    /// <response code="201">Project created successfully.</response>
    /// <response code="400">Invalid project data.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="403">Admin access required.</response>
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateProject(
        [FromBody] ProjectReq req,
        CancellationToken ct)
    {
        Result<ProjectRes> result = await projectService.CreateProjectAsync(req, ct);

        return result.IsSuccess
            ? CreatedAtAction(nameof(GetProjectById),
                new { id = result.Value.id },
                result.Value)
            : result.Problem();
    }

    /// <summary>
    /// Retrieves a project by its unique identifier.
    /// </summary>
    /// <param name="id">Project identifier.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>The requested project.</returns>
    /// <response code="200">Project found.</response>
    /// <response code="404">Project not found.</response>
    [Authorize(Roles = "Admin")]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetProjectById(
        Guid id,
        CancellationToken ct)
    {
        Result<ProjectRes> result = await projectService.GetProjectByIdAsync(id, ct);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.Problem();
    }

    /// <summary>
    /// Retrieves all projects.
    /// </summary>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>A list of all projects.</returns>
    /// <response code="200">Projects retrieved successfully.</response>
    [HttpGet]
    public async Task<IActionResult> GetAllProjects(CancellationToken ct)
    {
        Result<List<ProjectRes>> result = await projectService.GetAllProjectsAsync(ct);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.Problem();
    }

    /// <summary>
    /// Updates an existing project.
    /// </summary>
    /// <param name="id">Project identifier.</param>
    /// <param name="req">Updated project information.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>No content if the project is updated successfully.</returns>
    /// <response code="204">Project updated successfully.</response>
    /// <response code="400">Invalid project data.</response>
    /// <response code="404">Project not found.</response>
    /// <response code="403">Admin access required.</response>
    [Authorize(Roles = "Admin")]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> AmendProject(
        Guid id,
        [FromBody] ProjectReq req,
        CancellationToken ct)
    {
        Result result = await projectService.UpdateProjectAsync(id, req, ct);

        return result.IsSuccess
            ? NoContent()
            : result.Problem();
    }

    /// <summary>
    /// Deletes a project.
    /// </summary>
    /// <param name="id">Project identifier.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>No content if the project is deleted successfully.</returns>
    /// <response code="204">Project deleted successfully.</response>
    /// <response code="404">Project not found.</response>
    /// <response code="403">Admin access required.</response>
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteProject(
        Guid id,
        CancellationToken ct)
    {
        Result result = await projectService.DeleteProjectAsync(id, ct);

        return result.IsSuccess
            ? NoContent()
            : result.Problem();
    }
}