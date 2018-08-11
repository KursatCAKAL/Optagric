using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OptagricDataLayer;



namespace Optagric_Management_Portal.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            TempData["GirisHatasi"] = "";
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Session.Abandon();
            Session.Clear();
            return View();
        }
        public ActionResult Roter(string SendedEmail, string SendedPassword)
        {
            OptagricEntities DB = new OptagricEntities();
            List<Administration> yetkililer = DB.Administration.ToList();
            //Administration nesne = yetkililer.Where(x=>x.Password==SendedPassword && x.Email==SendedEmail).First();

            if (!yetkililer.Count(x => x.Password == SendedPassword && x.Email == SendedEmail).Equals(0))
            {
                Administration nesne = yetkililer.Where(x => x.Password == SendedPassword && x.Email == SendedEmail).First();
                return RedirectToAction("IndexAdminCapsulation", "Home", new { administrationGelecekEmail = nesne.Email.ToString(), administrationGelecekPassword = nesne.Password.ToString() });
            }
            else
            {

                TempData["GirisHatasi"] = "Kullanıcı ID veya PW yanlış girildi.";
                return View("Index");
            }

        }

        public ActionResult UserRoter(string SendedUserEmail, string SendedUserPassword)
        {
            OptagricEntities DB = new OptagricEntities();
            try
            {
                Customer girisYapanKullanici = DB.Customer.Where(x => x.Email == SendedUserEmail && x.Password == SendedUserPassword).First();

                return RedirectToAction("IndexCustomerCapsulation", "Home", new { userGelecekEmail = SendedUserEmail, userGelecekPassword = SendedUserPassword });

            }
            catch (Exception)
            {

                TempData["GirisHatasiKullanici"] = "Kullanıcı ID veya PW yanlış girildi.";
                return View("Index");
            }


        }

        public ActionResult Exit()
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Session.Abandon();
            Session.Clear();
            TempData["Exit"] = "Cıkıs Basarili";
            return RedirectToAction("Index");
        }
    }
}