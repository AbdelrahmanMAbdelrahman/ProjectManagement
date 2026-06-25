using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementDomain.Models.Auth
{
    [Owned]
    public class RefreshToken
    {
        public DateTime CreatedOn { get; set; }
        public DateTime? RevokedOn { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsExpire => DateTime.UtcNow > CreatedOn;
        public bool IsActive => DateTime.UtcNow < ExpirationDate && RevokedOn is null;
        public string Token { get; set; } = string.Empty;
    }
}
