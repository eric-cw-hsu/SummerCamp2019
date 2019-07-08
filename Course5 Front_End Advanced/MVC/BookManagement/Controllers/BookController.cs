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
        /*
        public ActionResult EditBook()
        {
            return View("EditBook");
        }
        */
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

        ///Replace <BR>
        [HttpPost()]
        public static string Replace(string BookNote)
        { 
            return Regex.Replace(BookNote.ToString(), "<BR>", "\r\n"); 
        }
    }
    
    public class BookController : Controller
    {
        readonly Models.CodeService codeService = new Models.CodeService();
        readonly Models.BookService bookService = new Models.BookService();


        /// <summary>
        /// 圖書資料查詢
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public ActionResult Index()
        {
            ViewBag.UserNameData = this.codeService.GetUserName();
            ViewBag.BookClassNameData = this.codeService.GetBookClassName();
            ViewBag.CodeNameData = this.codeService.GetCodeName();
            return View();
        }

        /// <summary>
        /// 圖書資料查詢
        /// </summary>
        /// <returns></returns>
        [HttpPost()]
        public ActionResult Index(Models.BookSearchArg arg)
        {
            ViewBag.UserNameData = this.codeService.GetUserName();
            ViewBag.BookClassNameData = this.codeService.GetBookClassName();
            ViewBag.CodeNameData = this.codeService.GetCodeName();
            ViewBag.SearchResult = bookService.GetBookByCondition(arg);
            return View("Index");
        }


        /// <summary>
        /// 新增圖書畫面
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public ActionResult InsertBook()
        {
            ViewBag.BookClassNameData = this.codeService.GetBookClassName();
            return View(new Models.Books());
        }

        /// <summary>
        /// 新增圖書
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPost()]
        public ActionResult InsertBook(Models.Books book)
        {
            ViewBag.BookClassNameData = this.codeService.GetBookClassName();
            if (ModelState.IsValid)
            {
                try
                {
                    DateTime dateTime = DateTime.Parse(book.BookBoughtDate);
                    int BookID = bookService.InsertBook(book);
                    return RedirectToAction("BookData", new { BookID = BookID });
                }
                catch
                {
                    Response.Write("<script language=javascript>alert('日期格式錯誤')</script>");
                }
            }
            return View(book);
        }

        /// <summary>
        /// 刪除圖書
        /// </summary>
        /// <param name="BookID"></param>
        /// <returns></returns>
        [HttpPost()]
        public JsonResult DeleteBook(int BookID)
        {
            try
            {
                bookService.DeleteBook(BookID);
                return this.Json(true);
            }
            catch (Exception ex)
            {
                return this.Json(false);
            }
        }

        /// <summary>
        /// 明細圖書畫面
        /// </summary>
        /// <param name="BookID"></param>
        /// <returns></returns>
        [HttpGet()]
        public ActionResult BookData(int BookID)
        {
            Models.Books books = bookService.GetBookDetail(BookID).FirstOrDefault();
            return View(books);
        }

        /// <summary>
        /// 修改圖書畫面
        /// </summary>
        /// <param name="BookID"></param>
        /// <returns></returns>
        [HttpGet()]
        public ActionResult UpdateBook(int BookID)
        {
            ViewBag.BookClassNameData = this.codeService.GetBookClassName();
            ViewBag.UserNameData = this.codeService.GetUserName();
            ViewBag.CodeNameData = this.codeService.GetCodeName();
            Models.Books books = bookService.GetBookData(BookID).FirstOrDefault();
            return View(books);
        }

        /// <summary>
        /// 修改圖書存檔
        /// </summary>
        /// <param name="BookID"></param>
        /// <param name="books"></param>
        /// <returns></returns>
        [HttpPost()]
        public ActionResult UpdateBook(Models.Books books)
        {
            ViewBag.BookClassNameData = this.codeService.GetBookClassName();
            ViewBag.UserNameData = this.codeService.GetUserName();
            ViewBag.CodeNameData = this.codeService.GetCodeName();
            if (ModelState.IsValid)
            {
                try
                {
                    DateTime dateTime = DateTime.Parse(books.BookBoughtDate);
                    bookService.UpdateBookData(books);
                    return RedirectToAction("BookData", new { BookID = books.BookID });
                }
                catch(Exception ex)
                {
                    Response.Write("<script language=javascript>alert('日期格式錯誤')</script>");
                }
            }
            return View(books);
        }

        /// <summary>
        /// 借閱紀錄畫面
        /// </summary>
        /// <param name="BookID"></param>
        /// <returns></returns>
        [HttpGet()]
        public ActionResult BookLendRecord(int BookID)
        {
            ViewBag.LendRecord = bookService.GetBookLendRecord(BookID);
            return View("BookLendRecord");
        }
    }
}