using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementApplication.Common.Results
{
    public record Error(string Code, string Description, int? StatusCode=0)
    {
        public static Error None = new Error(string.Empty, string.Empty, null);
    }
}
