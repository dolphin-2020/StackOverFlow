using MVC_page.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_page.Controllers
{
    public class LoginController: Controller
    {
        string user = "2";
        public ActionResult Login()
        {
            ViewBag.user =int.Parse( user);
            return View();
        }

        //public string GetUserId( int? id)
        //{
        //    this.user =Request[id.ToString()];
        //    return id.ToString();
        //}

        public string submit(UserInfo a)
        {
            string sql = @"insert into [User](name,email,password)values(@name,@email,@password)";
            SqlParameter[] p =
            {
                new SqlParameter("@name",a.Name),
                new SqlParameter("@email",a.Email),
                new SqlParameter("@password",a.password)
            };
            int x= DBHelper.ExecuteNonQuery(sql, p);
            if (x > 0)
            {
                SqlParameter[] paras =
                {
                    new SqlParameter("@name",a.Name),
                    new SqlParameter("@email",a.Email),
                };
                string sql2 = $"SELECT Id FROM [User] WHERE email=@email and Name=@name";
                var temp= DBHelper.GetDataTable(sql2, paras);
                string id="";
                for (int i=0;i< temp.Rows.Count;i++)
                {
                    id =temp.Rows[i]["id"].ToString();
                    user = id;
                }
                return id;
            }
            else
            {
                return "0";
            }
        }
    }
}