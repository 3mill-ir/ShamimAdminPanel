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
    public class LinksController : Controller
    {
        #region MainLinks
        [AuthLog]
        [PageTittleAttributeActionFilter(Function = "ListMainLinks_Links")]
        public ActionResult ListMainLinks(string Token)
        {
            ViewBag.Notification = TempData["JSNotifyMsg"];
            string F_UserName = Tools.F_UserName(Token);
            ViewBag.PrePath = Tools.ReturnPath("LinksPath", F_UserName, "ListMainLinks()");
            LinksManagement LM = new LinksManagement(F_UserName);
            return View(LM.LoadLinks().Where(u => u.Type == "Main"));
        }
        [AuthLog]
        [PageTittleAttributeActionFilter(Function = "EditMainLink_Links")]
        public ActionResult EditMainLink(int LinkID, string Token)
        {
            string F_UserName = Tools.F_UserName(Token);
            ViewBag.PrePath = Tools.ReturnPath("LinksPath", F_UserName, "EditMainLink()");
            LinksManagement LM = new LinksManagement(F_UserName);
            return View(LM.LoadLinks().FirstOrDefault(u => u.ID == LinkID));
        }
        [HttpPost]
        [AuthLog]
        [PageTittleAttributeActionFilter(Function = "EditMainLink_Links")]
        public ActionResult EditMainLink(LinkModel model, HttpPostedFileBase MyNewImage, string Token)
        {
            if (string.IsNullOrEmpty(model.Link))
                ModelState.AddModelError("Link", Resource.Resource.View_ValidationError);
            if (!ModelState.IsValid)
            {
                model.Image = "Rahbari.jpg";
                return View(model);
            }
            TempData["JSNotifyMsg"] = "success";
            LinksManagement LM = new LinksManagement(ViewBag.UserName);
            LM.EditLink(model,Token, MyNewImage);
            return RedirectToAction("ListMainLinks", "Links");
        }
        #endregion

        #region FooterLinks
        [AuthLog]
        [PageTittleAttributeActionFilter(Function = "ListFooterLinks_Links")]
        public ActionResult ListFooterLinks(string Token)
        {
            ViewBag.Notification = TempData["JSNotifyMsg"];
            LinksManagement LM = new LinksManagement(Tools.F_UserName(Token));
            return View(LM.LoadLinks().Where(u => u.Type == "Footer"));
        }
        [AuthLog]
        [PageTittleAttributeActionFilter(Function = "EditFooterLink_Links")]
        public ActionResult EditFooterLink(int LinkID, string Token)
        {
            LinksManagement LM = new LinksManagement(Tools.F_UserName(Token));
            return View(LM.LoadLinks().FirstOrDefault(u => u.ID == LinkID));
        }
        [HttpPost]
        [AuthLog]
        [PageTittleAttributeActionFilter(Function = "EditFooterLink_Links")]
        public ActionResult EditFooterLink(LinkModel model, string Token)
        {
            if (string.IsNullOrEmpty(model.Text))
                ModelState.AddModelError("Text", Resource.Resource.View_ValidationError);
            if (string.IsNullOrEmpty(model.Link))
                ModelState.AddModelError("Link", Resource.Resource.View_ValidationError);
            if (!ModelState.IsValid)
                return View(model);
            TempData["JSNotifyMsg"] = "success";
            LinksManagement LM = new LinksManagement(Tools.F_UserName(Token));
            LM.EditLink(model,Token);
            return RedirectToAction("ListFooterLinks", "Links");
        }
        #endregion
    }
}