using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PIBNAAPI.Command.Model
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
        public int TotalRecord { get; set; }
        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            TotalRecord = count;
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items);
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            int count = await source.CountAsync();
            int recordStart = ((pageIndex - 1) * pageSize) ;

            var items = await source.ToListAsync();
            items = items.Skip(recordStart).Take(pageSize).ToList();

            //var items = source.Skip(recordStart).Take(pageSize).ToList();
            
                //.ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}
