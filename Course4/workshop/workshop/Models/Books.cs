using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace workshop.Models
{
    public class Books
    {
        [DisplayName("書籍編號")]
        [Required(ErrorMessage = "此欄位必填")]
        public int BookId { get; set; }

        [DisplayName("書籍名稱")]
        [Required(ErrorMessage = "此欄位必填")]
        public string BookName { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        [DisplayName("購買日期")]
        public string BookBoughtDate { get; set; }

        [DisplayName("借閱狀態")]
        public string BookStatus { get; set; }


        [DisplayName("借閱人")]
        public string LendName { get; set; }

        [DisplayName("書籍種類")]
        public string BookCategory { get; set; }

        [DisplayName("作者")]
        [Required(ErrorMessage = "此欄位必填")]
        public string BookAuthor { get; set; }

        [DisplayName("出版商")]
        [Required(ErrorMessage = "此欄位必填")]
        public string BookPublisher { get; set; }

        [DisplayName("內容簡介")]
        [Required(ErrorMessage = "此欄位必填")]
        public string BookNote { get; set; }

        [DisplayName("類別代號")]
        [Required(ErrorMessage = "此欄位必填")]
        public string BookClassId { get; set; }

        [DisplayName("類別名稱")]
        [Required(ErrorMessage = "此欄位必填")]
        public string BookClassName { get; set; }

        [DisplayName("狀態名稱")]
        [Required(ErrorMessage = "此欄位必填")]
        public string BookCodeName { get; set; }

        [DisplayName("英文姓名")]
        [Required(ErrorMessage = "此欄位必填")]
        public string UserEName { get; set; }

        [DisplayName("中文姓名")]
        [Required(ErrorMessage = "此欄位必填")]
        public string UserCName { get; set; }

        [DisplayName("借閱人")]
        [Required(ErrorMessage = "此欄位必填")]
        public string UserName { get; set; }
    }
    
}