using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace workshop.Controllers
{
    public class LibraryController : Controller
    {
        Models.Service Service = new Models.Service();

        // GET: Default
        public ActionResult Index()
        {
            ViewBag.LendName = this.Service.GetTable("MEMBER_M", "USER_ENAME", "USER_ID");
            ViewBag.BookCategory = this.Service.GetTable("BOOK_CLASS", "BOOK_CLASS_NAME", "BOOK_CLASS_ID");
            ViewBag.BookStatus = this.Service.GetCodeTable("BOOK_STATUS");
            return View();
        }

        /// <summary>
        /// 員工資料查詢(查詢)
        /// </summary>
        /// <returns></returns>
        [HttpPost()]
        public ActionResult Index(Models.BookSearch arg)
        {
            ViewBag.LendName = this.Service.GetTable("MEMBER_M", "USER_ENAME", "USER_ID");
            ViewBag.BookCategory = this.Service.GetTable("BOOK_CLASS", "BOOK_CLASS_NAME", "BOOK_CLASS_ID");
            ViewBag.BookStatus = this.Service.GetCodeTable("BOOK_STATUS");
            Models.BookService BookService = new Models.BookService();
            //if (arg.HireDateEnd == null)
            //    arg.HireDateEnd = DateTime.Now.ToShortDateString();
            ViewBag.SearchResult = BookService.GetBookByCondtioin(arg);
            //ViewBag.JobTitleCodeData = this.Service.GetCodeTable("TITLE");
            return View("Index");
        }

        [HttpGet()]
        public ActionResult InsertBook()
        {
            ViewBag.BookCategory = this.Service.GetTable("BOOK_CLASS", "BOOK_CLASS_NAME", "BOOK_CLASS_ID");
            return View(new Models.Books());
        }

        [HttpPost()]
        public ActionResult InsertBook(Models.Books Book)
        {
            ViewBag.BookCategory = this.Service.GetTable("BOOK_CLASS", "BOOK_CLASS_NAME", "BOOK_CLASS_ID");

            if (ModelState.IsValid)
            {
                Models.BookService BookService = new Models.BookService();
                if (Book.BookBoughtDate != null)
                    Book.BookBoughtDate = Book.BookBoughtDate.Replace(",", "");
                BookService.InsertBook(Book);
                TempData["message"] = "存檔成功";
            }
            return View(Book);
        }

        /// <summary>
        /// 刪除員工
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpPost()]
        public JsonResult DeleteEmployee(string BookId)
        {
            try
            {
                Models.BookService BookService = new Models.BookService();
                BookService.DeleteBookById(BookId);
                return this.Json(true);
            }

            catch (Exception ex)
            {
                return this.Json(false);
            }
        }
    }

   
}