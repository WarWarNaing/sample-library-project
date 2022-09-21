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
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> BookForm(string FormType, int ID)
        {
            Book book = new Book();
            if (FormType == "Add")
            {
                return PartialView("_bookForm", book);
            }
            else
            {
                Book result = await BookApiRequestHelper.GetBookById(ID);
                return PartialView("_bookForm", result);
            }
        }

        public async Task<ActionResult> GetBookById (int ID)
        {
            RentedBook result = new RentedBook();
            Book book = await BookApiRequestHelper.GetBookById(ID);
            result.BookId = book.BookId;
            result.BookName = book.BookName;
            result.RentPrice = book.RentPrice;
            return PartialView("_bookRentForm", result);
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Save(Book book)
        {
            Book result =  await BookApiRequestHelper.UpsertBook(book);
            if (result != null)
            {
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Fail", JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> GetCategory()
        {
            List<Category> result = await BookApiRequestHelper.GetCategory();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetAuthor()
        {
            List<Author> result = await BookApiRequestHelper.GetAuthor();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> BookList(string bookname = null, int pagesize = 5, int page = 1)
        {
            PagedListClient<Book> result = await BookApiRequestHelper.GetBookListWithPaging(bookname, pagesize, page);
            return PartialView("_bookList", result);
        }

        public async Task<ActionResult> Delete(int ID = 0)
        {
            Book result = await BookApiRequestHelper.delete(ID);
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