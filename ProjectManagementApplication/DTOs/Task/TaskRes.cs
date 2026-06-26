using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementApplication.DTOs.Task
{
    public  record TaskRes(Guid id  ,string title,string description,string status ,DateTime dueDate ,int priority ,Guid projectId );
     
}
