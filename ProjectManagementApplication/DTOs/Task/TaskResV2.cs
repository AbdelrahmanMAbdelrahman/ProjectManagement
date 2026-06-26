using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementApplication.DTOs.Task
{
    public record TaskResV2(Guid id, string title, string description, string status, DateTime dueDate, int priority, string Project);

}
