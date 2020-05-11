using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_page.Models
{
    class DBHelper
    {
        /// <summary>
        /// 数据库帮助类
        /// </summary>
        //@"server=LAPTOP-UIJTGT3K\;database=WorldDB;integrated security=SSPI"
        //Reference of reference and select assemblies sysetem.configration
        private static string Connstr = @"server=LAPTOP-UIJTGT3K\;database=DolphinQuery;integrated security=SSPI";
        ///<summary>
        ///制执行添加、删除、修改的操作
        /// 
        /// </summary>        
        /// 
        public static object ExecuteScalar(string sql,params SqlParameter[] paras)
        {
            using(SqlConnection conn=new SqlConnection(Connstr))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddRange(paras);
                return command.ExecuteScalar();
            }
        }

        public static int ExecuteNonQuery(string sq1, params SqlParameter[] paras)
        {
            using (SqlConnection conn = new SqlConnection(Connstr))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(sq1, conn);
                command.Parameters.AddRange(paras);
                int result = command.ExecuteNonQuery();
                return result;
            }
        }

        public static SqlDataReader ExecuteReader(string sq1, params SqlParameter[] paras)//Can't use 'using' because read line by line
        {
            SqlConnection conn = new SqlConnection(Connstr);
            conn.Open();
            SqlCommand command = new SqlCommand(sq1, conn);
            if (paras.Length > 0)
            {
                command.Parameters.AddRange(paras);
            }

            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }
         
        public static DataRow GetDataRow(string sq1, params SqlParameter[] paras)
        {
            DataTable dt = null;
            using (SqlConnection conn = new SqlConnection(Connstr))
            {
                SqlCommand command = new SqlCommand(sq1, conn);
                command.Parameters.AddRange(paras);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                dt = new DataTable();
                adapter.Fill(dt);
            }
            if (dt.Rows.Count > 0)
                return dt.Rows[0];
            else
                return null;
        }
        public static DataTable GetDataTable(string sq1, params SqlParameter[] paras)
        {
            DataTable dt = null;
            using (SqlConnection conn = new SqlConnection(Connstr))
            {
                SqlCommand command = new SqlCommand(sq1, conn);
                command.Parameters.AddRange(paras);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                dt = new DataTable();
                adapter.Fill(dt);
            }
            return dt;
        }

        public static DataSet GetDataSet(string sq1, params SqlParameter[] paras)
        {
            DataSet ds = null;
            using (SqlConnection conn = new SqlConnection(Connstr))
            {
                SqlCommand command = new SqlCommand(sq1, conn);
                command.Parameters.AddRange(paras);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                ds = new DataSet();
                adapter.Fill(ds);
            }
            return ds;
        }
        //public static int GetId(string sql,params SqlParameter[] paras)
        //{
        //    using (SqlConnection conn = new SqlConnection(Connstr))
        //    {
        //        SqlCommand command = new SqlCommand(sql, conn);
        //        command.Parameters.AddRange(paras);
        //        SqlDataAdapter adapter = new SqlDataAdapter(command);
        //        dt = new DataTable();
        //        adapter.Fill(dt);
        //    }
        //}
    }
}

