
namespace ProjectManagementApplication.Services
{
    public class TaskService(AppDbContext database) : ITask
    {
        public async Task<Result<TaskRes>> CreateTaskAsync(TaskReq req, CancellationToken ct)
        {
            ProjectManagementDomain.Models.Task? task = await database.Tasks.FirstOrDefaultAsync(t => t.Title == req.title);
            if (task is not null) return Result.Fail<TaskRes>(TaskError.AlreadyExist);
            Project? project = await database.Projects.FindAsync(req.projectId);
            if (project is null) return Result.Fail<TaskRes>(TaskError.ProjectNotFound);
            ProjectManagementDomain.Models.Task Task = req.Adapt<ProjectManagementDomain.Models.Task>();
            await database.Tasks.AddAsync(Task, cancellationToken: ct);
            if (!await Commit()) return Result.Fail<TaskRes>(TaskError.AlreadyExist);
            TaskRes res = Task.Adapt<TaskRes>();
            return Result.Success(res);
        }
        public async Task<Result> DeleteTaskAsync(Guid Id, CancellationToken ct)
 
        {
            ProjectManagementDomain.Models.Task? Task = await database.Tasks.FindAsync(Id);
            if (Task is null) return Result.Fail<TaskRes>(TaskError.Notfound);
            database.Tasks.Remove(Task);
            if (!await Commit()) return Result.Fail(TaskError.InternalServerError);
            return Result.Success();
        }
        public async Task<Result<List<TaskRes>>> GetTasksByProjectAsync(Guid projectId, CancellationToken ct)
        {
            List<ProjectManagementDomain.Models.Task> Tasks = await database.Tasks.Where(t => t.ProjectId == projectId).ToListAsync();
            if (Tasks.Count == 0) return Result.Fail<List<TaskRes>>(TaskError.Notfound);
            List<TaskRes> res = Tasks.Adapt<List<TaskRes>>();
            return Result.Success(res);
        }
        public async Task<Result> UpdateTaskStatusAsync(Guid id,string newStatus, CancellationToken ct)
 
        {
            ProjectManagementDomain.Models.Task? task = await database.Tasks.FindAsync(id);
            if (task is null) return Result.Fail(TaskError.Notfound);
            task.Status = newStatus;
            if (!await Commit()) return Result.Fail(TaskError.InternalServerError);
            return Result.Success();
        }
        private async Task<bool> Commit()
        {
            return await database.SaveChangesAsync() > 0;
        }


    }
}
