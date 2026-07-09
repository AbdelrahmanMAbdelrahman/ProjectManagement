using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementApplication.DTOs.Paginated
{
    public record PaginatedReq(int pageNumber,int pageSize);
    
}
