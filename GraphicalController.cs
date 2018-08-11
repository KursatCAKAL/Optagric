using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OptagricDataLayer;

namespace Optagric_Management_Portal.Controllers
{
    public class GraphicalController : Controller
    {
        // GET: Graphical
        public ActionResult Precipitation()
        {
            if (Session["KisiTanimlayici"].ToString() == "Admin")
            {
                TempData["SecurityWall_Home"] = "Güvenlik Duvarına Takıldınız";
                Session["KisiTanimlayici"] = "";
                return RedirectToAction("Index", "Login");
            }
            else
            {
                Session["KisiTanimlayici"] = "Customer";
                return View();
            }

        }
        public ActionResult Temperature()
        {
            if (Session["KisiTanimlayici"].ToString() == "Admin")
            {
                TempData["SecurityWall_Home"] = "Güvenlik Duvarına Takıldınız";
                Session["KisiTanimlayici"] = "";
                return RedirectToAction("Index", "Login");
            }
            else
            {
                Session["KisiTanimlayici"] = "Customer";
                return View();
            }

        }
        public ActionResult TemperatureForSixMont()
        {
            if (Session["KisiTanimlayici"].ToString() == "Admin")
            {
                TempData["SecurityWall_Home"] = "Güvenlik Duvarına Takıldınız";
                Session["KisiTanimlayici"] = "";
                return RedirectToAction("Index", "Login");
            }
            else
            {
                Session["KisiTanimlayici"] = "Customer";
                return View();
            }

        }
        public ActionResult TotalYield()
        {
            if (Session["KisiTanimlayici"].ToString() == "Admin")
            {
                TempData["SecurityWall_Home"] = "Güvenlik Duvarına Takıldınız";
                Session["KisiTanimlayici"] = "";
                return RedirectToAction("Index", "Login");
            }
            else
            {
                Session["KisiTanimlayici"] = "Customer";
                return View();
            }

        }
        public ActionResult CropYieldRate()
        {
            if (Session["KisiTanimlayici"].ToString() == "Admin")
            {
                TempData["SecurityWall_Home"] = "Güvenlik Duvarına Takıldınız";
                Session["KisiTanimlayici"] = "";
                return RedirectToAction("Index", "Login");
            }
            else
            {
                Session["KisiTanimlayici"] = "Customer";
                return View();
            }

        }
        public ActionResult YearlyCropYieldRate()
        {
            if (Session["KisiTanimlayici"].ToString() == "Admin")
            {
                TempData["SecurityWall_Home"] = "Güvenlik Duvarına Takıldınız";
                Session["KisiTanimlayici"] = "";
                return RedirectToAction("Index", "Login");
            }
            else
            {
                Session["KisiTanimlayici"] = "Customer";
                return View();
            }

        }
        public ActionResult CurrentCropYieldAmount()
        {
            OptagricEntities DB = new OptagricEntities();
            
            var subFieldContentInformation = DB.SubRegion.Select(x => x.CroppedProductAmount);
            var subFieldContentName = DB.SubRegion.Select(x => x.Name);

            ViewBag.CropAmountCorrection = DB.SubRegion.ToList();
            ViewBag.SFCI = subFieldContentInformation;
            ViewBag.SFCN = subFieldContentName;
            Session["KisiTanimlayici"] = "Customer";
            return View();
        }
        public ActionResult WeatherAppIllustrator()
        {

            if (Session["KisiTanimlayici"].ToString() == "Admin")
            {
                TempData["SecurityWall_Home"] = "Güvenlik Duvarına Takıldınız";
                Session["KisiTanimlayici"] = "";
                return RedirectToAction("Index", "Login");
            }
            else
            {
                Session["KisiTanimlayici"] = "Customer";
                return View();
            }
        }

        public ActionResult PredictionPureValues()
        {
            using (OptagricEntities DB = new OptagricEntities())
            {
                ViewBag.TypeLabes = DB.Predictions.Select(x => x.Type).ToList();
                ViewBag.PrecipitationRates = DB.Predictions.Select(x => x.PA_Precipitatation).ToList();
                ViewBag.CroppedProductionRates = DB.Predictions.Select(x => x.PU_CropProduction).ToList();
                ViewBag.CropDestruction = DB.Predictions.Select(x => x.PK_Disaster).ToList();
                ViewBag.WaterConsumption = DB.Predictions.Select(x => x.PT_WaterConsumption).ToList();
            }
            Session["KisiTanimlayici"] = "Customer";
            return View();
        }
        public ActionResult HumidityPrecipitation() {


            return View();
        }
    }
}