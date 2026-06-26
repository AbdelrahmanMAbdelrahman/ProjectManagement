
namespace ProjectManagement.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProjectController(IProject projectService) :ControllerBase
    {
        [Authorize(Roles ="Admin")]
        [HttpPost("")]
        public async Task<IActionResult> CreateProject([FromBody] ProjectReq req,CancellationToken ct)
        {
            Result<ProjectRes>result=await projectService.CreateProjectAsync(req, ct);
            return result.IsSuccess?
                CreatedAtAction(nameof(GetProjectById),new{id=result.Value.id},result.Value):
                result.Problem();
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetProjectById(Guid id,CancellationToken ct)
        {
            Result<ProjectRes> result = await projectService.GetProjectByIdAsync(id, ct);
            return result.IsSuccess?
                Ok(result.Value):
                result.Problem();
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAllProjects(CancellationToken ct)
        {
            Result<List<ProjectRes>>result=await projectService.GetAllProjectsAsync(ct);
            return result.IsSuccess ?
                Ok(result.Value) :
                result.Problem();
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> AmendProject(Guid id, [FromBody] ProjectReq req,CancellationToken ct)
        {
            Result result = await projectService.UpdateProjectAsync(id,req,ct);
            return result.IsSuccess ?
                NoContent() :
                result.Problem();
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteProject(Guid id, CancellationToken ct) { 
        Result result =await projectService.DeleteProjectAsync(id, ct);
            return result.IsSuccess?
                NoContent():
                result.Problem();
        }

    }
}
