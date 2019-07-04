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
        /// <summary>
        /// 員工編號
        /// </summary>
        ///[MaxLength(5)]
        [DisplayName("書籍編號")]
        [Required(ErrorMessage = "此欄位必填")]
        public int BookId { get; set; }

        /// <summary>
        /// 員工姓名(First Name)
        /// </summary>
        [DisplayName("書籍名稱")]
        [Required(ErrorMessage = "此欄位必填")]
        public string BookName { get; set; }

        /// <summary>
        /// 員工姓名(Last Name)
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        [DisplayName("購買日期")]
        public string BookBoughtDate { get; set; }

        /// <summary>
        /// 職稱
        /// </summary>
        [DisplayName("借閱狀態")]
        public string BookStatus { get; set; }


        [DisplayName("借閱人")]
        public string LendName { get; set; }


        /// <summary>
        /// 任職日期
        /// </summary>
        [DisplayName("書籍種類")]
        public string BookCategory { get; set; }

        /// <summary>
        /// 任職日期
        /// </summary>
        [DisplayName("作者")]
        [Required(ErrorMessage = "此欄位必填")]
        public string BookAuthor { get; set; }

        /// <summary>
        /// 任職日期
        /// </summary>
        [DisplayName("出版商")]
        [Required(ErrorMessage = "此欄位必填")]
        public string BookPublisher { get; set; }

        /// <summary>
        /// 任職日期
        /// </summary>
        [DisplayName("內容簡介")]
        [Required(ErrorMessage = "此欄位必填")]
        public string BookNote { get; set; }

        /// <summary>
        /// 任職日期
        /// </summary>
        [DisplayName("類別代號")]
        [Required(ErrorMessage = "此欄位必填")]
        public string BookClassId { get; set; }

        /// <summary>
        /// 任職日期
        /// </summary>
        [DisplayName("類別名稱")]
        [Required(ErrorMessage = "此欄位必填")]
        public string BookClassName { get; set; }


        /// <summary>
        /// 任職日期
        /// </summary>
        [DisplayName("狀態名稱")]
        [Required(ErrorMessage = "此欄位必填")]
        public string BookCodeName { get; set; }


        /// <summary>
        /// 任職日期
        /// </summary>
        [DisplayName("英文姓名")]
        [Required(ErrorMessage = "此欄位必填")]
        public string UserEName { get; set; }

        /// <summary>
        /// 任職日期
        /// </summary>
        [DisplayName("中文姓名")]
        [Required(ErrorMessage = "此欄位必填")]
        public string UserCName { get; set; }

        /// <summary>
        /// 任職日期
        /// </summary>
        [DisplayName("借閱人")]
        [Required(ErrorMessage = "此欄位必填")]
        public string UserName { get; set; }
    }
    
}