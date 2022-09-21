using DAL.Helper;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Demo.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> CustomerForm(string FormType, int ID)
        {
            Customer customer = new Customer();
            if (FormType == "Add")
            {
                return PartialView("_customerForm", customer);
            }
            else
            {
                Customer result = await CustomerApiRequestHelper.GetCustomerById(ID);
                return PartialView("_customerForm", result);
            }
        }

        public async Task<ActionResult> CustomerList(string customername = null, int pagesize = 10, int page = 1)
        {
            PagedListClient<Customer> result = await CustomerApiRequestHelper.GetCustomerListWithPaging(customername, pagesize, page);
            return PartialView("_customerList", result);
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Save(Customer customer)
        {
            Customer result = await CustomerApiRequestHelper.UpsertCustomer(customer);
            if (result != null)
            {
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Fail", JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> Delete(int ID = 0)
        {
            Customer result = await CustomerApiRequestHelper.delete(ID);
            if (result != null)
            {
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Fail", JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> GetState()
        {
            List<Location> result = await CustomerApiRequestHelper.GetState();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetTownShip(string state = null)
        {
            IEnumerable<string> result = await CustomerApiRequestHelper.GetTownShip(state);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> _getCustomerList(string q = null)
        {
            List<Customer> result = await CustomerApiRequestHelper.exclusiveList(q);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetCustomerById (int ID)
        {
            Customer result = await CustomerApiRequestHelper.GetCustomerById(ID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}