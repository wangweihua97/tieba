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
    public class CommentController : DefaultController
    {
        string mysqlconnstr = "server=127.0.0.1;user id=root;database=lydb;password=w348031705" +
            ";sslmode=None;charset=utf8";
        // GET: Comment
        public ActionResult Index(string tid)
        {
            if (tid == null)
                return RedirectToAction("Index", "Home");
            if (Session["id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            MySqlConnection conn = new MySqlConnection(mysqlconnstr);
            conn.Open();
            string sqlstr = "select * from comments where sub_id=@sub_id order by sub2_id,id";
            MySqlCommand cmd = new MySqlCommand(sqlstr, conn);
            MySqlParameter p1 = new MySqlParameter("@sub_id", MySqlDbType.Int32);
            p1.Value = Convert.ToInt32(tid);
            cmd.Parameters.Add(p1);
            MySqlDataAdapter comm = new MySqlDataAdapter(cmd);
            System.Data.DataSet ds = new System.Data.DataSet();
            comm.Fill(ds);
            System.Data.DataTable dt = ds.Tables[0];
            int cout = dt.Rows.Count;
            List<Comment> list = new List<Comment>();
            Models.Comment ly = null;
            for (int i = 0; i < cout; i++)
            {
                string imgpath = dt.Rows[i]["img_path"].ToString();
                if (imgpath == "")
                    imgpath = "/Upload/null.jpg";
                ly = new Comment()
                {
                    time = dt.Rows[i]["time"].ToString(),
                    txt = dt.Rows[i]["txt"].ToString(),
                    name = dt.Rows[i]["username"].ToString(),
                    re1 = Convert.ToInt32(dt.Rows[i]["sub2_id"].ToString()),
                    re2 = dt.Rows[i]["reply_username"].ToString(),
                    img = imgpath
                };
                list.Add(ly);
            }
            MySqlCommand cmd2 = new MySqlCommand("select head_txt,text from title where id = @sub_id2", conn);
            MySqlParameter p2 = new MySqlParameter("@sub_id2", MySqlDbType.Int32);
            p2.Value = tid;
            cmd2.Parameters.Add(p2);
            MySqlDataAdapter comm2 = new MySqlDataAdapter(cmd2);
            System.Data.DataSet ds2 = new System.Data.DataSet();
            comm2.Fill(ds2);
            System.Data.DataTable dt2 = ds2.Tables[0];
            ViewData["tid"] = tid;
            ViewData["head"] = dt2.Rows[0]["head_txt"].ToString();
            ViewData["txt"]= dt2.Rows[0]["text"].ToString();
            ViewData["data"] = list;
            conn.Close();
            return View();
        }

    }
}