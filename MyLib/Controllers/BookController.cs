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
            return View();
        }
    }
}