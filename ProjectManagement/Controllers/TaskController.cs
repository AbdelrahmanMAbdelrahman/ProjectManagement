



using ProjectManagementApplication.DTOs.Task;
using ProjectManagementApplication.Services;

namespace TaskManagement.Controllers
{
    [ApiController]
    [Route("Api/[Controller]")]
    public class TaskController (ITask taskService): ControllerBase
    {
        [HttpPost("")]
        public async Task<IActionResult> CreateTask([FromBody] TaskReq req, CancellationToken ct)
        {
            Result<TaskRes> result = await taskService.CreateTaskAsync(req, ct);
            return result.IsSuccess ?
                CreatedAtAction(nameof(GetTaskByProjectId), new { projectId = result.Value.projectId }, result.Value) :
                result.Problem();
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetTaskByProjectId(Guid projectId, CancellationToken ct)
        {
            Result<List<TaskRes>> result = await taskService.GetTasksByProjectAsync(projectId, ct);
            return result.IsSuccess ?
                Ok(result.Value) :
                result.Problem();
        }
        
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> AmendTask(Guid id, [FromBody] UpdateTaskReq req, CancellationToken ct)
        {
            Result result = await taskService.UpdateTaskStatusAsync( req, ct);
            return result.IsSuccess ?
                NoContent() :
                result.Problem();
        }
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
