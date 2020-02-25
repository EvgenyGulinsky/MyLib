using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyLib.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }

        public ICollection<UserBook> UserBooks { get; set; }

        public Book()
        {
            UserBooks = new List<UserBook>();
        }
    }
}