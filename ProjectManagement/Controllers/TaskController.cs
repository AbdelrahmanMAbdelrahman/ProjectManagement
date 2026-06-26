

using Asp.Versioning;

namespace TaskManagement.Controllers
{
    [ApiVersion(1)]
    [ApiVersion(2)]
    [ApiController]
    [Route("api/v{v:apiVersion}/[Controller]")]
    public class TaskController (ITask taskService): ControllerBase
    {
        [Authorize(Roles = "Admin")]
        [HttpPost("")]
        public async Task<IActionResult> CreateTask([FromBody] TaskReq req, CancellationToken ct)
        {
            Result<TaskRes> result = await taskService.CreateTaskAsync(req, ct);
            return result.IsSuccess ?
                CreatedAtAction(nameof(GetTaskByProjectId), new { projectId = result.Value.projectId }, result.Value) :
                result.Problem();
        }
        [MapToApiVersion(1)]
        [HttpGet("{projectId:guid}")]
        public async Task<IActionResult> GetTaskByProjectId(Guid projectId, CancellationToken ct)
        {
            Result<List<TaskRes>> result = await taskService.GetTasksByProjectAsync(projectId, ct);
            return result.IsSuccess ?
                Ok(result.Value) :
                result.Problem();
        }
        [MapToApiVersion(2)]
        [HttpGet("{projectId:guid}")]
        public async Task<IActionResult> GetTaskByProjectIdV2(Guid projectId, CancellationToken ct)
        {
            Result<List<TaskResV2>> result = await taskService.GetTasksByProjectAsyncV2(projectId, ct);
            return result.IsSuccess ?
                Ok(result.Value) :
                result.Problem();
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateTask(Guid id, [FromBody]string newStatus, CancellationToken ct)
        {
            Result result = await taskService.UpdateTaskStatusAsync( id,newStatus, ct);
            return result.IsSuccess ?
                NoContent() :
                result.Problem();
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteProject(Guid id, CancellationToken ct)
        {
            Result result = await taskService.DeleteTaskAsync(id, ct);
            return result.IsSuccess ?
                NoContent() :
                result.Problem();
        }
    }
}
