using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyLib.Models
{
    public class Book
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int BookId { get; set; }
        [DisplayName("Название")]
        public string Name { get; set; }
        [DisplayName("Автор")]
        public string Author { get; set; }
        [DisplayName("Описание")]
        public string Note { get; set; }

        public ICollection<LentBooks> LentBooks { get; set; }
        public Book() { LentBooks = new List<LentBooks>(); }
    }
}