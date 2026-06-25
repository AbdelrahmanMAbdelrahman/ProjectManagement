using ProjectManagementApplication.Common.Results;
using ProjectManagementApplication.DTOs.Task;
using ProjectManagementDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementApplication.Interfaces
{
    public interface ITask
    {
        Task<Result<TaskRes>> CreateTaskAsync(TaskReq req, CancellationToken ct);
        Task<Result> UpdateTaskStatusAsync(int ID, TaskReq req, CancellationToken ct);
        Task<Result<TaskRes>> GetTasksByProjectAsync(int Id, CancellationToken ct);
        Task<Result> DeleteTaskAsync(int Id, CancellationToken ct);
        //Task<Result<TaskRes>> GetAsync(int Id, CancellationToken ct);
        //Task<Result> UpdateAsync(int ID, TaskReq req, CancellationToken ct);
        //Task<Result<List<TaskRes>>> GetAllAsync( CancellationToken ct);
    }
}
