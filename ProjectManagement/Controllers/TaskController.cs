



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
            return result.Success ?
                CreatedAtAction(nameof(GetTaskById), new { id = result.Value.id }, result.Value) :
                result.Problem();
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetTaskById(Guid id, CancellationToken ct)
        {
            Result<TaskRes> result = await taskService.GetTasksByProjectAsync(id, ct);
            return result.Success ?
                Ok(result.Value) :
                result.Problem();
        }
        //[HttpGet("")]
        //public async Task<IActionResult> GetAllTasks(CancellationToken ct)
        //{
        //    Result<List<TaskRes>> result = await taskService.get(ct);
        //    return result.Success ?
        //        Ok(result.Value) :
        //        result.Problem();
        //}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> AmendTask(Guid id, [FromBody] UpdateTaskReq req, CancellationToken ct)
        {
            Result result = await taskService.UpdateTaskStatusAsync( req, ct);
            return result.Success ?
                NoContent() :
                result.Problem();
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteProject(Guid id, CancellationToken ct)
        {
            Result result = await taskService.DeleteTaskAsync(id, ct);
            return result.Success ?
                NoContent() :
                result.Problem();
        }
    }
}
