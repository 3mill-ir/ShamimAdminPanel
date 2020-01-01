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
    public class SliderController : Controller
    {
        [PageTittleAttributeActionFilter(Function = "ListSlide_Slider")]
        [AuthLog]
        public ActionResult ListSlide(string Token)
        {
            @ViewBag.Notification = TempData["JSNotifyMsg"];
            string F_UserName = Tools.F_UserName(Token);
            SliderManagement SM = new SliderManagement(F_UserName);
            @ViewBag.prePath = Tools.ReturnPath("SliderPath", F_UserName, "ListSlide()");
            return View(SM.LoadSlider().OrderBy(u => u.Priority));
        }

        [PageTittleAttributeActionFilter(Function = "EditSlide_Slider")]
        [AuthLog]
        public ActionResult EditSlide(int SlideId, string Token)
        {
            string F_UserName = Tools.F_UserName(Token);
            SliderManagement SM = new SliderManagement(F_UserName);
            @ViewBag.prePath = Tools.ReturnPath("SliderPath", F_UserName, "EditSlide()");
            return View(SM.LoadSlider().FirstOrDefault(u => u.ID == SlideId));
        }

        [HttpPost]
        [AuthLog]
        [PageTittleAttributeActionFilter(Function = "EditSlide_Slider")]
        public ActionResult EditSlide(SliderModel model, HttpPostedFileBase newUploadImage, string Token)
        {
            string F_UserName = Tools.F_UserName(Token);
            TempData["JSNotifyMsg"] = "success";
            SliderManagement SM = new SliderManagement(F_UserName);
            SM.EditSlider(model, newUploadImage, F_UserName);
            return RedirectToAction("ListSlide", "Slider");
        }


        [HttpPost]
        [AuthLog]
        public ActionResult ChangeDisplaySlide(int SlideId, string Token)
        {
            SliderManagement SM = new SliderManagement(Tools.F_UserName(Token));
            if (SM.ChangeDisplaySlider(SlideId)) { return RedirectToAction("ListSlide", "Slider"); }
            return View("Error");
        }
    }
}