using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace workshop.Models
{
    public class BookService
    {
        /// <summary>
        /// 取得DB連線字串
        /// </summary>
        /// <returns></returns>
        private string GetDBConnectionString()
        {
            return
                System.Configuration.ConfigurationManager.ConnectionStrings["GSSWEB"].ConnectionString.ToString();
        }

        /// <summary>
        /// 依照條件取得客戶資料
        /// </summary>
        /// <returns></returns>
        public List<Models.Books> GetBookByCondtioin(Models.BookSearch arg)
        {

            DataTable dt = new DataTable();
            string sql = @"SELECT BD.BOOK_ID, BOOK_NAME, CODE_NAME, USER_ENAME, BOOK_CLASS_NAME, FORMAT(BOOK_BOUGHT_DATE, 'yyyy/MM/dd') AS BOOK_BOUGHT_DATE
                            FROM BOOK_DATA AS BD   

                            LEFT JOIN MEMBER_M AS MM
                            ON BD.BOOK_KEEPER = MM.USER_ID

                            INNER JOIN BOOK_CLASS AS BC
                            ON BD.BOOK_CLASS_ID = BC.BOOK_CLASS_ID

                            INNER JOIN BOOK_CODE AS BCD
                            ON (BD.BOOK_STATUS = BCD.CODE_ID AND BCD.CODE_TYPE = 'BOOK_STATUS')

                            WHERE (BD.BOOK_NAME = @BookName OR @BookName = '') AND
                                    (BC.BOOK_CLASS_ID = @BookCategory OR @BookCategory = '') AND
                                    (USER_ID = @LendName OR @LendName = '') AND
                                    (CODE_ID = @BookStatus OR @BookStatus = '')
                            ORDER BY BOOK_CLASS_NAME, BOOK_NAME, USER_ENAME
                            ";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@BookName", arg.BookName == null ? string.Empty : arg.BookName));
                cmd.Parameters.Add(new SqlParameter("@BookCategory", arg.BookCategory == null ? string.Empty : arg.BookCategory));
                cmd.Parameters.Add(new SqlParameter("@LendName", arg.LendName == null ? string.Empty : arg.LendName));
                cmd.Parameters.Add(new SqlParameter("@BookStatus", arg.BookStatus == null ? string.Empty : arg.BookStatus));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapBookDataToList(dt);
        }

        /// <summary>
        /// 新增書籍
        /// </summary>
        /// <param name="Book"></param>
        /// <returns>員工編號</returns>
        public int InsertBook(Models.Books Book)
        {
            string sql = @"INSERT INTO BOOK_DATA
                            (
                                BOOK_NAME, BOOK_AUTHOR, BOOK_PUBLISHER, BOOK_NOTE, BOOK_BOUGHT_DATE, BOOK_CLASS_ID, BOOK_STATUS
                            )
                            VALUES
                            (
                                @BookName, @BookAuthor, @BookPublisher, @BookNote, @BookBoughtDate, @BookClassId, 'A'
                            )
                            Select SCOPE_IDENTITY()";
            int BookId;
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@BookName", Book.BookName));
                cmd.Parameters.Add(new SqlParameter("@BookAuthor", Book.BookAuthor));
                cmd.Parameters.Add(new SqlParameter("@BookPublisher", Book.BookPublisher));
                cmd.Parameters.Add(new SqlParameter("@BookNote", Book.BookNote));
                cmd.Parameters.Add(new SqlParameter("BookBoughtDate", Book.BookBoughtDate));
                cmd.Parameters.Add(new SqlParameter("@BookClassId", Book.BookClassId == null ? (Object)DBNull.Value : Book.BookClassId));
                BookId = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
            }
            return BookId;
        }



        /// <summary>
        /// Map資料進List
        /// </summary>
        /// <param name="BookData"></param> ??
        /// <returns></returns>
        private List<Models.Books> MapBookDataToList(DataTable BookData)
        {   
            List<Models.Books> result = new List<Books>();
            foreach (DataRow row in BookData.Rows)
            {
                result.Add(new Books()
                {
                    BookId = (int)row["Book_ID"],
                    BookName = row["Book_NAME"].ToString(),
                    BookBoughtDate = row["BOOK_BOUGHT_DATE"].ToString(),
                    BookStatus = row["CODE_NAME"].ToString(),
                    LendName = row["USER_ENAME"].ToString(),
                    BookCategory = row["BOOK_CLASS_NAME"].ToString(),
                });
            }
            return result;
        }
    }
}