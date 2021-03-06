﻿
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
        LibContext db = new LibContext();

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
                db.Users.Add(user);
                db.SaveChanges();
                Session["Nickname"] = user.Nickname;
                Session["Id"] = db.Users.Where(u => u.Nickname == user.Nickname).FirstOrDefault().UserId;
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
            User user = db.Users.Where(u => u.Email == email).FirstOrDefault();
            if(user == null)
            {
                ViewBag.Massage = "Такой e-mail не зарегистрирован";
                return View();
            }else
            if (!user.Pass.Equals(pass))
            {
                ViewBag.Massage = "Неправильный пароль";
                return View();
            }
            Session["Nickname"] = user.Nickname;
            Session["Id"] = user.UserId;
            
            return Redirect("/Home/Index");
        }

        [HttpGet]
        public ActionResult EditProfile()
        {
            int id = int.Parse(Session["Id"].ToString());
            User user = db.Users.Where(u => u.UserId == id).FirstOrDefault();
            ViewBag.User = user;
            return View();
        }

        [HttpPost]
        public ActionResult EditProfile(string email, string nikname, string pass, string NewPass, string NewPass2)
        {
            User au = (User)Session["AuthorizedUser"];
            if (!au.Pass.Equals(pass))
            {
                ViewBag.Massage = "Неправильный пароль";
                return View();
            }
            else
            {
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