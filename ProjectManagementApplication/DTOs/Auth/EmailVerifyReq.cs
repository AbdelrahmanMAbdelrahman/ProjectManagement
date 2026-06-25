using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementApplication.DTOs.Auth
{
    public record EmailVerifyReq(string mailTo, string mailFrom, string id, string code);

}
