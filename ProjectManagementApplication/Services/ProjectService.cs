
using ProjectManagementApplication.DTOs.Paginated;
using ProjectManagementApplication.Pagination;

namespace ProjectManagementApplication.Services
{
    public class ProjectService (AppDbContext database): IProject
    {
        public async Task<Result<ProjectRes>> CreateProjectAsync(ProjectReq req, CancellationToken ct)
        {
            Project? project = await database.Projects.AsNoTracking().FirstOrDefaultAsync(p => p.Name == req.name);
            if (project is not null) return Result.Fail<ProjectRes>(ProjectError.AlreadyExist);
            project = req.Adapt<Project>();
            await database.Projects.AddAsync(project, cancellationToken: ct);
            if (!await Commit()) return Result.Fail<ProjectRes>(ProjectError.AlreadyExist);
            ProjectRes res = project.Adapt<ProjectRes>();
            return Result.Success(res);

        }

        public async Task<Result<PaginatedList<ProjectRes>>> GetAllProjectsAsync(PaginatedReq req,CancellationToken ct)

        {
            IQueryable<ProjectRes> projects =  database.Projects.AsNoTracking().ProjectToType<ProjectRes>();
            PaginatedList<ProjectRes> paginatedProjects =
                await PaginatedList<ProjectRes>.Create(projects,req.pageNumber,req.pageSize);
            if (paginatedProjects.TotalPages == 0) return Result.Fail<PaginatedList<ProjectRes>>(ProjectError.Notfound);
            return Result.Success(paginatedProjects);

        }

        public async Task<Result<ProjectRes>> GetProjectByIdAsync(Guid Id, CancellationToken ct)
        {
            Project? project = await database.Projects.FindAsync(Id);
            if (project is null) return Result.Fail<ProjectRes>(ProjectError.Notfound);
            ProjectRes res = project.Adapt<ProjectRes>();
            return Result.Success(res);
        }

        public async Task<Result> UpdateProjectAsync(Guid Id, ProjectReq req, CancellationToken ct)

        {
            Project? project = await database.Projects.FindAsync(Id);
            if (project is null) return Result.Fail<ProjectRes>(ProjectError.Notfound);
            req.Adapt(project);
            if (!await Commit()) return Result.Fail<ProjectRes>(ProjectError.BadRequest);
            return Result.Success();
        }


      

        public async Task<Result> DeleteProjectAsync(Guid Id, CancellationToken ct)

        {
            Project? project = await database.Projects.FindAsync(Id);
            if (project is null) return Result.Fail<ProjectRes>(ProjectError.Notfound);
            database.Projects.Remove(project);
            if (!await Commit()) return Result.Fail(ProjectError.InternalServerError);
            return Result.Success();
        }
        private async Task<bool> Commit()
        {
            return await database.SaveChangesAsync() > 0;
        }
    }
}
