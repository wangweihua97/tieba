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
    public class AdminController : AdminDefault1Controller
    {
        string mysqlconnstr = "server=127.0.0.1;user id=root;database=lydb;password=w348031705;sslmode=None;charset=utf8";
        // GET: Admin
        public ActionResult Index(string password)
        {
            if (password == "wc19980624")
            {
                Session["id"] = "-1";
                Session["name"] = "admin";
                return View();
            }
            return View();
        }
        public ActionResult Charge()
        {
            if (Session["id"].ToString() != "-1" || Session["name"].ToString() != "admin")
            {
                return RedirectToAction("Index", "Login");
            }
            MySqlConnection conn = new MySqlConnection(mysqlconnstr);
            conn.Open();
            string sqlstr = "select * from user";
            MySqlCommand cmd = new MySqlCommand(sqlstr, conn);
            MySqlDataAdapter comm = new MySqlDataAdapter(cmd);
            System.Data.DataSet ds = new System.Data.DataSet();
            comm.Fill(ds);
            System.Data.DataTable dt = ds.Tables[0];
            int cout = dt.Rows.Count;
            List<AUser> list = new List<AUser>();
            Models.AUser us = null;
            for (int i = 0; i < cout; i++)
            {
                us = new AUser()
                {
                    id = Convert.ToInt32(dt.Rows[i]["id"].ToString()),
                    uname = dt.Rows[i]["name"].ToString(),
                    username = dt.Rows[i]["username"].ToString(),
                    password = dt.Rows[i]["password"].ToString(),
                    brith = dt.Rows[i]["brith"].ToString(),
                    sex = dt.Rows[i]["sex"].ToString()
                };
                list.Add(us);

            }
            ViewData["data"] = list;
            conn.Close();
            return View();
        }
        public ActionResult Delect(int id,string name)
        {
            MySqlParameter parid = new MySqlParameter("@id", MySqlDbType.Int32);
            parid.Value = id;
            int k = 0;
            using (MySqlConnection conn = new MySqlConnection(mysqlconnstr))
            {
                conn.Open();
                MySqlCommand comm = new MySqlCommand();
                comm.CommandType = System.Data.CommandType.Text;
                comm.Connection = conn;
                comm.CommandText = "delete from user where id=@id";
                comm.Parameters.Add(parid);
                k = comm.ExecuteNonQuery();
                comm = new MySqlCommand();
                comm.CommandType = System.Data.CommandType.Text;
                comm.Connection = conn;
                comm.CommandText = "delete comments from comments,title where title.userId=@userId and title.id=comments.sub_id;";
                MySqlParameter p2 = new MySqlParameter("@userId", MySqlDbType.Int32);
                p2.Value = id;
                comm.Parameters.Add(p2);
                comm.ExecuteNonQuery();
                comm = new MySqlCommand();
                comm.CommandType = System.Data.CommandType.Text;
                comm.Connection = conn;
                comm.CommandText = "delete from title where userId=@userId";
                MySqlParameter p1 = new MySqlParameter("@userId", MySqlDbType.Int32);
                p1.Value = id;
                comm.Parameters.Add(p1);
                comm.ExecuteNonQuery();
                
            }
            if (k == 0)
            {
                Response.Write("<script>alert('删除失败');</script>");
                return View();
            }
            return View();
        }
        public ActionResult Find(string txt,string type)
        {
            string sqlstr = "";
            MySqlConnection conn = new MySqlConnection(mysqlconnstr);
            MySqlParameter p = new MySqlParameter("@p", MySqlDbType.VarChar, 20);
            conn.Open();
            if (type == "uname")
            {
                sqlstr = "select * from user where name like @p";
                p.Value = "%" + txt + "%";
            }
            if (type == "username")
            {
                sqlstr = "select * from user where username = @p";
                p.Value = txt;
            }
            MySqlCommand cmd = new MySqlCommand(sqlstr, conn);
            cmd.Parameters.Add(p);
            MySqlDataAdapter comm = new MySqlDataAdapter(cmd);
            System.Data.DataSet ds = new System.Data.DataSet();
            comm.Fill(ds);
            System.Data.DataTable dt = ds.Tables[0];
            int cout = dt.Rows.Count;
            List<AUser> list = new List<AUser>();
            Models.AUser us = null;
            for (int i = 0; i < cout; i++)
            {
                us = new AUser()
                {
                    id = Convert.ToInt32(dt.Rows[i]["id"].ToString()),
                    uname = dt.Rows[i]["name"].ToString(),
                    username = dt.Rows[i]["username"].ToString(),
                    password = dt.Rows[i]["password"].ToString(),
                    brith = dt.Rows[i]["brith"].ToString(),
                    sex = dt.Rows[i]["sex"].ToString()
                };
                list.Add(us);

            }
            ViewData["data"] = list;
            conn.Close();
            return View();
        }
        [HttpPost]
        public ActionResult Change(AUser cu)
        {
            string s = cu.brith;
            string[] sArray1 = s.Split(new char[3] { '-', '/',' '});
            string bt= sArray1[0]+"-"+sArray1[1]+"-"+sArray1[2];
            int k = 0;
            using (MySqlConnection conn = new MySqlConnection(mysqlconnstr))
            {
                conn.Open();
                MySqlCommand comm = new MySqlCommand();
                comm.CommandType = System.Data.CommandType.Text;
                comm.Connection = conn;
               
                comm.CommandText = "UPDATE user SET name = @uname, username = @username,password=@password,brith=@brith,sex=@sex WHERE id = @id";
                MySqlParameter parid = new MySqlParameter("@id", MySqlDbType.Int32);
                parid.Value = cu.id;
                MySqlParameter uname = new MySqlParameter("@uname", MySqlDbType.VarChar,20);
                uname.Value = cu.uname;
                MySqlParameter username = new MySqlParameter("@username", MySqlDbType.VarChar, 20);
                username.Value = cu.username;
                MySqlParameter password = new MySqlParameter("@password", MySqlDbType.VarChar, 20);
                password.Value = cu.password;
                MySqlParameter brith = new MySqlParameter("@brith", MySqlDbType.VarChar, 20);
                brith.Value = bt;
                MySqlParameter sex = new MySqlParameter("@sex", MySqlDbType.VarChar, 20);
                sex.Value = cu.sex;
                comm.Parameters.Add(parid);
                comm.Parameters.Add(uname);
                comm.Parameters.Add(username);
                comm.Parameters.Add(password);
                comm.Parameters.Add(brith);
                comm.Parameters.Add(sex);
                k = comm.ExecuteNonQuery();
            }
            if (k == 0)
            {
                Response.Write("<script>alert('删除失败');</script>");
                return View();
            }
            return View();
        }
    }
}