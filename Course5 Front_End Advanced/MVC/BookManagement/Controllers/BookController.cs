using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;


namespace BookManagement.Controllers
{
    public class LibraryController : Controller
    {
        Models.CodeService CodeService = new Models.CodeService();
        Models.BookService BookService = new Models.BookService();
        public ActionResult Index()
        {
            return View();
        }

        ///GetBookByCondition
        [HttpPost()]
        public JsonResult GetBookData(Models.BookSearchArg arg)
        {
            return Json(BookService.GetBookByCondition(arg));
        }

        /// GetDropDownList
        [HttpPost()]
        public JsonResult GetClass()
        {
            return Json(this.CodeService.GetBookClassName());
        }
        /// GetDropDownList
        [HttpPost()]
        public JsonResult GetUserName()
        {
            return Json(this.CodeService.GetUserName());
        }
        /// GetDropDownList
        [HttpPost()]
        public JsonResult GetStatus()
        {
            return Json(this.CodeService.GetCodeName());
        }

        ///DeleteBook
        [HttpPost()]
        public void DeleteBook(Models.Books arg)
        {
            BookService.DeleteBook(arg.BookID);
        }

        public ActionResult InsertBook()
        {
            return View("InsertBook");
        }

        ///InsertBook
        [HttpPost()]
        public void InsertBook(Models.Books arg)
        {
            BookService.InsertBook(arg);
        }

        ///EditBook
        [HttpGet()]
        public ActionResult EditBook()
        {
            return View("EditBook");
        }

        ///GetBookDetail
        [HttpPost()]
        public JsonResult GetBookDetail(int BookId)
        {
            return Json(BookService.GetBookDetail(BookId));
        }

        ///UpdateBook
        [HttpPost()]
        public void UpdateBook(Models.Books book)
        {
            BookService.UpdateBookData(book);
        }

        ///BookLendRecord
        [HttpGet()]
        public ActionResult BookLendRecord()
        {
            return View("BookLendRecord");
        }
        ///GetBookRecord
        [HttpPost()]
        public JsonResult GetBookLendRecord(int BookId)
        {
            return Json(BookService.GetBookLendRecord(BookId));
        }
    }
}