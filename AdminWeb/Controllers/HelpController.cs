using AdminWeb.CustomFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminWeb.Controllers
{
    public class HelpController : Controller
    {
        [AuthLog]
        [PageTittleAttributeActionFilter(Function = "Index_Help")]
        public ActionResult Index()
        {
            return View();
        }
    }
}