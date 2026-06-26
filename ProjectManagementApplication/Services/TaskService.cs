using ProjectManagementApplication.Common.Results;
using ProjectManagementApplication.DTOs.Task;
using ProjectManagementApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementApplication.Services
{
    public class TaskService : ITask
    {
        public Task<Result<TaskRes>> CreateTaskAsync(TaskReq req, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<Result> DeleteTaskAsync(Guid Id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }


        public Task<Result<TaskRes>> GetTasksByProjectAsync(Guid Id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<Result> UpdateTaskStatusAsync(UpdateTaskReq req, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
