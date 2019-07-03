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
        public List<Models.Books> GetEmployeeByCondtioin(Models.BookSearch arg)
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

                            WHERE (BD.BOOK_ID = @BookName OR @BookName = '') AND
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
            return this.MapEmployeeDataToList(dt);
        }


        /// <summary>
        /// Map資料進List
        /// </summary>
        /// <param name="employeeData"></param> ??
        /// <returns></returns>
        private List<Models.Books> MapEmployeeDataToList(DataTable employeeData)
        {   
            List<Models.Books> result = new List<Books>();
            foreach (DataRow row in employeeData.Rows)
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