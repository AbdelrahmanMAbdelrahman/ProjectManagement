using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementApplication.DTOs.Auth
{
    public record RoleRes(string name,string normalizedRoleName,bool isDefault,bool isDeleted);

}
