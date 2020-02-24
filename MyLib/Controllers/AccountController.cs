using MyLib.Models;
using MyLib.Models.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyLib.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(User user)
        {
            if (user != null)
            {
                LibContext db = new LibContext();
                user.BooksFromTheShelf = new Bookshelf();
                user.FinishedBooks = new FinishedBooks();
                user.DesiredBooks = new DesiredBooks();                
                db.Users.Add(user);
                db.SaveChanges();
                Session["AuthorizedUser"] = db.Users.Last();
            }
            return Redirect("/Home/Index");
        }

        [HttpGet]
        public ActionResult Authorization()
        {
            ViewBag.Massage = "";
            return View();
        }

        [HttpPost]
        public ActionResult Authorization(string email, string pass)
        {
            LibContext db = new LibContext();
            try
            {
                User user = (User)db.Users.Where(u => u.Email.Equals(email)).First();
                if(!user.Pass.Equals(pass))
                {
                    ViewBag.Massage = "Неправильный пароль";
                    return View();
                }
                Session["AuthorizedUser"] = user;
            }
            catch(Exception e)
            {
                ViewBag.Massage = "Такой e-mail не зарегистрирован";
                return View();
            }
            return Redirect("/Home/Index");
        }

        [HttpGet]
        public ActionResult EditProfile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EditProfile(string email, string nikname, string pass, string NewPass, string NewPass2)
        {
            User au = (User)Session["AuthorizedUser"];
            if(!au.Pass.Equals(pass))
            {
                ViewBag.Massage = "Неправильный пароль";
                return View();
            } else
            {
                LibContext db = new LibContext();
                if (NewPass != null && NewPass2 != null)
                    if (NewPass.Equals(NewPass2))
                    {
                        au.Pass = NewPass;
                        au.Nickname = nikname;
                        au.Email = email;                        
                        db.Entry(au).State = EntityState.Modified;
                        db.SaveChanges();
                        return Redirect("/Home/Index");
                    }
                    else
                    {
                        ViewBag.Massage = "Не удалось утвердить новый пароль";

                        return View();
                    }
                au.Nickname = nikname;
                au.Email = email;
                db.Entry(au).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("/Home/Index");
            }
        }
    }
}