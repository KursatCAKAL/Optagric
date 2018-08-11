using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OptagricDataLayer;

namespace Optagric_Management_Portal.Controllers
{
    public class AdminPanel_CRUDController : Controller
    {
        // GET: AdminPanel_CRUD
        //CRUD_Loglama_Kullanımı
        /*
         
            string p1 = this.ControllerContext.RouteData.Values["action"].ToString();
            string p2 = this.ControllerContext.RouteData.Values["controller"].ToString();
            CRUD_Logs(p1, Session["CurrentUserGuid"].ToString(), "INSERT", eklenecekNesne.Name + " / " + p1 + " / " + p2);
             
        */
        //CRUD_Loglama_Kullanımı

        /*CRUDLogs olarak güncellendi.*/
        public void ActionLogs(string gelenAdminID, string gelenProcessType, string gelenModelName)
        {
            try
            {
                if (gelenAdminID != null && gelenProcessType != null && gelenModelName != null)
                {
                    OptagricEntities DB = new OptagricEntities();
                    Panel_CRUD_Process_Logs ekle = new Panel_CRUD_Process_Logs
                    {
                        AdminID = new Guid(gelenAdminID.ToString()),
                        ProcessType = gelenProcessType,
                        ModelName = gelenModelName,
                        Date = DateTime.Now


                    };
                    DB.Panel_CRUD_Process_Logs.Add(ekle);
                    DB.SaveChanges();
                    TempData["LogSuccess"] = "Log Ekleme Başarılı";

                }
                else
                {
                    TempData["LogError"] = "Lütfen Değeleri Uygun Giriniz";

                }
            }
            catch (Exception)
            {
                TempData["LogError"] = "Lütfen Değeleri Uygun Giriniz CATCH";

            }

        }
        public ActionResult SubRegion_ContentHandler(string inputSubRegionName, string selectedCropFieldID, string selectedCropTypeID, string inputCroppedProductAmount, string inputSoilWaterContent) //Statik olarak veri girdisi
        //Var olan bir tarlaya yeni bir ekim alanı eklersin
        //böylece yeni tahminler için Azure ML ' e girdi sağlamış olursun
        //bu girdiler eklenen her sensörün gönderdiği değerleri temsil etmektedir.
        {
            if (Session["KisiTanimlayici"].ToString() == "Customer")
            {
                TempData["SecurityWall_Home"] = "Güvenlik Duvarına Takıldınız.";
                return RedirectToAction("Index", "Login");
            }
            else
            {
                OptagricEntities DB = new OptagricEntities();
                ViewBag.CropFields = DB.CropField.ToList();
                ViewBag.CropType = DB.CropType.ToList();
                try
                {
                    if (inputSubRegionName != null && selectedCropFieldID != null && selectedCropTypeID != null && inputCroppedProductAmount != null && inputSoilWaterContent != null)
                    {
                        SubRegion eklenecek = new SubRegion
                        {
                            Name = inputSubRegionName,
                            CF_ID = Convert.ToInt32(selectedCropFieldID),
                            CropTypeID = Convert.ToInt32(selectedCropTypeID),
                            CroppedProductAmount = Convert.ToInt32(inputCroppedProductAmount),
                            SoilWaterContent = Convert.ToInt32(inputSoilWaterContent),
                        };
                        DB.SubRegion.Add(eklenecek);
                        DB.SaveChanges();

                        //ActionLogs(tut, "Insert", "SubRegion_ContentHandler");

                        string p1 = this.ControllerContext.RouteData.Values["action"].ToString();
                        string p2 = this.ControllerContext.RouteData.Values["controller"].ToString();
                        CRUD_Logs(p1, Session["CurrentUserGuid"].ToString(), "INSERT", eklenecek.Name + " / " + p1 + " / " + p2);

                        TempData["SubRegionSuccess"] = "Ekleme Başarılı";
                        return View();
                    }
                    else
                    {
                        return View();
                    }
                }
                catch (Exception)
                {
                    TempData["SubRegionError"] = "Lütfen Değeleri Uygun Giriniz";
                    return View();

                }
            }


        }
        public ActionResult AddCropType(string inputCropLeafAreaIndex, string inputCropTypeName, string gidenBasari = "")
        {
            if (Session["KisiTanimlayici"].ToString() == "Customer")
            {
                TempData["SecurityWall_Home"] = "Güvenlik Duvarına Takıldınız.";
                return RedirectToAction("Index", "Login");
            }
            else
            {
                try
                {
                    if (inputCropTypeName != null && inputCropLeafAreaIndex != "" && Convert.ToDouble(inputCropLeafAreaIndex) < 10)

                    {
                        OptagricEntities DB = new OptagricEntities();
                        CropType eklenecekNesne = new CropType
                        {
                            LeafAreaIndex = Convert.ToDouble(inputCropLeafAreaIndex),
                            Name = inputCropTypeName
                        };

                        DB.CropType.Add(eklenecekNesne);
                        DB.SaveChanges();

                        //ActionLogs(Session["CurrentUserGuid"].ToString(), "Insert", "AddCropType");

                        //Aşağıdakilerde çalıştırılacak SİLME
                        //CRUD_Logs("AddCropType", Session["CurrentUserGuid"].ToString(), "Insert", "AddCropType" + " " + "AdminPanel_CRUD");

                        string p1 = this.ControllerContext.RouteData.Values["action"].ToString();
                        string p2 = this.ControllerContext.RouteData.Values["controller"].ToString();
                        CRUD_Logs(p1, Session["CurrentUserGuid"].ToString(), "INSERT", eklenecekNesne.Name + " / " + p1 + " / " + p2);

                        TempData["AddCropTypeSuccess"] = "Ekleme Başarılı";

                        return View();
                    }
                    else
                    {
                        TempData["AddCropTypeError"] = "Lütfen Değeleri Giriniz.Float değerleri girmek için ayraç olarak virgül kullanınız";
                        return View();
                    }

                }
                catch (Exception)
                {
                    TempData["AddCropTypeError"] = "Lütfen Değeleri Uygun Giriniz";
                    return View();
                }
            }
        }
        public ActionResult CRUD_Logs(string donulecekYer, string gelenAdminID, string gelenProcessType, string gelenModelName)
        {
            if (Session["KisiTanimlayici"].ToString() == "Customer")
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
                            AdminID = new Guid(gelenAdminID.ToString()),
                            ProcessType = gelenProcessType,
                            ModelName = gelenModelName,
                            Date = DateTime.Now


                        };
                        DB.Panel_CRUD_Process_Logs.Add(ekle);
                        DB.SaveChanges();
                        TempData["LogSuccess"] = "Admin Log Ekleme Başarılı";
                        return RedirectToAction(donulecekYer.ToString(), "AdminPanel_CRUD");
                    }
                    else
                    {
                        TempData["LogError"] = "Lütfen Değeleri Uygun Giriniz";
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
        public ActionResult ProcessFlag(string gidenID, string processFlagDesiredValue)
        {
            if (Session["KisiTanimlayici"].ToString() == "")
            {
                TempData["SecurityWall_Home"] = "Güvenlik Duvarına Takıldınız.";
                return RedirectToAction("Index", "Login");
            }
            else
            {
                OptagricEntities DB = new OptagricEntities();
                ViewBag.ProcessFlagList = DB.ProcessFlags.ToList();

                int gelenID = Convert.ToInt32(gidenID);

                if (processFlagDesiredValue != null && gelenID != -1)
                {


                    bool FlagStatus = Boolean.Parse(processFlagDesiredValue);

                    try
                    {
                        ProcessFlags degisecek = DB.ProcessFlags.Where(x => x.ID == gelenID).First();

                        degisecek.ProcessFlagStatus = FlagStatus;
                        DB.ProcessFlags.Attach(degisecek);
                        DB.Entry(degisecek).State = System.Data.Entity.EntityState.Modified;
                        DB.SaveChanges();
                        TempData["ProcessFlagUpdate"] = "Guncelleme Basarili";
                        return View();
                    }
                    catch (Exception)
                    {
                        TempData["ProcessFlagUpdateError"] = "Guncelleme Basarisiz";
                        return View();
                    }
                }
                else
                {
                    return View();
                }
            }

        }
        public ActionResult AdministrationUpdate(string inputName, string inputSurname, string inputEmail, string currentPassword, string newPassword, string newPasswordVerification)
        {

            if (Session["KisiTanimlayici"].ToString() == "Customer")
            {
                TempData["SecurityWall_Home"] = "Güvenlik Duvarına Takıldınız.";
                return RedirectToAction("Index", "Login");

            }
            else
            {
                OptagricEntities DB = new OptagricEntities();

                Guid temp = new Guid(Session["CurrentUserGuid"].ToString());
                ViewBag.currentAdmin = DB.Administration.Where(x => x.ID == temp).First();
                string tempedPassword = ViewBag.currentAdmin.Password;
                string tempedName = ViewBag.currentAdmin.Name;
                string tempedSurname = ViewBag.currentAdmin.Surname;
                string tempedEmail = ViewBag.currentAdmin.Email;


                if (inputName != null || inputSurname != null || inputEmail != null || currentPassword != null || newPassword != null || newPasswordVerification != null)
                {
                    Administration yonetici = ViewBag.currentAdmin;

                    if (currentPassword != null && newPassword != null && newPasswordVerification != null && currentPassword != "" && newPassword != "" && newPasswordVerification != "")//Password Changer Control
                    {
                        yonetici = ViewBag.currentAdmin;
                        if (yonetici.Password == currentPassword && newPassword == newPasswordVerification && newPassword != null)
                        {
                            yonetici.Password = newPassword;
                        }

                        DB.Administration.Attach(yonetici);
                        DB.Entry(yonetici).State = System.Data.Entity.EntityState.Modified;
                        DB.SaveChanges();
                        string p1 = this.ControllerContext.RouteData.Values["action"].ToString();
                        string p2 = this.ControllerContext.RouteData.Values["controller"].ToString();

                        CRUD_Logs(p1, Session["CurrentUserGuid"].ToString(), "UPDATE", tempedPassword + "-->" + yonetici.Password + " / " + p1 + " / " + p2);
                    }
                    if (inputName != null && inputName != "")//Name Changer Control
                    {

                        yonetici = ViewBag.currentAdmin;

                        yonetici.Name = inputName;

                        DB.Administration.Attach(yonetici);
                        DB.Entry(yonetici).State = System.Data.Entity.EntityState.Modified;
                        DB.SaveChanges();
                        string p1 = this.ControllerContext.RouteData.Values["action"].ToString();
                        string p2 = this.ControllerContext.RouteData.Values["controller"].ToString();

                        CRUD_Logs(p1, Session["CurrentUserGuid"].ToString(), "UPDATE", tempedName + "-->" + yonetici.Name + " / " + p1 + " / " + p2);

                    }
                    if (inputSurname != null && inputSurname != "")//Surname Changer Control
                    {

                        yonetici = ViewBag.currentAdmin;

                        yonetici.Surname = inputSurname;

                        DB.Administration.Attach(yonetici);
                        DB.Entry(yonetici).State = System.Data.Entity.EntityState.Modified;
                        DB.SaveChanges();
                        string p1 = this.ControllerContext.RouteData.Values["action"].ToString();
                        string p2 = this.ControllerContext.RouteData.Values["controller"].ToString();

                        CRUD_Logs(p1, Session["CurrentUserGuid"].ToString(), "UPDATE", tempedSurname + "-->" + yonetici.Surname + " / " + p1 + " / " + p2);
                    }
                    if (inputEmail != null && inputEmail != "")//Email Changer Control
                    {

                        yonetici = ViewBag.currentAdmin;

                        yonetici.Email = inputEmail;

                        DB.Administration.Attach(yonetici);
                        DB.Entry(yonetici).State = System.Data.Entity.EntityState.Modified;
                        DB.SaveChanges();

                        string p1 = this.ControllerContext.RouteData.Values["action"].ToString();
                        string p2 = this.ControllerContext.RouteData.Values["controller"].ToString();

                        CRUD_Logs(p1, Session["CurrentUserGuid"].ToString(), "UPDATE", tempedEmail + "-->" + yonetici.Email + " / " + p1 + " / " + p2);
                    }

                    TempData["Degislik"] = "Degisiklik Basarili";

                    ViewBag.currentAdmin = yonetici;
                    Session["AdminEmail"] = ViewBag.currentAdmin.Email;
                    Session["AdminPassword"] = ViewBag.currentAdmin.Password;
                    return View();
                }

                else
                {
                    TempData["Degislik"] = "Degisiklik Yapılmadi";
                    return View();
                }

            }


        }
        public void AuthenticationRouterForCustomer(string donulecekYer)
        {
            if (Session["KisiTanimlayici"].ToString() == "Customer")
            {
                TempData["GirisHatasi"] = "Güvenlik Duvarına Takıldınız.";
                Response.Redirect(Url.Action("Index", "Login"));
            }
            else
            {
                Response.Redirect(Url.Action(donulecekYer.ToString(), "AdminPanel_CRUD"));
            }
        }
        public ActionResult PredictionCreator(string predictionType, string subRegionID, string inputPrecipitation, string inputTemperature, string inputCropProduction, string inputCropDestruction, string inputWaterConsumption)
        {
            OptagricEntities DB = new OptagricEntities();
            List<Predictions> lst = DB.Predictions.ToList();
            //ViewBag.SubRegion= DB.SubRegion.Select(x => new { x.ID , x.Name }).Distinct().ToList();
            ViewBag.SubRegion = DB.SubRegion.ToList();
            if (predictionType != "" && predictionType != null && subRegionID != "" && inputPrecipitation != "" && inputTemperature != "" && inputCropProduction != "" && inputCropDestruction != "" && inputWaterConsumption != "" && subRegionID != null && inputPrecipitation != null && inputTemperature != null && inputCropProduction != null && inputCropDestruction != null && inputWaterConsumption != null)
            {


                Predictions ekle = new Predictions
                {
                    Type = predictionType,
                    IDSubRegion = Convert.ToInt32(subRegionID),
                    PA_Precipitatation = Convert.ToInt32(inputPrecipitation),
                    PA_Temp = Convert.ToInt32(inputTemperature),
                    PU_CropProduction = Convert.ToInt32(inputCropProduction),
                    PK_Disaster = Convert.ToInt32(inputCropDestruction),
                    PT_WaterConsumption = Convert.ToInt32(inputWaterConsumption),
                    PredictionDate = DateTime.Now
                };
                DB.Predictions.Add(ekle);
                DB.SaveChanges();
                return View();
            }
            else
            {
                return View(lst);
            }
        }
        public ActionResult ScalingPredictions()
        {


            //string staticSubs =["Satakum-1-2018", "Satakum-2-2018", "Satakum-3-2018", "Satakum-4-2018", "Satakum-5-2018", "Satakum-6-2018"];
            string[] staticTypes = new string[]{ "Irrigation", "Crop Destruction", "Croped Product" };

            Random arr = new Random();
            Random scaling = new Random();
            

            //string tempP1 = staticSubs[arr.Next(0, 5)].ToString();

            OptagricEntities DB = new OptagricEntities();
            for (int i = 0; i < 100; i++)
            {
                Predictions eklenecek = new Predictions
                {
                    IDSubRegion = arr.Next(2, 20),
                    PA_Precipitatation = scaling.Next(0, 100),
                    PA_Temp = scaling.Next(0, 100),
                    PK_Disaster = scaling.Next(0, 100),
                    PT_WaterConsumption = scaling.Next(0, 100),
                    PU_CropProduction = scaling.Next(0, 100),
                    Type = staticTypes[arr.Next(0, 2)].ToString(),
                    PredictionDate = DateTime.Now
                };
                DB.Predictions.Add(eklenecek);
                DB.SaveChanges();
            }

            return RedirectToAction("PredictionCreator");
        }
        public ActionResult List_Panel_CRUD_Log()
        {
            OptagricEntities DB = new OptagricEntities();
            List<Panel_CRUD_Process_Logs> listCRUDLogs = DB.Panel_CRUD_Process_Logs.ToList();

            return View(listCRUDLogs);
        }
        public ActionResult AssignCropField()
        {
            return View();
        }

    }
}