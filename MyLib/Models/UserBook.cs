using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyLib.Models
{
    public class UserBook
    {
        [Key]
        public int Id { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }

        public int? BookId { get; set; }
        public Book Book { get; set; }

        public bool OnShelf { get; set; }
        public bool Readed { get; set; }
        public bool Desired { get; set; }

        public Debtor Debtor { get; set; }
        public Note Note { get; set; }

        public UserBook(User user, Book book)
        {
            User = user;
            Book = book;
            OnShelf = false;
            Readed = false;
            Desired = false;
        }
        public UserBook()
        {
            OnShelf = false;
            Readed = false;
            Desired = false;
        }
    }
}