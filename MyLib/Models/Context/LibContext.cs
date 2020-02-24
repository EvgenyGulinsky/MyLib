using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyLib.Models.Context
{
    public class LibContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Bookshelf> Bookshelves { get; set; }
        public DbSet<DesiredBooks> DesiredBooks { get; set; }
        public DbSet<FinishedBooks> FinishedBooks { get; set; }
        public DbSet<LentBooks> LentBooks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserNotes> UserNotes { get; set; }
    }
}