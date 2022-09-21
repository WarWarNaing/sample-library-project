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
    public class RentedBooksController : Controller
    {
        // GET: RentedBook
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> SaveRentBook(RentedBook book)
        {
            RentedBook result = await RentedBookApiRequestHelper.SaveRentedBook(book);
            if (result != null)
            {
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Fail", JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> list(string bookname = null, string customername = null,  int pagesize = 10, int page = 1)
        {
            PagedListClient<RentedBook> result = await RentedBookApiRequestHelper.GetRentBookListWithPaging(bookname, customername, pagesize, page);
            return PartialView("_list", result);
        }

        public async Task<ActionResult> GetRecordById(int ID)
        {
            RentedBook result = await RentedBookApiRequestHelper.GetRecordById(ID);
            return PartialView("_bookForm", result);
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Save(RentedBook book)
        {
            RentedBook result = await RentedBookApiRequestHelper.SaveRentedBook(book);
            if (result != null)
            {
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Fail", JsonRequestBehavior.AllowGet);
            }
        }

    }
}