using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using WebApplication1;
using System.Web.Script.Serialization;

namespace WebApplication1.Controllers
{
    public class AddTitleController : DefaultController
    {
        string mysqlconnstr = "server=127.0.0.1;user id=root;database=lydb;password=w348031705;sslmode=None;charset=utf8";
        // GET: AddTitle
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Add_title at)
        {
            string erro = "";
            string title = at.title;
            string text = at.text;
            bool jug = true;
            if (title.Length <= 5 || title.Length >= 50)
            {
                jug = false;
                erro = "标题过短或者过长";
            }
            if (text.Length <= 5 || text.Length >= 500)
            {
                jug = false;
                erro = "标题过短或者过长";
            }
            if (jug != false)
            {
                /*
                    添加验证用户名密码代码
                */
                MySqlParameter p1 = new MySqlParameter("@title", MySqlDbType.VarChar, 255);
                p1.Value = title;
                MySqlParameter p2 = new MySqlParameter("@text", MySqlDbType.VarChar, 255);
                p2.Value = text;
                string time=DateTime.Now.ToString();
                MySqlParameter p3 = new MySqlParameter("@time", MySqlDbType.VarChar, 255);
                p3.Value = time;
                MySqlParameter p4 = new MySqlParameter("@userId", MySqlDbType.Int32);
                p4.Value = Session["id"];
                int k = 0;
                using (MySqlConnection con = new MySqlConnection(mysqlconnstr))
                {
                    con.Open();
                    MySqlCommand comm = new MySqlCommand();
                    comm.CommandType = System.Data.CommandType.Text;
                    comm.Connection = con;
                    comm.CommandText = "insert into  title(text,head_txt,time,userId)values(@text,@title,@time,@userId)";
                    comm.Parameters.Add(p1);
                    comm.Parameters.Add(p2);
                    comm.Parameters.Add(p3);
                    comm.Parameters.Add(p4);
                    k = comm.ExecuteNonQuery();
                }
                if (k != 0)
                {
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    Response.Write("<script>alert('添加失败!');</script>");

                    return View();
                }
            }
            else
            {
                /*返回序列话的Json数据
                var serializer = new JavaScriptSerializer();
                return Content(serializer.Serialize(us));*/
                Response.Write("<script>alert('添加失败!" + erro + "');</script>");
                return View();
            }

        }
    }
}