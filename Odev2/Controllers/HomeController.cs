using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Odev2.Models;


namespace Odev2.Controllers
{
    public class HomeController : Controller
    {
        private WebProjeGuncelEntities2 db = new WebProjeGuncelEntities2();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        //Contact

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UrunEkle(FormCollection form)
        {
            urunler model = new urunler();
            model.UrunAdi = form["urun"].Trim();
            model.Adet = form["adet"];
            model.Fiyat = form["fiyat"];
            db.urunler.Add(model);
            db.SaveChanges();
            return View();
        }

        public ActionResult UrunEkle()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Contact(FormCollection form)
        {
            contact model = new contact();
            model.name = form["name"].Trim();
            model.mail = form["email"].Trim();
            model.message = form["Message"].Trim();
            db.contact.Add(model);
            db.SaveChanges();
            return View();
        }

        public ActionResult Giris()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Giris(users model)
        {
            var kullanici = db.users.FirstOrDefault(x => x.user_name == model.user_name && x.user_pass == model.user_pass);
            if (kullanici != null)
            {
                Session["KULLANICIADI"] = kullanici.user_name;
                if (kullanici.user_rank == 1)
                {
                    Session["KULLANICIRANK"] = "admin";
                }
                else {
                    Session["KULLANICIRANK"] = "kullanıcı";
                }
                return RedirectToAction("Index", "Home");
            }
            ViewBag.hata = "kullanıcı adı yada şifre yanlış";
            return View();
        }

        [HttpPost]
        public ActionResult InsertUser(FormCollection form)
        {
            users model2 = new users();
            model2.user_name = form["name"].Trim();
            model2.user_mail = form["mail"].Trim();
            model2.user_pass = form["pass"].Trim();
            model2.user_rank = 0;
            db.users.Add(model2);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            Session.Remove("KULLANICIADI");
            Session.Remove("KULLANICIRANK");
            return RedirectToAction("Index", "Home");
        }

    }
}