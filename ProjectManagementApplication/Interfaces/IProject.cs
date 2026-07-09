
using ProjectManagementApplication.DTOs.Paginated;
using ProjectManagementApplication.Pagination;

namespace ProjectManagementApplication.Interfaces
{
    public interface IProject
    {
        Task<Result<ProjectRes>> CreateProjectAsync(ProjectReq req, CancellationToken ct);
        Task<Result<PaginatedList<ProjectRes>>> GetAllProjectsAsync( PaginatedReq req ,CancellationToken ct);
        Task<Result<ProjectRes>> GetProjectByIdAsync(Guid Id, CancellationToken ct);
        Task<Result> UpdateProjectAsync(Guid ID, ProjectReq req, CancellationToken ct);
        Task<Result> DeleteProjectAsync(Guid Id, CancellationToken ct);
    }
}
