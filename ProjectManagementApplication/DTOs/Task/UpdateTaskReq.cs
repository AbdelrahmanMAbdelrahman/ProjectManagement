using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementApplication.DTOs.Task
{
    public record UpdateTaskReq(Guid taskId,int status);
    
}
