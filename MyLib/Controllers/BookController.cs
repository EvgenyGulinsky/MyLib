using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            IEnumerable<Book> Books = db.Books.Include("UserBook");
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
            if (db.Books.Where(b => b.Name == book.Name).FirstOrDefault() != null)
            {
                ViewBag.Massage = "Книга с таким названием уже существует";
                return View();
            }
            else if (state == null)
            {
                ViewBag.Massage = "Вы не выбрали сотояние книги на момент внесения в базу";
                return View();
            }
            db.Books.Add(book);
            db.SaveChanges();

            int id = int.Parse(Session["Id"].ToString());
            User user = db.Users.Where(u => u.UserId == id).FirstOrDefault();

            UserBook ub = new UserBook(user, book);

            if (state.Contains("IsHave")) ub.OnShelf = true;
            if (state.Contains("Readed")) ub.Readed = true;
            if (state.Contains("Desired")) ub.Desired = true;

            db.UserBooks.Add(ub);
            db.SaveChanges();
            return Redirect("/Book/Bookshelf");
        }

        [HttpGet]
        public ActionResult Bookshelf()
        {
            int id = int.Parse(Session["Id"].ToString());
            List<UserBook> books = db.UserBooks.Where(ub => ub.UserId == id).
                                    Union(db.UserBooks.Where(ub => ub.Readed == true).
                                    Include("Book").
                                    Include("Note").
                                    Include("Debtor")).
                                    ToList();
            ViewBag.Books = books;
            return View();
        }

        [HttpGet]
        public ActionResult Finished()
        {
            int id = int.Parse(Session["Id"].ToString());
            List<UserBook> books = db.UserBooks.Where(ub => ub.UserId == id).
                                    Union(db.UserBooks.Where(ub => ub.Readed == true).
                                    Include("Book").
                                    Include("Note").
                                    Include("Debtor")).
                                    ToList();
            ViewBag.Books = books;
            return View();
        }

        [HttpGet]
        public ActionResult Lent()
        {
            int id = int.Parse(Session["Id"].ToString());
            List<UserBook> books = new List<UserBook>();
            foreach(UserBook ub in db.UserBooks.Where(ub => ub.UserId == id).Include("Debtor"))
            {
                if (ub.Debtor != null) books.Add(ub);
            }
            ViewBag.Books = books;
            return View();
        }

        [HttpGet]
        public ActionResult Desired()
        {
            int id = int.Parse(Session["Id"].ToString());
            List<UserBook> books = new List<UserBook>();
            foreach (UserBook ub in db.UserBooks.Where(ub => ub.UserId == id).Include("Debtor"))
            {
                if (ub.Desired) books.Add(ub);
            }
            ViewBag.Books = books;
            return View();
        }
    }
}