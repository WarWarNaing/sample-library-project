using DAL.Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Helper
{
  public   class RentedBookApiRequestHelper
    {
        public static async Task<RentedBook> SaveRentedBook(RentedBook book)
        {
            string url = string.Format("api/rentedbook/save");
            RentedBook result = await ApiRequest<RentedBook>.PostRequest(url, book);
            return result;
        }
        public static async Task<PagedListClient<RentedBook>> GetRentBookListWithPaging(string bookname = null, string customername = null, int pagesize = 10, int page = 1)
        {
            string url = $"api/rentedbook/list?bookname={bookname}&customername={customername}&pagesize={pagesize}&page={page}";
            var data = await ApiRequest<PagedListServer<RentedBook>>.GetRequest(url);
            var model = new PagedListClient<RentedBook>();
            var pagedList = new StaticPagedList<RentedBook>(data.Results, page, pagesize, data.TotalCount);
            model.Results = pagedList;
            model.TotalCount = data.TotalCount;
            model.TotalPages = data.TotalPages;
            return model;
        }
        public static async Task<RentedBook> GetRecordById(int ID)
        {
            string url = string.Format("api/rentedbook/getRecordById?ID={0}", ID);
            RentedBook result = await ApiRequest<RentedBook>.GetRequest(url);
            return result;
        }
    }
}
