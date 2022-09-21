using DAL.Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Helper
{
   public  class BookApiRequestHelper
    {
        public static async Task<Book> GetBookById(int ID)
        {
            string url = string.Format("api/book/getBookById?ID={0}", ID);
            Book result = await ApiRequest<Book>.GetRequest(url);
            return result;
        }

        public static async Task<Book> UpsertBook(Book book)
        {
            string url = string.Format("api/book/upsert");
            Book result = await ApiRequest<Book>.PostRequest(url, book);
            return result;
        }

        public static async Task<PagedListClient<Book>> GetBookListWithPaging(string bookname = null, int pagesize = 5, int page = 1)
        {
            string url = $"api/book/list?bookname={bookname}&pagesize={pagesize}&page={page}";
            var data = await ApiRequest<PagedListServer<Book>>.GetRequest(url);
            var model = new PagedListClient<Book>();
            var pagedList = new StaticPagedList<Book>(data.Results, page, pagesize, data.TotalCount);
            model.Results = pagedList;
            model.TotalCount = data.TotalCount;
            model.TotalPages = data.TotalPages;
            return model;
        }

        public static async Task<Book> delete(int ID)
        {
            var url = string.Format("api/book/delete?ID={0}", ID);
            Book result = await ApiRequest<Book>.GetRequest(url);
            return result;
        }

        public static async Task<List<Category>> GetCategory()
        {
            string url = string.Format("api/book/getCategory");
            return await ApiRequest<List<Category>>.GetRequest(url);
        }

        public static async Task<List<Author>> GetAuthor()
        {
            string url = string.Format("api/book/getAuthor");
            return await ApiRequest<List<Author>>.GetRequest(url);
        }
    }
}
