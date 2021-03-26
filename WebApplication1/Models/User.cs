using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class User
    {
        public string uname
        {
            set; get;
        }
        public string username
        {
            set; get; 
        }
        public string password
        {
            set; get; 
        }
        
        public string brith
        {
            set; get; 
        }
        public string sex
        {
            set; get; 
        }
        public IEnumerable<HttpPostedFileBase> filename
        {
            set; get;
        }


    }
}