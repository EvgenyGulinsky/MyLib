using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyLib.Models
{
    public class FinishedBooks
    {
        [ForeignKey("User")]
        public int Id { get; set; }
        public virtual User User { get; set; }

        public ICollection<Book> finishedBooks { get; set; }

        public FinishedBooks()
        { finishedBooks = new List<Book>(); }
    }
}