using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminWeb.Controllers
{
    public class UtilityController : Controller
    {
        // GET: Utility
        public ActionResult NotAuthorized()
        {
            return View();
        }


    }
}