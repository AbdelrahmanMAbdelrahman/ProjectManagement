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

        public Task<Result> DeleteTaskAsync(int Id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<Result<TaskRes>> GetTasksByProjectAsync(int Id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<Result> UpdateTaskStatusAsync(int ID, TaskReq req, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
