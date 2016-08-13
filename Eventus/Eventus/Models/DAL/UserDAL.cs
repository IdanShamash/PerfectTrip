using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eventus.Models.DAL
{
    public class User
    {
        public int ID { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }

        public void RegisterUser() { }
    }
}