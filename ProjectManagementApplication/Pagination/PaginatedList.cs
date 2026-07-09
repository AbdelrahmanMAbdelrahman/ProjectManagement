using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementApplication.Pagination
{
    public class PaginatedList<T>
    {
        public List<T> Items { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
        public PaginatedList() { }
        public PaginatedList(List<T>items,int pageNumber,int pageSize,int count)
        {
            Items = items;
            PageNumber = pageNumber;
            TotalPages =Convert.ToInt32( Math.Ceiling(count/(double)pageSize));
            HasNextPage = pageNumber < TotalPages;
            HasPreviousPage= pageNumber > 1;
        }
        public static async Task<PaginatedList<T>> Create(IQueryable<T>source,int pageNumber=1,int pageSize=10)
        {
           List<T>data=await source.Skip((pageNumber-1)*pageSize).Take(pageSize).ToListAsync();
            int count = await source.CountAsync();
            return new PaginatedList<T>(data,pageNumber,pageSize,count);
        }
    }
}
