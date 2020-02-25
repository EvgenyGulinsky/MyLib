using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyLib.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public string Nickname { get; set; }

        public ICollection<UserBook> UserBooks { get; set; }

        public User()
        {
            UserBooks = new List<UserBook>();
        }
    }
}