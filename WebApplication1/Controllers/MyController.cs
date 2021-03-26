using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc;
using WebApplication1.Models;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace WebApplication1.Controllers
{
    public class MyController : Controller
    {
        string mysqlconnstr = "server=127.0.0.1;user id=root;database=lydb;password=w348031705;sslmode=None;charset=utf8";
        // GET: My
        public ActionResult Index()
        {            
            if (Session["id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            string uid = Session["id"].ToString();
            MySqlConnection conn = new MySqlConnection(mysqlconnstr);
            conn.Open();
            string sqlstr = "select * from title where userId="+ uid;
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
                ly = new Lytable()
                {
                    id = Convert.ToInt32(dt.Rows[i]["Id"].ToString()),
                    time = dt.Rows[i]["time"].ToString(),
                    head_txt = dt.Rows[i]["head_txt"].ToString()
                };
                list.Add(ly);
            }
            ViewData["title_data"] = list;
            string sqlstr2 = "select * from comments where username=@username";
            MySqlCommand cmd2 = new MySqlCommand(sqlstr2, conn);
            MySqlParameter un = new MySqlParameter("@username", MySqlDbType.VarChar, 255);
            un.Value = Session["name"].ToString();
            cmd2.Parameters.Add(un);
            MySqlDataAdapter comm2 = new MySqlDataAdapter(cmd2);
            System.Data.DataSet ds2 = new System.Data.DataSet();
            comm2.Fill(ds2);
            System.Data.DataTable dt2 = ds2.Tables[0];
            int cout2 = dt2.Rows.Count;
            List<Del_cm> list2 = new List<Del_cm>();
            Models.Del_cm dc = null;
            for (int i = 0; i < cout2; i++)
            {
                dc = new Del_cm()
                {
                    id = Convert.ToInt32(dt2.Rows[i]["id"].ToString()),
                    time = dt2.Rows[i]["time"].ToString(),
                    tid = Convert.ToInt32(dt2.Rows[i]["sub_id"].ToString()),
                    txt = dt2.Rows[i]["txt"].ToString(),
                    sub2_id = dt2.Rows[i]["sub2_id"].ToString(),
                    rep = dt2.Rows[i]["reply_username"].ToString()
                };
                list2.Add(dc);
            }
            ViewData["comment_data"] = list2;
            return View();
        }
        public ActionResult Delect_title(int id)
        {
            int k = 0,n=0;
            MySqlParameter parid = new MySqlParameter("@id", MySqlDbType.Int32);
            parid.Value = id;
            using (MySqlConnection conn = new MySqlConnection(mysqlconnstr))
            {
                conn.Open();
                MySqlCommand comm = new MySqlCommand();
                comm.CommandType = System.Data.CommandType.Text;
                comm.Connection = conn;
                comm.CommandText = "delete from title where id=@id";
                comm.Parameters.Add(parid);
                k=comm.ExecuteNonQuery();
                MySqlCommand comm2 = new MySqlCommand();
                comm2.CommandType = System.Data.CommandType.Text;
                comm2.Connection = conn;
                comm2.CommandText = "delete from comments where sub_id=@id2";
                MySqlParameter parid2 = new MySqlParameter("@id2", MySqlDbType.Int32);
                parid2.Value = id;
                comm2.Parameters.Add(parid2);
                n = comm2.ExecuteNonQuery();
            }
            if(k>=1&&n>=0)
            {
                return RedirectToAction("Index", "My");
            }
            else
                Response.Write("<script>alert('删除失败');</script>");
            return View();
        }
        public ActionResult Delect_comment(int id,int sub2_id,string rep_n)
        {
            int k = 0;
           
            using (MySqlConnection conn = new MySqlConnection(mysqlconnstr))
            {
                conn.Open();
                MySqlCommand comm = new MySqlCommand();
                if(rep_n=="*")
                {
                    comm.CommandType = System.Data.CommandType.Text;
                    comm.Connection = conn;
                    comm.CommandText = "delete from comments where sub2_id=@id";
                    MySqlParameter parid = new MySqlParameter("@id", MySqlDbType.Int32);
                    parid.Value = sub2_id;
                    comm.Parameters.Add(parid);
                    k = comm.ExecuteNonQuery();
                }
                else if(rep_n == "-")
                {
                    comm.CommandType = System.Data.CommandType.Text;
                    comm.Connection = conn;
                    comm.CommandText = "delete from comments where id=@id";
                    MySqlParameter parid = new MySqlParameter("@id", MySqlDbType.Int32);
                    parid.Value = id;
                   
                    comm.Parameters.Add(parid);
                  
                    k = comm.ExecuteNonQuery();
                    comm = new MySqlCommand();
                   
                        comm.CommandType = System.Data.CommandType.Text;
                        comm.Connection = conn;
                        comm.CommandText = "delete from comments where sub2_id = @sub2_id and reply_username = @name";
                    MySqlParameter pars2 = new MySqlParameter("@sub2_id", MySqlDbType.VarChar, 30);
                    pars2.Value = sub2_id;
                    MySqlParameter parname = new MySqlParameter("@name", MySqlDbType.VarChar,30);
                    parname.Value = Session["name"].ToString();
                    comm.Parameters.Add(pars2);
                    comm.Parameters.Add(parname);
                        k += comm.ExecuteNonQuery();
                }
                else
                {
                    comm.CommandType = System.Data.CommandType.Text;
                    comm.Connection = conn;
                    comm.CommandText = "delete from comments where id=@id";
                    MySqlParameter parid = new MySqlParameter("@id", MySqlDbType.Int32);
                    parid.Value = id;
                    comm.Parameters.Add(parid);
                    k = comm.ExecuteNonQuery();
                }
              
            }
            if (k >= 1)
            {
                return RedirectToAction("Index", "My");
            }
            else
                Response.Write("<script>alert('删除失败');</script>");
            return View();
        }
    }
}