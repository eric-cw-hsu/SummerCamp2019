using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace workshop.Models
{
    public class BookSearch
    {
        [DisplayName("書名")]
        public string BookName { get; set; }
        [DisplayName("圖書類別")]
        public string BookCategory { get; set; }
        [DisplayName("借閱人")]
        public string LendName { get; set; }
        [DisplayName("借閱狀態")]
        public string BookStatus { get; set; }
    }
}