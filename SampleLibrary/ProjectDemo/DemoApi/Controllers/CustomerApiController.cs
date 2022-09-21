using DAL.Model;
using DemoApi.Repository;
using LinqKit;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DemoApi.Controllers
{
    public class CustomerApiController : ApiController
    {
        LibraryDBContext dbContext;
        private CustomerRepository customerRepo = null;
        public CustomerApiController()
        {
            dbContext = new LibraryDBContext();
            customerRepo = new CustomerRepository(dbContext);
        }

        [Route("api/customer/getCustomerById")]
        [HttpGet]
        public HttpResponseMessage getCustomerById(HttpRequestMessage request, int ID)
        {
            Customer customer = customerRepo.GetWithoutTracking().FirstOrDefault(a => a.IsDeleted != true && a.CustomerId == ID);
            return request.CreateResponse<Customer>(HttpStatusCode.OK, customer);
        }

        [Route("api/customer/upsert")]
        [HttpPost]
        public HttpResponseMessage upsert(HttpRequestMessage request, Customer customer)
        {
            Customer UpdateEntity = null;

            try
            {
                if (customer.CustomerId > 0)
                {
                    customer.JoinedDate = DateTime.UtcNow;
                    UpdateEntity = customerRepo.UpdatewithObj(customer);
                }
                else
                {
                    customer.JoinedDate = DateTime.UtcNow;
                    customer.IsDeleted = false;
                    UpdateEntity = customerRepo.AddWithGetObj(customer);
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

            return request.CreateResponse<Customer>(HttpStatusCode.OK, UpdateEntity);
        }

        [Route("api/customer/list")]
        [HttpGet]
        public HttpResponseMessage list(HttpRequestMessage request, string customername = null, int pagesize = 10, int page = 1)
        {
            Expression<Func<Customer, bool>> namefilter;
            if (customername != null)
            {
                namefilter = l => l.CustomerName.StartsWith(customername);
            }
            else
            {
                namefilter = l => l.IsDeleted != true;
            }

            var objs = customerRepo.GetWithoutTracking().Where(a => a.IsDeleted != true).Where(namefilter).OrderBy(x => x.CustomerName);
            var totalCount = objs.Count();
            var results = objs.Skip(pagesize * (page - 1)).Take(pagesize).ToList();
            var model = new PagedListServer<Customer>(results, totalCount, pagesize);

            return request.CreateResponse<PagedListServer<Customer>>(HttpStatusCode.OK, model);
        }

        [HttpGet]
        [Route("api/customer/delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int ID)
        {
            Customer UpdatedEntity = new Customer();
            Customer article = customerRepo.Get().FirstOrDefault(a => a.CustomerId == ID);
            article.IsDeleted = true;

            UpdatedEntity = customerRepo.UpdatewithObj(article);
            return request.CreateResponse<Customer>(HttpStatusCode.OK, UpdatedEntity);
        }

        [Route("api/customer/getState")]
        [HttpGet]
        public HttpResponseMessage GetState(HttpRequestMessage request)
        {
            List<Location> location = dbContext.Locations.Where(a => a.IsDeleted != true).DistinctBy(a => a.StateDivision).ToList();
            return request.CreateResponse<List<Location>>(HttpStatusCode.OK, location);
        }

        [Route("api/customer/gettownship")]
        [HttpGet]
        public HttpResponseMessage GetTownShip(HttpRequestMessage request, string state = null)
        {
            Expression<Func<Location, bool>> statefilter = null;
            if (state != null)
            {
                statefilter = a => a.StateDivision.StartsWith(state);
            }
            else
            {
                statefilter = a => a.IsDeleted != true;
            }
            IEnumerable<string> township = dbContext.Locations.Where(a => a.StateDivision == state).Where(statefilter).Select(a => a.Township).ToList();
            return request.CreateResponse<IEnumerable<string>>(HttpStatusCode.OK, township);
        }

        [HttpGet]
        [Route("api/customer/exclusiveList")]
        public HttpResponseMessage ExclusiveList(HttpRequestMessage request, string q = null)
        {
            Expression<Func<Customer, bool>> namefilter = null;
            if (q != null)
            {
                namefilter = PredicateBuilder.New<Customer>();
                namefilter = namefilter.Or(l => l.CustomerName.StartsWith(q));
            }
            else
            {
                namefilter = l => l.IsDeleted != true;
            }

            List<Customer> result = customerRepo.Get().Where(a => a.IsDeleted != true).Where(namefilter).ToList();
            return request.CreateResponse<List<Customer>>(HttpStatusCode.OK, result);
        }

    }
}