
namespace ProjectManagementApplication.Services
{
    public class TaskService(AppDbContext database) : ITask
    {
        public async Task<Result<TaskRes>> CreateTaskAsync(TaskReq req, CancellationToken ct)
        {
            ProjectManagementDomain.Models.Task? task = await database.Tasks.AsNoTracking().FirstOrDefaultAsync(t => t.Title == req.title);
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
            List<ProjectManagementDomain.Models.Task> Tasks = await database.Tasks.AsNoTracking().Where(t => t.ProjectId == projectId).ToListAsync();
            if (Tasks.Count == 0) return Result.Fail<List<TaskRes>>(TaskError.Notfound);
            List<TaskRes> res = Tasks.Adapt<List<TaskRes>>();
            return Result.Success(res);
        }

        public async Task<Result<List<TaskResV2>>> GetTasksByProjectAsyncV2(Guid projectId, CancellationToken ct)
        {
            var Tasks = await database.Tasks.AsNoTracking()
                .Where(t => t.ProjectId == projectId).Select(t =>
                new TaskResV2(t.Id,t.Title,t.Description,t.Status,t.DueDate,t.Priority,t.Project.Name)).ToListAsync();
            if (Tasks.Count == 0) return Result.Fail<List<TaskResV2>>(TaskError.Notfound);
            //List<TaskResV2> res = Tasks.Adapt<List<TaskResV2>>();
            return Result.Success(Tasks);
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
