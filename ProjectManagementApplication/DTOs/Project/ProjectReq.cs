using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementApplication.DTOs.Project
{
    public record ProjectReq(string name, string description, DateTime createdAt);
    
}
