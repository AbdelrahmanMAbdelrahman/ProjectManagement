

using Asp.Versioning;
/// <summary>
/// Provides endpoints for managing project tasks.
/// </summary>
namespace TaskManagement.Controllers
{
    [ApiVersion(1)]
    [ApiVersion(2)]
    [ApiController]
    [Route("api/v{v:apiVersion}/[Controller]")]
    public class TaskController (ITask taskService): ControllerBase
    {
        /// <summary>
        /// Creates a new task for a project.
        /// </summary>
        /// <param name="req">The task information.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>The newly created task.</returns>
        /// <response code="201">Task created successfully.</response>
        /// <response code="400">Invalid task data.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">Admin access required.</response>
        [Authorize(Roles = "Admin")]
        [HttpPost("")]
        public async Task<IActionResult> CreateTask([FromBody] TaskReq req, CancellationToken ct)
        {
            Result<TaskRes> result = await taskService.CreateTaskAsync(req, ct);
            return result.IsSuccess ?
                CreatedAtAction(nameof(GetTaskByProjectId), new { projectId = result.Value.projectId }, result.Value) :
                result.Problem();
        }
        /// <summary>
        /// Retrieves all tasks for a specific project.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>A list of tasks belonging to the specified project.</returns>
        /// <response code="200">Tasks retrieved successfully.</response>
        /// <response code="404">Project not found.</response>
        [MapToApiVersion(1)]
        [HttpGet("{projectId:guid}")]
        public async Task<IActionResult> GetTaskByProjectId(Guid projectId, CancellationToken ct)
        {
            Result<List<TaskRes>> result = await taskService.GetTasksByProjectAsync(projectId, ct);
            return result.IsSuccess ?
                Ok(result.Value) :
                result.Problem();
        }
        /// <summary>
        /// Retrieves all tasks for a specific project (V2).
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>A list of tasks belonging to the specified project.</returns>
        /// <response code="200">Tasks retrieved successfully.</response>
        /// <response code="404">Project not found.</response>
        [MapToApiVersion(2)]
        [HttpGet("{projectId:guid}")]
        public async Task<IActionResult> GetTaskByProjectIdV2(Guid projectId, CancellationToken ct)
        {
            Result<List<TaskResV2>> result = await taskService.GetTasksByProjectAsyncV2(projectId, ct);
            return result.IsSuccess ?
                Ok(result.Value) :
                result.Problem();
        }
        /// <summary>
        /// Updates the status of an existing task.
        /// </summary>
        /// <param name="id">Task identifier.</param>
        /// <param name="newStatus">The new task status.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>No content if the task status is updated successfully.</returns>
        /// <response code="204">Task updated successfully.</response>
        /// <response code="400">Invalid status.</response>
        /// <response code="404">Task not found.</response>
        /// <response code="403">Admin access required.</response>
        [Authorize(Roles = "Admin")]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateTask(Guid id, [FromBody]string newStatus, CancellationToken ct)
        {
            Result result = await taskService.UpdateTaskStatusAsync( id,newStatus, ct);
            return result.IsSuccess ?
                NoContent() :
                result.Problem();
        }
        /// <summary>
        /// Deletes a task.
        /// </summary>
        /// <param name="id">Task identifier.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>No content if the task is deleted successfully.</returns>
        /// <response code="204">Task deleted successfully.</response>
        /// <response code="404">Task not found.</response>
        /// <response code="403">Admin access required.</response>
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
