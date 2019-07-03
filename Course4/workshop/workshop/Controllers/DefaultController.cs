using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace workshop.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 員工資料查詢(查詢)
        /// </summary>
        /// <returns></returns>
        [HttpPost()]
        public ActionResult Index(Models.BookSearch arg)
        {
            Models.BookService BookService = new Models.BookService();
            //if (arg.HireDateEnd == null)
            //    arg.HireDateEnd = DateTime.Now.ToShortDateString();
            ViewBag.SearchResult = BookService.GetEmployeeByCondtioin(arg);
            //ViewBag.JobTitleCodeData = this.Service.GetCodeTable("TITLE");
            return View("Index");
        }
    }

   
}