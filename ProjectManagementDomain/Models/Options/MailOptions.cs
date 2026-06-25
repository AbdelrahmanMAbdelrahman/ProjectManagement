using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementDomain.Models.Options
{
    public class MailOptions
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public int Port { get; set; }
        public string Host { get; set; }
    }
}
