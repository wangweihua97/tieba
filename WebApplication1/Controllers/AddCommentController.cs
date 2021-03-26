using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;

namespace WebApplication1.Controllers
{
    public class AddCommentController : DefaultController
    {
        string mysqlconnstr = "server=127.0.0.1;user id=root;database=lydb;password=w348031705;sslmode=None;charset=utf8";
        // GET: AddComment
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Add_comment ac)
        {
            if(ac.txt==""||ac.txt.Length<=5)
            {
                Response.Write("<script>alert('输入过短');</script>");
                ViewData["tid"] = ac.tid;
                return View();
            }
            MySqlConnection conn = new MySqlConnection(mysqlconnstr);
            conn.Open();
            string sqlstr = "select * from comments where sub_id="+ac.tid+ " order by sub2_id";
            MySqlCommand cmd = new MySqlCommand(sqlstr, conn);
            MySqlDataAdapter comm = new MySqlDataAdapter(cmd);
            System.Data.DataSet ds = new System.Data.DataSet();
            comm.Fill(ds);
            System.Data.DataTable dt = ds.Tables[0];
            int cout = dt.Rows.Count;
          
                string sql = "insert into  comments(sub_id,sub2_id,reply_username,time,username,txt,img_path)values(@sub_id,@sub2_id,@reply_username,@time,@username,@txt,@img_path)";
                cmd = new MySqlCommand(sql, conn);
                MySqlParameter p1 = new MySqlParameter("@sub_id", MySqlDbType.VarChar, 255);
                p1.Value = ac.tid;
                MySqlParameter p2 = new MySqlParameter("@sub2_id", MySqlDbType.VarChar, 255);
            if(cout==0)
                p2.Value = 1.ToString();
            else if(ac.rid2=="*")
            {
                int k2 = Convert.ToInt32(dt.Rows[cout-1]["sub2_id"].ToString()) + 1;
                p2.Value = k2.ToString();
            }
            else
            {
                p2.Value = ac.rid;
            }
                MySqlParameter p3 = new MySqlParameter("@reply_username", MySqlDbType.VarChar, 255);
                p3.Value = ac.rid2;
                MySqlParameter p4 = new MySqlParameter("@time", MySqlDbType.VarChar, 255);
                p4.Value = DateTime.Now.ToString(); ;
                MySqlParameter p5 = new MySqlParameter("@username", MySqlDbType.VarChar, 255);
                p5.Value = Session["name"];
                MySqlParameter p6 = new MySqlParameter("@txt", MySqlDbType.VarChar, 255);
                p6.Value = ac.txt;
                MySqlParameter p7 = new MySqlParameter("@img_path", MySqlDbType.VarChar, 255);
                p7.Value = Session["img"];
                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);
                cmd.Parameters.Add(p3);
                cmd.Parameters.Add(p4);
                cmd.Parameters.Add(p5);
                cmd.Parameters.Add(p6);
                cmd.Parameters.Add(p7);
            int  k = cmd.ExecuteNonQuery();
                if(k==0)
                {
                    Response.Write("<script>alert('评论失败');</script>");
                ViewData["tid"] = ac.tid;
                return View();
                }
                else
                {
                    Response.Write("<script>alert('评论成功');</script>");
                    return RedirectToAction("Index", "Comment", new {  ac.tid });
                }           
        }
    }
}