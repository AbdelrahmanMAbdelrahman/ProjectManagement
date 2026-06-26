using ProjectManagementApplication.Common.Results;

namespace ProjectManagementApplication.Interfaces
{
    public interface ITask
    {
        Task<Result<TaskRes>> CreateTaskAsync(TaskReq req, CancellationToken ct);
        Task<Result> UpdateTaskStatusAsync( UpdateTaskReq req, CancellationToken ct);
        Task<Result<List<TaskRes>>> GetTasksByProjectAsync(Guid projectId, CancellationToken ct);
        Task<Result> DeleteTaskAsync(Guid Id, CancellationToken ct);
        
    }
}
