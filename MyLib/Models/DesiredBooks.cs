using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyLib.Models
{
    public class DesiredBooks
    {
        [ForeignKey("User")]
        public int Id { get; set; }
        public virtual User User { get; set; }

        public ICollection<Book> desiredBooks { get; set; }

        public DesiredBooks()
        { desiredBooks = new List<Book>(); }
    }
}