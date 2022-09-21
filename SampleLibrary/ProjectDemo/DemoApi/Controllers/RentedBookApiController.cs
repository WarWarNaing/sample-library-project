using DAL.Model;
using DemoApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace DemoApi.Controllers
{
    public class RentedBookApiController : ApiController
    {
        LibraryDBContext dbContext;
        private RentedBookRepository rentedBookRepo = null;
        private BookRepository bookRepo = null;
        private CustomerRepository custRepo = null;

        public RentedBookApiController()
        {
            dbContext = new LibraryDBContext();
            rentedBookRepo = new RentedBookRepository(dbContext);
            bookRepo = new BookRepository(dbContext);
            custRepo = new CustomerRepository(dbContext);
        }

        [Route("api/rentedbook/save")]
        [HttpPost]
        public HttpResponseMessage upsert(HttpRequestMessage request, RentedBook book)
        {
            RentedBook UpdateEntity = null;
            try
            {
                if (book.Id > 0)
                {
                    RentedBook result = rentedBookRepo.Get().Where(x => x.Id == book.Id).FirstOrDefault();
                    result.IsDeStrict = book.IsDeStrict;
                    result.PenaltyFee = book.PenaltyFee;
                    result.IsRePay = true;
                    UpdateEntity = rentedBookRepo.UpdatewithObj(result);
                }
                else
                {
                    book.AccessTime = DateTime.UtcNow;
                    book.CustomerName = custRepo.Get().Where(x => x.CustomerId == book.CustomerId).Select(x => x.CustomerName).FirstOrDefault();
                    book.BookName = bookRepo.Get().Where(x => x.BookId == book.BookId).Select(x => x.BookName).FirstOrDefault();
                    UpdateEntity = rentedBookRepo.AddWithGetObj(book);
                }

                Book bookentity = bookRepo.Get().Where(x => x.BookId == UpdateEntity.BookId && x.IsDeleted != true).FirstOrDefault();

                int noOfRentedBooks = rentedBookRepo.Get().Where(x => x.BookId == UpdateEntity.BookId && x.IsDeleted != true && x.IsRePay != true).Count();
                int noOfexistingBooks = bookentity.Quantity;

                if (noOfexistingBooks > noOfRentedBooks)
                {
                    bookentity.IsAvailable = true;
                }
                else
                {
                    bookentity.IsAvailable = false;
                }
                bookRepo.UpdatewithObj(bookentity);
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

            return request.CreateResponse<RentedBook>(HttpStatusCode.OK, UpdateEntity);
        }

        [Route("api/rentedbook/list")]
        [HttpGet]
        public HttpResponseMessage list(HttpRequestMessage request, string bookname = null, string customername = null, int pagesize = 10, int page = 1)
        {
            Expression<Func<RentedBook, bool>> namefilter, customerfilter;
            if (bookname != null)
            {
                namefilter = l => l.BookName.StartsWith(bookname);
            }
            else
            {
                namefilter = l => l.IsDeleted != true;
            }

            if (customername != null)
            {
                customerfilter = l => l.CustomerName.StartsWith(customername);
            }
            else
            {
                customerfilter = l => l.IsDeleted != true;
            }
            var objs = rentedBookRepo.GetWithoutTracking().Where(x => x.IsDeleted != true && x.IsRePay != true).Where(namefilter).Where(customerfilter).OrderBy(x => x.AccessTime);
            var totalCount = objs.Count();
            var results = objs.Skip(pagesize * (page - 1)).Take(pagesize).ToList();
            var model = new PagedListServer<RentedBook>(results, totalCount, pagesize);

            return request.CreateResponse<PagedListServer<RentedBook>>(HttpStatusCode.OK, model);
        }

        [Route("api/rentedbook/getRecordById")]
        [HttpGet]
        public HttpResponseMessage getBookById(HttpRequestMessage request, int ID)
        {
            RentedBook customer = rentedBookRepo.GetWithoutTracking().FirstOrDefault(a => a.IsDeleted != true && a.Id == ID);
            return request.CreateResponse<RentedBook>(HttpStatusCode.OK, customer);
        }
    }
}