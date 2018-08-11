using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Optagric_Management_Portal.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult RoutingErrorUncoveredPage()
        {
            return View();
        }
    }
}