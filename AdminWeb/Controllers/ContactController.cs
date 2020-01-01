using AdminWeb.CustomFilters;
using AdminWeb.Models.BLL;
using AdminWeb.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminWeb.Controllers
{
    public class ContactController : Controller
    {
        [AuthLog]
        [PageTittleAttributeActionFilter(Function = "EditContact_Contact")]
        public ActionResult EditContact(string Token)
        {
            ContactManagement CM = new ContactManagement(Tools.F_UserName(Token));
            return View(CM.LoadContact());
        }
        [HttpPost]
        [AuthLog]
        [PageTittleAttributeActionFilter(Function = "EditContact_Contact")]
        public ActionResult EditContact(ContactManagementModel model,string Token)
        {
            ContactManagement CM = new ContactManagement(Tools.F_UserName(Token));
            if (CM.UpdateContact(model))
                ViewBag.Notification = "success";
            else
                ViewBag.Notification = "danger";
            return PartialView();
        }
    }
}