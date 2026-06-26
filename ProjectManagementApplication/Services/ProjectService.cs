using ProjectManagementApplication.Common.Results;
using ProjectManagementApplication.DTOs.Project;
using ProjectManagementApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementApplication.Services
{
    public class ProjectService : IProject
    {
        public Task<Result<ProjectRes>> CreateProjectAsync(ProjectReq req, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<Result> DeleteProjectAsync(Guid Id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<Result<List<ProjectRes>>> GetAllProjectsAsync(CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<Result<ProjectRes>> GetProjectByIdAsync(Guid Id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<Result> UpdateProjectAsync(Guid ID, ProjectReq req, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
