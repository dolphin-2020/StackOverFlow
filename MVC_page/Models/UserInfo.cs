using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_page.Models
{
    public class UserInfo
    {

        public string Name { get; set; }

        public string Email { get; set; }

        public string password { get; set; }

    }
}