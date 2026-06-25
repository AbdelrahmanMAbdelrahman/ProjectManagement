using ProjectManagementApplication.Common.Results;
using ProjectManagementApplication.DTOs.Project;
using ProjectManagementDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementApplication.Interfaces
{
    public interface IProject
    {
        Task<Result<ProjectRes>> CreateProjectAsync(ProjectReq req, CancellationToken ct);
        Task<Result<List<ProjectRes>>> GetAllProjectsAsync(  CancellationToken ct);
        Task<Result<ProjectRes>> GetProjectByIdAsync(int Id, CancellationToken ct);
        Task<Result> UpdateProjectAsync(int ID, ProjectReq req, CancellationToken ct);
        Task<Result> DeleteProjectAsync(int Id, CancellationToken ct);
    }
}
