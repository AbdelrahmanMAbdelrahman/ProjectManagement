using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementApplication.DTOs.Auth
{
    public record UserRoleReq(string email, string role);
}
