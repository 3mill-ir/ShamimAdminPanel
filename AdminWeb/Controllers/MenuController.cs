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
    public class MenuController : Controller
    {
        [PageTittleAttributeActionFilter(Function = "Menu_List")]
        [AuthLog]
        public async Task<ActionResult> List(string Token)
        {
            MenuManagement menu = new MenuManagement();
            ViewBag.Notification = TempData["Notification"];
            return View(await menu.ListMenus(Token,new List<string> {"StaticPost","DynamicPost","NoneStaticDynamic" }, Tools.LanguagesCombo(Token).Select(u=>u.Value).ToList(),ViewBag.UserName as string));
        }
        [PageTittleAttributeActionFilter(Function = "Menu_Add")]
        [AuthLog]
        public ActionResult Add(string Token)
        {var Langs= Tools.LanguagesCombo(Token);
            ViewBag.F_MenuIDs = Tools.F_MenuIDs(ViewBag.UserName as string, new List<string> { "StaticPost", "DynamicPost", "NoneStaticDynamic" }, Langs.Select(u => u.Value).ToList(), Token, null, null);
            ViewBag.Languages = Langs;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PageTittleAttributeActionFilter(Function = "Menu_Add")]
        [AuthLog]
        public async Task<ActionResult> Add(MenuDataModel model, HttpPostedFileBase MyFile, string Token)
        {
            if (ModelState.IsValid)
            {
                MenuManagement menu = new MenuManagement();
               
                ViewBag.Languages = Tools.LanguagesCombo(Token);
                string scale = await menu.AddMenu(model, MyFile, Token, ViewBag.UserName as string);
                if (scale == "OK")
                {
                    TempData["Notification"] = "success";
                    return RedirectToAction("List", "Menu");
                }
            }

            ViewBag.F_MenuIDs = Tools.F_MenuIDs(ViewBag.UserName as string, new List<string> { "StaticPost", "DynamicPost", "NoneStaticDynamic" }, new List<string> { model.Language}, Token, model.F_MenuID, null);
            ViewBag.Notification = "danger";
            return View(model);

        }

        [HttpPost]
        [AuthLog]
        public JsonResult GetLanguageMenus(string Lang, string Token)
        {
            return (Json(Tools.F_MenuIDs(ViewBag.UserName as string, new List<string> { "StaticPost", "DynamicPost", "NoneStaticDynamic" }, new List<string> { Lang }, Token,null,null)));
        }
        [PageTittleAttributeActionFilter(Function = "Menu_Edit")]
        [AuthLog]
        public async Task<ActionResult> Edit(int ID, string Token)
        {
            MenuManagement menu = new MenuManagement();
            var detail = await menu.DetailMenu(ID, Token);
            ViewBag.F_MenuIDs = Tools.F_MenuIDs(ViewBag.UserName as string, new List<string> { "StaticPost", "DynamicPost", "NoneStaticDynamic" }, new List<string> { detail.Language }, Token, detail.F_MenuID, null);
            ViewBag.Languages = Tools.LanguagesCombo(Token,detail.Language);
            return View(detail);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PageTittleAttributeActionFilter(Function = "Menu_Edit")]
        [AuthLog]
        public async Task<ActionResult> Edit(MenuDataModel model, HttpPostedFileBase MyFile, string Token)
        {

            if (ModelState.IsValid)
            {
                ViewBag.Languages = Tools.LanguagesCombo(Token);
                MenuManagement menu = new MenuManagement();
                string scale = await menu.EditMenu(model, MyFile, Token, ViewBag.UserName as string);
                if (scale == "OK")
                {
                    TempData["Notification"] = "success";
                    return RedirectToAction("List", "Menu");
                }
            }
            ViewBag.F_MenuIDs = Tools.F_MenuIDs(ViewBag.UserName as string, new List<string> { }, new List<string> { model.Language}, Token, model.F_MenuID);
            ViewBag.Languages = Tools.LanguagesCombo(Token, model.Language);

            ViewBag.Notification = "danger";
            return View(model);

        }

        [HttpPost]
        [AuthLog]
        public async Task<ActionResult> ChangeStatusMenu(int ID, int Page, string Token)
        {
            MenuManagement menu = new MenuManagement();
            string scale = await menu.ChangeStatusMenu(ID, Token);
            if (scale == "OK")
                TempData["Notification"] = "success";
            else
                TempData["Notification"] = "danger";
            return RedirectToAction("List", "Menu");
        }

        [HttpPost]
        [AuthLog]
        public async Task<ActionResult> DeleteMenu(int ID, int Page, string Token)
        {
            MenuManagement menu = new MenuManagement();
            string scale = await menu.DeleteMenu(ID, Token);
            if (scale == "OK")
                TempData["Notification"] = "success";
            else
                TempData["Notification"] = "danger";
            return RedirectToAction("List", "Menu");
        }

        [HttpPost]
        [AuthLog]
        public async Task<ActionResult> DeleteMenuCascade(int ID, int Page, string Token)
        {
            MenuManagement menu = new MenuManagement();
            string scale = await menu.DeleteMenuCascade(ID, Token);
            if (scale == "OK")
                TempData["Notification"] = "success";
            else
                TempData["Notification"] = "danger";
            return RedirectToAction("List", "Menu");
        }
    }
}