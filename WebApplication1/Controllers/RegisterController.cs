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
using System.IO;
using Avatar.Helper;

namespace WebApplication1.Controllers
{
    public class RegisterController : Controller
    {
        string mysqlconnstr = "server=127.0.0.1;user id=root;database=lydb;password=w348031705;sslmode=None;charset=utf8;allowPublicKeyRetrieval=true";
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(User us)
        {
            string img_path = "";
           
            string erro = "";
            string username = us.username;
            string password = us.password;
            string brith = us.brith;
            string sex = us.sex;
            string uname = us.uname;
            bool jug = true;
            String reg = @"^[1-9]\d{5,10}$";
            if (username == null || !Regex.IsMatch(username, reg))
            {
                jug = false;
                erro = "用户名纯数字6-11位";
            }
            reg = @"^[a-zA-Z1-9]\d{5,19}$";
            if (password == null || !Regex.IsMatch(password, reg))
            {
                jug = false;
                erro = "密码数字和字母6-20位";
            }
            reg = @"^\w{2,8}$";
            if (uname == null || !Regex.IsMatch(uname, reg))
            {
                jug = false;
                erro = "呢称支持汉字2-8位";
            }
            reg = @"^\d{4}-\d{1,2}-\d{1,2}";
            if (brith == null || !Regex.IsMatch(brith, reg))
            {
                jug = false;
                erro = "生日YYYY-MM-DD";
            }
            if(sex!="男"&sex!="女")
                jug = false;
            if (jug != false)
            {
                foreach (var file in us.filename)
                {
                    if (file != null)
                        using (Stream inputStream = file.InputStream)
                        {
                            Random ran = new Random();
                            String path = Server.MapPath("/Upload/");
                            string fname = DateTime.Now.ToFileTime().ToString() + Convert.ToString(ran.Next(1, 8888)) + "." + file.ContentType.Split('/')[1];
                            //string fname = "test1." + file.ContentType.Split('/')[1];
                            String img = path + fname;
                            img_path = "/Upload/" + fname;
                            Console.WriteLine(img);
                            Console.WriteLine(img);
                            StreamToFile(inputStream, img);

                        }
                }
                /*
                    添加验证用户名密码代码
                */
                MySqlParameter p1 = new MySqlParameter("@username", MySqlDbType.VarChar, 255);
                p1.Value = username;
                MySqlParameter p2 = new MySqlParameter("@password", MySqlDbType.VarChar, 255);
                p2.Value = password;
                MySqlParameter p3 = new MySqlParameter("@name", MySqlDbType.VarChar, 255);
                p3.Value = uname;
                MySqlParameter p4 = new MySqlParameter("@brith", MySqlDbType.VarChar, 255);
                p4.Value = brith;
                MySqlParameter p5 = new MySqlParameter("@sex", MySqlDbType.VarChar, 255);
                p5.Value = sex;
                MySqlParameter p6 = new MySqlParameter("@img_path", MySqlDbType.VarChar, 255);
                p6.Value = img_path;
                int k = 0;
                using (MySqlConnection con = new MySqlConnection(mysqlconnstr))
                {
                    con.Open();
                    MySqlCommand comm = new MySqlCommand();
                    comm.CommandType = System.Data.CommandType.Text;
                    comm.Connection = con;
                    comm.CommandText = "insert into  user(username,name,password,brith,sex,img_path)values(@username,@name,@password,@brith,@sex,@img_path)";
                    comm.Parameters.Add(p1);
                    comm.Parameters.Add(p2);
                    comm.Parameters.Add(p3);
                    comm.Parameters.Add(p4);
                    comm.Parameters.Add(p5);
                    comm.Parameters.Add(p6);
                    try
                    {
                        k = comm.ExecuteNonQuery();
                    }
                    catch(Exception e)
                    {

                    }
                }
                if (k != 0)
                {
                   
                    return RedirectToAction("Index", "Login");                    
                }
                else
                {
                    Response.Write("<script>alert('注册失败!用户名或者呢称重复');</script>");
                   
                    return View();
                }                               
            }
            else
            {
                /*返回序列话的Json数据
                var serializer = new JavaScriptSerializer();
                return Content(serializer.Serialize(us));*/
                Response.Write("<script>alert('注册失败!" + erro + "');</script>");
                return View();
            }

        }
        
        [HttpPost]
        public ActionResult Index3(User us2)
        {
            User my = new User();
            try
            {
                //该方法可以动态的获取前台传过来的值，比如前台有编辑功能，单机保存调用Edit方法时，这是调用该方法可以获取到更改后的值。
                //使用Controller 基类的内置方法UpdateModel()。该方法支持使用传入的表单参数更新
                //对象的属性，它使用反射机制来解析对象的属性名称，接着基于客户端传入的参数值自动赋值给对象相

                this.UpdateModel(my);//这个是UpdateModel的用法。
                string name = my.uname;
            }
            catch (Exception ex)
            {
                return null;
            }
            Response.Write("<script>alert('注册格式错误!');</script>");
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return Json(jss.Serialize(new { Name = my.uname, Message = my.sex }), JsonRequestBehavior.AllowGet);
        
        }
        [HttpPost]
        public ActionResult Upload(IEnumerable<HttpPostedFileBase> filename)
        {

            foreach (var file in filename)
            {
                using (Stream inputStream = file.InputStream)
                {
                    String path = Server.MapPath("/Upload/");
                    String img = path + "test." + file.ContentType.Split('/')[1];
                    StreamToFile(inputStream, img);

                }
            }
            return View("Index");
        }
        public  void StreamToFile(Stream stream, string fileName)
        {
            // 把 Stream 转换成 byte[]   
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始   
            stream.Seek(0, SeekOrigin.Begin);

            // 把 byte[] 写入文件   
            FileStream fs = new FileStream(fileName, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(bytes);
            bw.Close();
            fs.Close();
        }

    }
}