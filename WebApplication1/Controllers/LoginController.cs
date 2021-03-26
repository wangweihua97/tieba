using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace WebApplication1.Controllers
{
    public class LoginController : Controller
    {
        string mysqlconnstr = "server=127.0.0.1;user id=root;database=lydb;password=w348031705;sslmode=None;charset=utf8";
        // GET: Login
        public ActionResult Index(string ReturnUrl)
        {
                if (Session["id"] != null)
                {
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.Url = ReturnUrl;
                return View();
            
        }
        [HttpPost]
        public ActionResult Index(Login_user lu)
        {
            string name = lu.username;
            string password = lu.password;
            string returnUrl = lu.url;
            if (lu.username == "admin" && lu.password == "w348031705")
            {
                Session["id"] = "-1";
                Session["name"] = "admin";
                return RedirectToRoute(new { controller = "Admin", action = "Charge" });
            }
            /*
                添加验证用户名密码代码
            */
            MySqlConnection conn = new MySqlConnection(mysqlconnstr);
            conn.Open();
            MySqlParameter f_name = new MySqlParameter("@name", MySqlDbType.VarChar);
            f_name.Value = name;
            string sqlstr = "select * from user where username =@name";
            MySqlCommand cmd = new MySqlCommand(sqlstr, conn);
            cmd.Parameters.Add(f_name);
            MySqlDataAdapter comm = new MySqlDataAdapter(cmd);
            System.Data.DataSet ds = new System.Data.DataSet();
            comm.Fill(ds, "table");       
            System.Data.DataTable dt = ds.Tables["table"];
            if (dt.Rows.Count!=0 )
            {
                if (dt.Rows[0]["password"].ToString() == password)
                {
                    Session["id"] = dt.Rows[0]["id"].ToString();
                    Session["name"] = dt.Rows[0]["name"].ToString();
                    if (dt.Rows[0]["img_path"].ToString() == "")
                        Session["img"] = "/Upload/null.jpg";
                    else
                    Session["img"] = dt.Rows[0]["img_path"].ToString();
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    Response.Write("<script>alert('密码错误!');</script>");
                    return View();
                }
            }
            else
            {
                Response.Write("<script>alert('账号错误!');</script>");
                return View("index");
            }

        }
        // POST: /Account/LogOff
        public ActionResult LogOff()
        {
            Session["id"] = null;
            Session["name"] = null;
            Session["img"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
    
}