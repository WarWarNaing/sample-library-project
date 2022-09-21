using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class PagedListClient<T>
    {
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public int PageIndex { get; set; }
        public string prevLink { get; set; }
        public string nextLink { get; set; }
        public IPagedList<T> Results { get; set; }
        public PagedListClient() { }
        public PagedListClient(IEnumerable<T> objs, int page, int pagesize, int totalcount)
        {
            Results = new StaticPagedList<T>(objs, page, pagesize, totalcount);
            TotalCount = totalcount;
            TotalPages = (int)Math.Ceiling((double)TotalCount / pagesize);
        }
        public int min { get; set; }
        public int max { get; set; }
    }

    public class PagedListServer<T>
    {
        public int Pagesize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages
        {
            get
            {
                if (Pagesize != 0)
                {
                    return (int)Math.Ceiling((double)TotalCount / Pagesize);
                }
                return 0;
            }
        }
        public string prevLink { get; set; }
        public string nextLink { get; set; }
        public IEnumerable<T> Results { get; set; }
        public PagedListServer(IEnumerable<T> results, int totalcount, int pagesize = 0)
        {
            TotalCount = totalcount;
            Pagesize = pagesize;
            Results = results;
        }
        public int min { get; set; }
        public int max { get; set; }
    }
}
