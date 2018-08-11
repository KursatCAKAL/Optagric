using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OptagricDataLayer;

namespace Optagric_Management_Portal.Controllers
{
    public class CustomerPanel_CRUDController : Controller
    {
        // GET: CustomerPanel_CRUD
        public ActionResult PersonelInfo_Update(string inputName, string inputSurname, string inputEmail, string currentPassword, string newPassword, string newPasswordVerification)
        {
            if (Session["KisiTanimlayici"].ToString() == "Admin")
            {
                TempData["SecurityWall_Home"] = "Güvenlik Duvarına Takıldınız.";
                return RedirectToAction("Index", "Login");

            }
            else
            {
                OptagricEntities DB = new OptagricEntities();

                Guid temp = new Guid(Session["CurrentUserGuid"].ToString());
                ViewBag.currentCustomer = DB.Customer.Where(x => x.UID == temp).First();
                string tempedPassword = ViewBag.currentCustomer.Password;
                string tempedName = ViewBag.currentCustomer.Name;
                string tempedSurname = ViewBag.currentCustomer.Surname;
                string tempedEmail = ViewBag.currentCustomer.Email;
                Session["KisiTanimlayici"] = "Customer";

                if (inputName != null || inputSurname != null || inputEmail != null || currentPassword != null || newPassword != null || newPasswordVerification != null)
                {
                    Customer kullanici = ViewBag.currentCustomer;

                    if (currentPassword != null && newPassword != null && newPasswordVerification != null && currentPassword != "" && newPassword != "" && newPasswordVerification != "")//Password Changer Control
                    {
                        kullanici = ViewBag.currentCustomer;
                        if (kullanici.Password == currentPassword && newPassword == newPasswordVerification && newPassword != null)
                        {
                            kullanici.Password = newPassword;
                        }

                        DB.Customer.Attach(kullanici);
                        DB.Entry(kullanici).State = System.Data.Entity.EntityState.Modified;
                        DB.SaveChanges();
                        string p1 = this.ControllerContext.RouteData.Values["action"].ToString();
                        string p2 = this.ControllerContext.RouteData.Values["controller"].ToString();
                        CRUD_Logs(p1, Session["CurrentUserGuid"].ToString(), "UPDATE", tempedPassword + "-->" + kullanici.Password + " / " + p1 + " / " + p2);
                    }
                    if (inputName != null && inputName != "")//Name Changer Control
                    {

                        kullanici = ViewBag.currentCustomer;

                        kullanici.Name = inputName;

                        DB.Customer.Attach(kullanici);
                        DB.Entry(kullanici).State = System.Data.Entity.EntityState.Modified;
                        DB.SaveChanges();
                        string p1 = this.ControllerContext.RouteData.Values["action"].ToString();
                        string p2 = this.ControllerContext.RouteData.Values["controller"].ToString();
                        CRUD_Logs(p1, Session["CurrentUserGuid"].ToString(), "UPDATE", tempedName + "-->" + kullanici.Name + " / " + p1 + " / " + p2);
                    }
                    if (inputSurname != null && inputSurname != "")//Surname Changer Control
                    {

                        kullanici = ViewBag.currentCustomer;

                        kullanici.Surname = inputSurname;

                        DB.Customer.Attach(kullanici);
                        DB.Entry(kullanici).State = System.Data.Entity.EntityState.Modified;
                        DB.SaveChanges();
                        string p1 = this.ControllerContext.RouteData.Values["action"].ToString();
                        string p2 = this.ControllerContext.RouteData.Values["controller"].ToString();
                        CRUD_Logs(p1, Session["CurrentUserGuid"].ToString(), "UPDATE", tempedSurname + "-->" + kullanici.Surname + " / " + p1 + " / " + p2);
                    }
                    if (inputEmail != null && inputEmail != "")//Email Changer Control
                    {
                        kullanici = ViewBag.currentCustomer;

                        kullanici.Email = inputEmail;

                        DB.Customer.Attach(kullanici);
                        DB.Entry(kullanici).State = System.Data.Entity.EntityState.Modified;
                        DB.SaveChanges();
                        string p1 = this.ControllerContext.RouteData.Values["action"].ToString();
                        string p2 = this.ControllerContext.RouteData.Values["controller"].ToString();
                        CRUD_Logs(p1, Session["CurrentUserGuid"].ToString(), "UPDATE", tempedEmail + "-->" + kullanici.Email + " / " + p1 + " / " + p2);
                    }

                    TempData["Degislik"] = "Degisiklik Basarili";

                    ViewBag.currentAdmin = kullanici;
                    Session["UserEmail"] = ViewBag.currentCustomer.Email;
                    Session["UserPassword"] = ViewBag.currentCustomer.Password;
                    return View();
                }

                else
                {
                    TempData["Degislik"] = "Degisiklik Yapılmadi";
                    return View();
                }

                
            }
        }

        public ActionResult CRUD_Logs(string donulecekYer, string gelenAdminID, string gelenProcessType, string gelenModelName)
        {
            if (Session["KisiTanimlayici"].ToString() == "Admin")
            {
                TempData["SecurityWall_Home"] = "Güvenlik Duvarına Takıldınız.";
                return RedirectToAction("Index", "Login");
            }
            else
            {
                try
                {
                    if (gelenAdminID != null && gelenProcessType != null && gelenModelName != null)
                    {
                        OptagricEntities DB = new OptagricEntities();
                        Panel_CRUD_Process_Logs ekle = new Panel_CRUD_Process_Logs
                        {
                            CustomerID = new Guid(gelenAdminID.ToString()),
                            ProcessType = gelenProcessType,
                            ModelName = gelenModelName,
                            Date = DateTime.Now


                        };
                        DB.Panel_CRUD_Process_Logs.Add(ekle);
                        DB.SaveChanges();
                        TempData["LogSuccess"] = "Admin Log Ekleme Başarılı";
                        Session["KisiTanimlayici"] = "Customer";
                        return RedirectToAction(donulecekYer.ToString(), "AdminPanel_CRUD");
                    }
                    else
                    {
                        TempData["LogError"] = "Lütfen Değeleri Uygun Giriniz";
                        Session["KisiTanimlayici"] = "Customer";
                        return RedirectToAction(donulecekYer.ToString(), "AdminPanel_CRUD");
                    }
                }
                catch (Exception)
                {
                    TempData["LogError"] = "Lütfen Değeleri Uygun Giriniz CATCH";
                    return RedirectToAction(donulecekYer.ToString(), "AdminPanel_CRUD");
                }
            }
        }

    }
}