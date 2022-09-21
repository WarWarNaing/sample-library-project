using DAL.Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Helper
{
   public  class CustomerApiRequestHelper
    {
        public static async Task<Customer> GetCustomerById(int ID)
        {
            string url = string.Format("api/customer/getCustomerById?ID={0}", ID);
            Customer result = await ApiRequest<Customer>.GetRequest(url);
            return result;
        }

        public static async Task<Customer> UpsertCustomer(Customer customer)
        {
            string url = string.Format("api/customer/upsert");
            Customer result = await ApiRequest<Customer>.PostRequest(url, customer);
            return result;
        }

        public static async Task<PagedListClient<Customer>> GetCustomerListWithPaging(string customername = null, int pagesize = 10, int page = 1)
        {
            string url = $"api/customer/list?customername={customername}&pagesize={pagesize}&page={page}";
            var data = await ApiRequest<PagedListServer<Customer>>.GetRequest(url);
            var model = new PagedListClient<Customer>();
            var pagedList = new StaticPagedList<Customer>(data.Results, page, pagesize, data.TotalCount);
            model.Results = pagedList;
            model.TotalCount = data.TotalCount;
            model.TotalPages = data.TotalPages;
            return model;
        }

        public static async Task<Customer> delete(int ID)
        {
            var url = string.Format("api/customer/delete?ID={0}", ID);
            Customer result = await ApiRequest<Customer>.GetRequest(url);
            return result;
        }

        public static async Task<List<Location>> GetState()
        {
            string url = string.Format("api/customer/GetState");
            return await ApiRequest<List<Location>>.GetRequest(url);
        }

        public static async Task<IEnumerable<string>> GetTownShip(string state = null)
        {
            string url = string.Format("api/customer/GetTownShip?state={0}", state);
            return await ApiRequest<IEnumerable<string>>.GetRequest(url);
        }

        public static async Task<List<Customer>> exclusiveList(string q = null)
        {
            string url = string.Format("api/customer/exclusiveList?q={0}", q);
            var data = await ApiRequest<List<Customer>>.GetRequest(url);
            return data;
        }
    }
}