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
            string sql = @"SELECT *
                           FROM BOOK_DATA";

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
                    BookName = row["Book_Name"].ToString(),
                    BoughtDate = row["BOOK_Bought_Date"].ToString(),
                    BookStatus = row["Book_Status"].ToString(),
                    //LendName = row["Lend_Name"].ToString(),
                    //BookCategory = row["Book_Category"].ToString(),
                });
            }
            return result;
        }
    }
}