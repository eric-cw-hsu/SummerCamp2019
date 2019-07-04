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
            string sql = @"SELECT BD.BOOK_ID, BOOK_NAME, CODE_NAME, USER_ENAME, BOOK_CLASS_NAME, 
                            FORMAT(BOOK_BOUGHT_DATE, 'yyyy/MM/dd') AS BOOK_BOUGHT_DATE
                            FROM BOOK_DATA AS BD   

                            LEFT JOIN MEMBER_M AS MM
                            ON BD.BOOK_KEEPER = MM.USER_ID

                            INNER JOIN BOOK_CLASS AS BC
                            ON BD.BOOK_CLASS_ID = BC.BOOK_CLASS_ID

                            INNER JOIN BOOK_CODE AS BCD
                            ON (BD.BOOK_STATUS = BCD.CODE_ID AND BCD.CODE_TYPE = 'BOOK_STATUS')

                            WHERE (BD.BOOK_NAME LIKE '%'+@BookName+'%' OR @BookName = '') AND
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
                cmd.Parameters.Add(new SqlParameter("@  BookBoughtDate", Book.BookBoughtDate));
                cmd.Parameters.Add(new SqlParameter("@BookClassId", Book.BookClassId == null ? (Object)DBNull.Value : Book.BookClassId));
                BookId = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
            }
            return BookId;
        }

        /// <summary>
        /// 刪除客戶
        /// </summary>
        public void DeleteBookById(string BookId)
        {
            try
            {
                DataTable dt = new DataTable();
                string sql = "Delete FROM BOOK_DATA " +
                    "         Where BOOK_ID = @BookId";
                using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add(new SqlParameter("@BookId", BookId));
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                    sqlAdapter.Fill(dt);
                    //cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
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

        /// UPDATE

        /// <summary>
        /// Upload 資料
        /// </summary>
        public void UpdateBookData(Models.Books arg)
        {
            DataTable dt = new DataTable();
            string sql = @"UPDATE BOOK_DATA
                            SET BOOK_NAME = @BookName,
                                    BOOK_AUTHOR = @BookAuthor,
                                    BOOK_PUBLISHER = @BookPublisher,
                                    BOOK_NOTE = @BookNote,
                                    BOOK_BOUGHT_DATE = @BookBoughtDate,
                                    BOOK_CLASS_ID = @BookClassId
                            WHERE BOOK_ID = @BookId";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                cmd.Parameters.Add(new SqlParameter("@BookName", arg.BookName == null ? "" : arg.BookName.ToString()));
                cmd.Parameters.Add(new SqlParameter("@BookAuthor", arg.BookAuthor == null ? "" : arg.BookAuthor.ToString()));
                cmd.Parameters.Add(new SqlParameter("@BookPublisher", arg.BookPublisher == null ? "" : arg.BookPublisher.ToString()));
                cmd.Parameters.Add(new SqlParameter("@BookNote", arg.BookNote == null ? "" : arg.BookNote.ToString()));
                cmd.Parameters.Add(new SqlParameter("@BookBoughtDate", arg.BookBoughtDate == null ? "" : arg.BookBoughtDate.ToString()));
                cmd.Parameters.Add(new SqlParameter("@BookClassId", arg.BookClassId == null ? "" : arg.BookClassId.ToString()));
                cmd.Parameters.Add(new SqlParameter("@BookId", Convert.ToInt32(arg.BookId)));
                sqlAdapter.Fill(dt);
                conn.Close();
            }
        }


        ///Edit
        /// <summary>
        /// Get Origin data
        /// </summary>
        public Models.Books GetOriginData(int BookId)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT BOOK_ID, BOOK_NAME, BOOK_AUTHOR, BOOK_PUBLISHER, BOOK_NOTE, BOOK_BOUGHT_DATE,
                                    BOOK_CLASS_NAME, BOOK_STATUS, CODE_NAME, (USER_ENAME + '-' + USER_CNAME) AS USER_NAME
                            FROM BOOK_DATA AS BD
                            LEFT JOIN MEMBER_M AS MM
                            ON BD.BOOK_KEEPER = MM.USER_ID

                            INNER JOIN BOOK_CLASS AS BC
                            ON BD.BOOK_CLASS_ID = BC.BOOK_CLASS_ID

                            INNER JOIN BOOK_CODE AS BCD
                            ON (BD.BOOK_STATUS = BCD.CODE_ID AND BCD.CODE_TYPE = 'BOOK_STATUS')
                            WHERE BOOK_ID = @BookId";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                cmd.Parameters.Add(new SqlParameter("@BookId", Convert.ToInt32(BookId)));
                sqlAdapter.Fill(dt);
                conn.Close();
            }

            return this.MapOriginData(dt);
        }

        private Models.Books MapOriginData(DataTable dt)
        {
            Models.Books result = new Models.Books();

            result.BookId = Convert.ToInt32(dt.Rows[0]["BOOK_ID"]);
            result.BookName = dt.Rows[0]["BOOK_NAME"].ToString();
            result.BookAuthor = dt.Rows[0]["BOOK_AUTHOR"].ToString();
            result.BookPublisher = dt.Rows[0]["BOOK_PUBLISHER"].ToString();
            result.BookNote = dt.Rows[0]["BOOK_NOTE"].ToString();
            result.BookBoughtDate = dt.Rows[0]["BOOK_BOUGHT_DATE"].ToString();
            result.BookClassName = dt.Rows[0]["BOOK_CLASS_NAME"].ToString();
            result.BookStatus= dt.Rows[0]["BOOK_STATUS"].ToString();
            result.BookCodeName = dt.Rows[0]["CODE_NAME"].ToString();
            result.UserName = dt.Rows[0]["USER_NAME"].ToString();
            return result;
        }
    }
}