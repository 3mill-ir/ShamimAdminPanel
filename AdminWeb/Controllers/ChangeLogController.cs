using AdminWeb.CustomFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminWeb.Controllers
{
    public class ChangeLogController : Controller
    {
        [AuthLog]
        [PageTittleAttributeActionFilter(Function = "Index_ChangeLog")]
        public ActionResult Index()
        {
            return View();
        }
    }
}