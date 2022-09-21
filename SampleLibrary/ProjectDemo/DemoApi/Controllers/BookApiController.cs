using DAL.Model;
using DemoApi.Repository;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using LinqKit;

namespace DemoApi.Controllers
{
    public class BookApiController : ApiController
    {
        LibraryDBContext dbContext;
        private BookRepository bookRepo = null;
        public BookApiController()
        {
            dbContext = new LibraryDBContext();
            bookRepo = new BookRepository(dbContext);
        }

        [Route("api/book/getBookById")]
        [HttpGet]
        public HttpResponseMessage getBookById(HttpRequestMessage request, int ID)
        {
            Book customer = bookRepo.GetWithoutTracking().FirstOrDefault(a => a.IsDeleted != true && a.BookId == ID);
            return request.CreateResponse<Book>(HttpStatusCode.OK, customer);
        }

        [Route("api/book/upsert")]
        [HttpPost]
        public HttpResponseMessage upsert(HttpRequestMessage request, Book book)
        {
            Book UpdateEntity = null;
            try
            {
                if (book.BookId > 0)
                {
                    UpdateEntity = bookRepo.UpdatewithObj(book);
                }
                else
                {
                    book.IsAvailable = true;
                    UpdateEntity = bookRepo.AddWithGetObj(book);
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }

            return request.CreateResponse<Book>(HttpStatusCode.OK, UpdateEntity);
        }

        [Route("api/book/getCategory")]
        [HttpGet]
        public HttpResponseMessage GetCategory(HttpRequestMessage request)
        {
            List<Category> categories = dbContext.Categories.Where(a => a.IsDeleted != true).DistinctBy(a => a.CategoryName).ToList();
            return request.CreateResponse<List<Category>>(HttpStatusCode.OK, categories);
        }

        [Route("api/book/getAuthor")]
        [HttpGet]
        public HttpResponseMessage GetAuthor(HttpRequestMessage request)
        {
            List<Author> authors = dbContext.Authors.Where(a => a.IsDeleted != true).DistinctBy(a => a.AuthorName).ToList();
            return request.CreateResponse<List<Author>>(HttpStatusCode.OK, authors);
        }

        [Route("api/book/list")]
        [HttpGet]
        public HttpResponseMessage list(HttpRequestMessage request, string bookname = null, int pagesize = 5, int page = 1)
        {
            Expression<Func<Book, bool>> namefilter;
            if (bookname != null)
            {
                namefilter = l => l.BookName.StartsWith(bookname);
            }
            else
            {
                namefilter = l => l.IsDeleted != true;
            }

            var objs = bookRepo.GetWithoutTracking().Where(a => a.IsDeleted != true).Where(namefilter).OrderBy(x => x.BookName);
            var totalCount = objs.Count();
            var results = objs.Skip(pagesize * (page - 1)).Take(pagesize).ToList();
            var model = new PagedListServer<Book>(results, totalCount, pagesize);

            return request.CreateResponse<PagedListServer<Book>>(HttpStatusCode.OK, model);
        }

        [HttpGet]
        [Route("api/book/delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int ID)
        {
            Book UpdatedEntity = new Book();
            Book result = bookRepo.Get().FirstOrDefault(a => a.BookId == ID);
            result.IsDeleted = true;

            UpdatedEntity = bookRepo.UpdatewithObj(result);
            return request.CreateResponse<Book>(HttpStatusCode.OK, UpdatedEntity);
        }

    }
}