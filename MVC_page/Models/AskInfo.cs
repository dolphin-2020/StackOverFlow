using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_page.Models
{
    public class AskInfo
    {
        public int Id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public DateTime createtime { get; set; }
        public string picturepath { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string headImgpath { get; set; }

    }
}