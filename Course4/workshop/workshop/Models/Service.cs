using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace workshop.Models
{
    public class Service
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
        /// 取得書籍資料
        /// </summary> 
        /// <return></return>
        public List<SelectListItem> GetBook(string BookId)
        {
            DataTable dt = new DataTable();
            string sql = @"Select EmployeeID As CodeId, (FirstName + ' ' + LastName) As CodeName 
                           FROM HR.Employees
                           WHERE EmployeeID != @EmployeeId";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@BookId", BookId));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapCodeData(dt);
        }

        /// <summary>
        /// Maping 代碼資料
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private List<SelectListItem> MapCodeData(DataTable dt)
        {
            List<SelectListItem> result = new List<SelectListItem>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new SelectListItem()
                {
                    Text = row["CODE_NAME"].ToString(),
                    Value = row["CODE_ID"].ToString(),
                    Selected = true
                });
            }
            return result;
        }

        public List<SelectListItem> GetCodeTable(string type)
        {
            DataTable dt = new DataTable();
            string sql = @"Select Distinct CODE_NAME, CODE_ID
                           From Book_CODE 
                           Where Code_TYPE = @Type";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@Type", type));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapCodeData(dt);
        }

        public List<SelectListItem> GetTable(string type, string target, string targetId)
        {
            DataTable dt = new DataTable();
            string sql = @"Select Distinct " + target + ',' + targetId + " FROM " + type;

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapData(dt, target, targetId);
        }

        /// <summary>
        /// Maping 代碼資料
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private List<SelectListItem> MapData(DataTable dt, string target, string targetId)
        {
            List<SelectListItem> result = new List<SelectListItem>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new SelectListItem()
                {
                    Text = row[target].ToString(),
                    Value = row[targetId].ToString()
                });
            }
            return result;
        }

        public List<Models.Record> GetRecordTable(int UserId)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT FORMAT(LEND_DATE, 'yyyy-MM-dd') AS LEND_DATE, [USER_ID], USER_ENAME, USER_CNAME
                                    FROM BOOK_DATA AS BD
                                    INNER JOIN BOOK_LEND_RECORD AS BLR
                                    ON BD.BOOK_ID = BLR.BOOK_ID
                                    INNER JOIN MEMBER_M AS MM
                                    ON BLR.KEEPER_ID = MM.[USER_ID]
                                    WHERE BD.BOOK_ID = @UserId
                                    ORDER BY LEND_DATE DESC";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@UserId", UserId));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }

            List<Models.Record> result = new List<Record>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new Record()
                {
                    LendDate = row["LendDate"].ToString(),
                    UserId = Convert.ToInt32(row["UserId"]),
                    UserEName = row["UserEName"].ToString(),
                    UserCName = row["UserCName"].ToString()
                });
            }

            return result;
        }
    }


}