using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementApplication.DTOs.Auth
{
    public record LoginRes(
     string name, string email, string userName
     , string phone, string token, List<string> roles,
     string refreshToken);
}
