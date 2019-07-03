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
        [DisplayName("購買日期")]
        public string BoughtDate { get; set; }

        /// <summary>
        /// 職稱
        /// </summary>
        [DisplayName("借閱狀態")]
        [Required(ErrorMessage = "此欄位必填")]
        public string BookStatus { get; set; }

        [DisplayName("借閱人")]
        [Required(ErrorMessage = "此欄位必填")]
        public string LendName { get; set; }

        /// <summary>
        /// 稱謂
        /// </summary>
        [DisplayName("稱謂")]
        [Required(ErrorMessage = "此欄位必填")]
        public string TitleOfCourtesy { get; set; }


        /// <summary>
        /// 任職日期
        /// </summary>
        [DisplayName("書籍種類")]
        [Required(ErrorMessage = "此欄位必填")]
        public string BookCategory { get; set; }

    
    }
}