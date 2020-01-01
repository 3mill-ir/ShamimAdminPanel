using AdminWeb.CustomFilters;
using AdminWeb.Models.BLL;
using AdminWeb.Models.DataModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AdminWeb.Controllers
{
    public class CategoryController : Controller
    {

        [PageTittleAttributeActionFilter(Function = "Category_List")]
        [AuthLog]
        public async Task<ActionResult> List(string Token)
        {
            MenuManagement Category = new MenuManagement();
            ViewBag.Notification = TempData["Notification"];
            return View(await Category.ListMenus(Token,new List < string > { "FanbazarDemand", "FanbazarOffer","FanbazarOfferDemand" ,"FanbazarCompany"},new List<string> { "FA"}, ViewBag.UserName as string));
        }

        [HttpPost]
        [AuthLog]
        public JsonResult GetLanguageTypeCategory(string Lang,string Type, string Token)
        {
            return (Json(Tools.F_MenuIDs(ViewBag.UserName as string, new List<string> {Type}, new List<string> { Lang }, Token, null, null)));
        }
        [PageTittleAttributeActionFilter(Function = "Category_Add")]
        [AuthLog]
        public ActionResult Add(string Token)
        {
            var Langs = Tools.LanguagesCombo(Token);
            ViewBag.F_MenuIDs = Tools.F_MenuIDs(ViewBag.UserName as string, new List<string> { "FanbazarDemand", "FanbazarOffer", "FanbazarOfferDemand", "FanbazarCompany" }, Langs.Select(u => u.Value).ToList(), Token, null, null);
            ViewBag.Languages = Langs;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PageTittleAttributeActionFilter(Function = "Category_Add")]
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
                    return RedirectToAction("List", "Category");
                }
            }

            ViewBag.F_MenuIDs = Tools.F_MenuIDs(ViewBag.UserName as string, new List<string> { "FanbazarDemand", "FanbazarOffer", "FanbazarOfferDemand", "FanbazarCompany" }, new List<string> { model.Language }, Token, model.F_MenuID, null);
            ViewBag.Notification = "danger";
            return View(model);
        }

        [HttpPost]
        [AuthLog]
        public JsonResult GetLanguageCategorys(string Lang, string Token)
        {
            return (Json(Tools.F_CategoryIDs(Lang, Token)));

        }
        [PageTittleAttributeActionFilter(Function = "Category_Edit")]
        [AuthLog]
        public async Task<ActionResult> Edit(int ID, string Token)
        {
            MenuManagement Category = new MenuManagement();
            var detail = await Category.DetailMenu(ID, Token);
            ViewBag.F_CategoryIDs = Tools.F_MenuIDs(ViewBag.UserName as string, new List<string> { "FanbazarDemand", "FanbazarOffer", "FanbazarOfferDemand", "FanbazarCompany" }, new List<string> { detail.Language }, Token, detail.F_MenuID, null);
            ViewBag.Languages = Tools.LanguagesCombo(Token, detail.Language);
            return View(detail);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PageTittleAttributeActionFilter(Function = "Category_Edit")]
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
                    return RedirectToAction("List", "Category");
                }
            }
            ViewBag.F_MenuIDs = Tools.F_MenuIDs(ViewBag.UserName as string, new List<string> { }, new List<string> { model.Language }, Token, model.F_MenuID);
            ViewBag.Languages = Tools.LanguagesCombo(Token, model.Language);

            ViewBag.Notification = "danger";
            return View(model);
        }

        [HttpPost]
        [AuthLog]
        public async Task<ActionResult> ChangeStatusCategory(int ID, string Token)
        {
            MenuManagement Category = new MenuManagement();
            string scale = await Category.ChangeStatusMenu(ID, Token);
            if (scale == "OK")
                TempData["Notification"] = "success";
            else
                TempData["Notification"] = "danger";
            return RedirectToAction("List", "Category");
        }

        [HttpPost]
        [AuthLog]
        public async Task<ActionResult> DeleteCategory(int ID, string Token)
        {
            MenuManagement Category = new MenuManagement();
            string scale = await Category.DeleteMenu(ID, Token);
            if (scale == "OK")
                TempData["Notification"] = "success";
            else
                TempData["Notification"] = "danger";
            return RedirectToAction("List", "Category");
        }


        [HttpPost]
        [AuthLog]
        public async Task<ActionResult> DeleteCategoryCascade(int ID, string Token)
        {
            MenuManagement Category = new MenuManagement();
            string scale = await Category.DeleteMenuCascade(ID, Token);
            if (scale == "OK")
                TempData["Notification"] = "success";
            else
                TempData["Notification"] = "danger";
            return RedirectToAction("List", "Category");
        }
    }
}