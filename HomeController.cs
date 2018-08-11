using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OptagricDataLayer;
namespace Optagric_Management_Portal.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult IndexAdmin(string activeUser)
        {
            //if (administrationGelecekEmail == null)//Giriş Yapılmamış İse
            //{
            //    TempData["SecurityWall_Home"] = "Güvenlik Duvarına Takıldınız";
            //    return RedirectToAction("Index", "Login");

            //}
            //else//Giriş Yapılmış İse
            //{

            //    Session["AdminEmail"]= administrationGelecekEmail;
            //    Session["AdminPassword"] = administrationGelecekPassword;
            //    return View();
            //}
            if (Session["AdminEmail"] == null || Session["AdminPassword"] == null)
            {
                TempData["SecurityWall_Home"] = "Güvenlik Duvarına Takıldınız";
                return RedirectToAction("Index", "Login");
            }

            else//Giriş Yapılmış İse
            {
                return View();
            }



        }
        public ActionResult IndexAdminCapsulation(string administrationGelecekEmail, string administrationGelecekPassword)
        {
            Session["KisiTanimlayici"] = "Admin";
            if (administrationGelecekEmail == null)//Giriş Yapılmamış İse
            {
                TempData["SecurityWall_Home"] = "Güvenlik Duvarına Takıldınız";
                return RedirectToAction("Index", "Login");

            }
            else//Giriş Yapılmış İse
            {

                Session["AdminEmail"] = administrationGelecekEmail.ToString();
                Session["AdminPassword"] = administrationGelecekPassword;

                OptagricEntities DB = new OptagricEntities();
                Administration girisYapanAdmin = DB.Administration.Where(x => x.Email == administrationGelecekEmail.ToString() && x.Password == administrationGelecekPassword.ToString()).First();
                Session["CurrentUserGuid"] = girisYapanAdmin.ID;
                //return View("Index");

                return RedirectToAction("IndexAdmin", new { activeUser = girisYapanAdmin.Name + " " + girisYapanAdmin.Surname });
            }
        }
        public ActionResult IndexCustomer(string activeUser)
        {
            if (Session["UserEmail"] == null || Session["UserPassword"] == null)
            {
                TempData["SecurityWall_Home"] = "Güvenlik Duvarına Takıldınız";
                return RedirectToAction("Index", "Login");
            }

            else//Giriş Yapılmış İse
            {
                OptagricEntities DB = new OptagricEntities();
                ViewBag.ControlFlagHumidity = DB.ProcessFlags.Where(x => x.ProcessName== "HumidityHardwareSignal").First();
                ViewBag.ControlFlagTemperature = DB.ProcessFlags.Where(x => x.ProcessName == "TemperatureHardwareSignal").First();

                Session["KisiTanimlayici"] = "Customer";
                return View();
            }
        }
        public ActionResult IndexCustomerCapsulation(string userGelecekEmail, string userGelecekPassword)
        {
            Session["KisiTanimlayici"] = "Customer";
            if (userGelecekEmail == null && userGelecekPassword == null)//Giriş Yapılmamış İse
            {
                TempData["SecurityWall_Home"] = "Güvenlik Duvarına Takıldınız";
                return RedirectToAction("Index", "Login");

            }
            else//Giriş Yapılmış İse
            {

                Session["UserEmail"] = userGelecekEmail;
                Session["UserPassword"] = userGelecekPassword;

                OptagricEntities DB = new OptagricEntities();
                Customer girisYapanCustomer = DB.Customer.Where(x => x.Email == userGelecekEmail.ToString() && x.Password == userGelecekPassword.ToString()).First();
                Session["CurrentUserGuid"] = girisYapanCustomer.UID;
                return RedirectToAction("IndexCustomer", new { activeUser = girisYapanCustomer.Name + " " + girisYapanCustomer.Surname });
            }
        }
        public ActionResult SideBar()
        {

            try
            {
                if (Session["KisiTanimlayici"].ToString() == "Admin")

                {
                    OptagricEntities DB = new OptagricEntities();
                    string temp1 = Session["AdminEmail"].ToString();
                    string temp2 = Session["AdminPassword"].ToString();
                    Administration girisYapanAdmin = DB.Administration.Where(x => x.Email == temp1 && x.Password == temp2).First();

                    return View(girisYapanAdmin);

                }
                else
                {
                    OptagricEntities DB = new OptagricEntities();
                    string temp1 = Session["UserEmail"].ToString();
                    string temp2 = Session["UserPassword"].ToString();
                    Customer girisYapanCustomer = DB.Customer.Where(x => x.Email == temp1 && x.Password == temp2).First();
                    return View(girisYapanCustomer);
                }
            }
            catch (Exception)
            {
                TempData["SecurityWall_Home"] = "Güvenlik Duvarına Takıldınız.";
                return RedirectToAction("Index", "Login");
            }
           
        }

    }
}