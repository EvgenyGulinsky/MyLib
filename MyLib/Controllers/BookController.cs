using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyLib.Models;
using MyLib.Models.Context;

namespace MyLib.Controllers
{
    public class BookController : Controller
    {
        LibContext db = new LibContext();

        [HttpGet]
        public ActionResult AllBooks()
        {
            IEnumerable<Book> Books = db.Books;
            ViewBag.Books = Books;
            return View();
        }

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.Massage = "";
            return View();
        }

        [HttpPost]
        public ActionResult Add(Book book, string[] state)
        {
            if(db.Books.Where(b => b.Name == book.Name).FirstOrDefault() != null)
            {
                ViewBag.Massage = "Книга с таким названием уже существует";
                return View();
            }
            else if(state == null)
            {
                ViewBag.Massage = "Вы не выбрали сотояние книги на момент внесения в базу";
                return View();
            }
            User user = (User)Session["AuthorizedUser"];
            if (state.Contains("IsHave")) user.BooksFromTheShelf.bookshelf.Add(book);
            if (state.Contains("Readed")) user.FinishedBooks.finishedBooks.Add(book);
            if (state.Contains("Desired")) user.DesiredBooks.desiredBooks.Add(book);
          
            db.Books.Add(book);
            db.SaveChanges();
            return Redirect("/Book/Bookshelf");
        }

        [HttpGet]
        public ActionResult Bookshelf()
        {
            User user = (User)Session["AuthorizedUser"];
            List<BookDisplay> books = new List<BookDisplay>();
            foreach (Book book in db.Books.ToList())
            {
                BookDisplay bd = new BookDisplay();
                if (user.BooksFromTheShelf.bookshelf.Contains(book)) { bd.Book = book; bd.InHave = true; }
                if (user.FinishedBooks.finishedBooks.Contains(book)) { bd.Book = book; bd.Readed = true; }
                foreach (LentBooks lb in user.LentBooks)
                {
                    if (lb.Book == book)
                    {
                        bd.LentBook = lb;
                        break;
                    }
                }
                if (bd.Book != null) books.Add(bd);
            }
            ViewBag.Books = books;
            return View();
        }
    }
}