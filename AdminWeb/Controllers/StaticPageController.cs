using AdminWeb.CustomFilters;
using AdminWeb.Models.BLL;
using AdminWeb.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AdminWeb.Controllers
{
    public class StaticPageController : Controller
    {
        [AuthLog]
        [PageTittleAttributeActionFilter(Function = "StaticPage_PageList")]
        public ActionResult PageList(string Token)
        {
            ViewBag.Notification = TempData["Notification"];
             var menu = Tools.UserMenu(ViewBag.RootUserName, new List<string> { "StaticPost" }, Token);
            PageManagement pg = new PageManagement();
            return View(pg.ListPage(Token));
        }
        [AuthLog]
        [PageTittleAttributeActionFilter(Function = "StaticPage_PageDetail")]
        public async Task<ActionResult> PageDetail(int idm, string Token)
        {
            ViewBag.Notification = TempData["Notification"];
            string F_UserName = ViewBag.RootUserName;
            ViewBag.UserImageFolder = Tools.ReturnPath("DynamicPageContentImages", F_UserName, "PostCreate()");
            MenuManagement menu = new MenuManagement();
            var detail = await menu.DetailMenu(idm, Token);
            if (detail == null | detail.Type != "StaticPost")
            {
                return RedirectToAction("PageList");
            }
            ViewBag.MenuName = detail.Name;
            ViewBag.Lang = detail.Language;
            PageManagement pm = new PageManagement();
            var page = await pm.DetailPage(idm, Token);
            if (page == null)
            {
                return RedirectToAction("PageList", "StaticPage");

            }
            page.ActionContent = Tools.GetHtmldetail(page.ActionContent, F_UserName);
            return View(page);
        }

        [AuthLog]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditPage(PageDataModel model, string Token)
        {
            string F_UserName = ViewBag.RootUserName;
            ViewBag.UserImageFolder = Tools.ReturnPath("DynamicPageContentImages", F_UserName, "PostCreate()");

            PageManagement pm = new PageManagement();

            model.ActionContent = Tools.SaveHtmlDetail(model.ActionContent, F_UserName, model.ActionContent);
            string scale = await pm.EditPage(model, Token);
            if (scale == "OK")
                TempData["Notification"] = "success";
            else
                TempData["Notification"] = "danger";
            return RedirectToAction("PageDetail", "StaticPage", new { idm = model.ID });
        }
    }
}