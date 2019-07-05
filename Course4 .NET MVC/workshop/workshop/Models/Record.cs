using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace workshop.Models
{
    public class Record
    {
        [DisplayName("借閱日期")]
        public string LendDate { get; set; }

        [DisplayName("借閱人編號")]
        public int UserId { get; set; }

        [DisplayName("英文姓名")]
        public string UserEName { get; set; }

        [DisplayName("中文姓名")]
        public string UserCName { get; set; }
    }
}