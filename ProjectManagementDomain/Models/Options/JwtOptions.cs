using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementDomain.Models.Options
{
    public class JwtOptions
    {
        public static string Section = "JwtOptions";
        public string Issuer { get; set; } = default!;
        public string Audience { get; set; } = default!;
        public int Period { get; set; }
        public string Key { get; set; } = default!;
    }
}
