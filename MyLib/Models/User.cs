using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public virtual Bookshelf BooksFromTheShelf { get; set; }
        public virtual DesiredBooks DesiredBooks { get; set; }
        public virtual FinishedBooks FinishedBooks { get; set; } 
        
        public ICollection<LentBooks> LentBooks { get; set; }
        public User() { LentBooks = new List<LentBooks>(); }
    }
}