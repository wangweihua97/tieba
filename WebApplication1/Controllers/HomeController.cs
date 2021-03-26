using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace WebApplication1.Controllers
{
    public class HomeController : DefaultController
    {
        string mysqlconnstr = "server=127.0.0.1;user id=root;database=lydb;password=w348031705;sslmode=None;charset=utf8";
        public ActionResult Index()
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (Session["id"].ToString() == "-1")
            {
                return RedirectToRoute(new { controller = "Admin", action = "Charge" });
            }
                ViewData["name"] = Session["name"];
            MySqlConnection conn = new MySqlConnection(mysqlconnstr);
            conn.Open();
            string sqlstr = "select * from title order by id desc";
            MySqlCommand cmd = new MySqlCommand(sqlstr, conn);
            MySqlDataAdapter comm = new MySqlDataAdapter(cmd);
            System.Data.DataSet ds = new System.Data.DataSet();
            comm.Fill(ds);
            System.Data.DataTable dt = ds.Tables[0];
            int cout = dt.Rows.Count;
            List<Lytable> list = new List<Lytable>();
            Models.Lytable ly = null;
            for (int i = 0; i < cout; i++)
            {
                string userid = dt.Rows[i]["userId"].ToString();
                MySqlCommand cmd2 = new MySqlCommand("select name,img_path from user where id=@id", conn);
                MySqlParameter p1 = new MySqlParameter("@id", MySqlDbType.VarChar,10);
                p1.Value = userid;
                cmd2.Parameters.Add(p1);
                MySqlDataAdapter comm2 = new MySqlDataAdapter(cmd2);
                DataSet ds2 = new DataSet();
                comm2.Fill(ds2,"table");
                System.Data.DataTable dt2 = ds2.Tables["table"];
                string userid_name = dt2.Rows[0]["name"].ToString();
                string img_path = dt2.Rows[0]["img_path"].ToString();
                if (img_path == "")
                    img_path = "/Upload/null.jpg";
                ly = new Lytable()
                {
                    id = Convert.ToInt32(dt.Rows[i]["Id"].ToString()),
                    text = dt.Rows[i]["text"].ToString(),
                    time = dt.Rows[i]["time"].ToString(),
                    username = userid_name,
                    head_txt = dt.Rows[i]["head_txt"].ToString(),
                    img = img_path

                };
                list.Add(ly);
            }
            ViewData["data"] = list;
            conn.Close();
            return View();
        }

        public ActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
        public ActionResult Lookup(string tname)
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (Session["id"].ToString() == "-1")
            {
                return RedirectToRoute(new { controller = "Admin", action = "Charge" });
            }
            ViewData["name"] = Session["name"];
            MySqlConnection conn = new MySqlConnection(mysqlconnstr);
            conn.Open();
            string sqlstr = "select * from title where head_txt like '%" + tname+ "%' order by id desc";
            MySqlCommand cmd = new MySqlCommand(sqlstr, conn);
            MySqlDataAdapter comm = new MySqlDataAdapter(cmd);
            System.Data.DataSet ds = new System.Data.DataSet();
            comm.Fill(ds);
            System.Data.DataTable dt = ds.Tables[0];
            int cout = dt.Rows.Count;
            List<Lytable> list = new List<Lytable>();
            Models.Lytable ly = null;
            for (int i = 0; i < cout; i++)
            {
                string userid = dt.Rows[i]["userId"].ToString();
                MySqlCommand cmd2 = new MySqlCommand("select name,img_path from user where id=@id", conn);
                MySqlParameter p1 = new MySqlParameter("@id", MySqlDbType.VarChar, 10);
                p1.Value = userid;
                cmd2.Parameters.Add(p1);
                MySqlDataAdapter comm2 = new MySqlDataAdapter(cmd2);
                DataSet ds2 = new DataSet();
                comm2.Fill(ds2, "table");
                System.Data.DataTable dt2 = ds2.Tables["table"];
                string userid_name = dt2.Rows[0]["name"].ToString();
                string img_path = dt2.Rows[0]["img_path"].ToString();
                if (img_path == "")
                    img_path = "/Upload/null.jpg";
                ly = new Lytable()
                {
                    id = Convert.ToInt32(dt.Rows[i]["Id"].ToString()),
                    text = dt.Rows[i]["text"].ToString(),
                    time = dt.Rows[i]["time"].ToString(),
                    username = userid_name,
                    head_txt = dt.Rows[i]["head_txt"].ToString(),
                    img = img_path

                };
                list.Add(ly);
            }
            ViewData["data"] = list;
            conn.Close();
            return View("Index");
        }

        public ActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        /// <summary>
        /// 删除留言
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult delete(int id)
        {
            MySqlParameter parid = new MySqlParameter("@id", MySqlDbType.Int32);
            parid.Value = id;
            using (MySqlConnection conn = new MySqlConnection(mysqlconnstr))
            {
                conn.Open();
                MySqlCommand comm = new MySqlCommand();
                comm.CommandType = System.Data.CommandType.Text;
                comm.Connection = conn;
                comm.CommandText = "delete from title where `Id`=@id";
                comm.Parameters.Add(parid);
                comm.ExecuteNonQuery();

            }
            return View();
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="detail"></param>
        /// <returns></returns>
      
        [HttpPost]
        public ActionResult add(string detail)
        {
            MySqlParameter pardetail = new MySqlParameter("@text", MySqlDbType.VarChar, 255);
            pardetail.Value = detail;
            using (MySqlConnection con = new MySqlConnection(mysqlconnstr))
            {
                con.Open();

                MySqlCommand comm = new MySqlCommand();
                comm.CommandType = System.Data.CommandType.Text;
                comm.Connection = con;
                comm.CommandText = "insert into  title(`Id`,`text`)values(null,@text)";
                comm.Parameters.Add(pardetail);
                comm.ExecuteNonQuery();
            }
            return View();
        }
        public ActionResult Error()
        {
            return View();
        }
    }

}