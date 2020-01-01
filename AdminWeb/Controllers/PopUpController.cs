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
    public class PopUpController : Controller
    {
        [AuthLog]
        [PageTittleAttributeActionFilter(Function = "EditPopUp_PopUp")]
        public ActionResult EditPopUp(string Token)
        {
            ViewBag.Notification = TempData["JSNotifyMsg"];
            string F_UserName = Tools.F_UserName(Token);
            ViewBag.PrePath = Tools.ReturnPath("PopUpPath", F_UserName, "EditPopUp()");
            PopUpManagement LM = new PopUpManagement(F_UserName);
            return View(LM.LoadPopUp());
        }
        [HttpPost]
        [AuthLog]
        [PageTittleAttributeActionFilter(Function = "EditPopUp_PopUp")]
        public ActionResult EditPopUp(PopUpModel model, HttpPostedFileBase MyNewImage, string Token)
        {
            TempData["JSNotifyMsg"] = "success";
            PopUpManagement LM = new PopUpManagement(ViewBag.UserName);
            LM.EditPopUp(model, Token, MyNewImage);
            return RedirectToAction("EditPopUp", "PopUp");
        }
    }
}