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
    public class FormBuilderController : Controller
    {
        // GET: FormBuilder
        public ActionResult Index()
        {
            return View();
        }

        [PageTittleAttributeActionFilter(Function = "Form_List")]
        [AuthLog]
        public async Task<ActionResult> List(string Token)
        {
            MenuManagement Category = new MenuManagement();
            ViewBag.Notification = TempData["Notification"];
            return View(await Category.ListMenus(Token, new List<string> { "Form" }, new List<string> { "FA" }, ViewBag.UserName as string));
        }

        [AuthLog]
        [PageTittleAttributeActionFilter(Function = "Item_ListItem")]
        public async Task<ActionResult> ListForms(string Token, string Type, string profile, string FromDate, string ToDate, string SearchText = "", string sortby = "", int? PageSize = 10, int? SearchType = 1, int? page = 1, int id = 0, string lang = "FA")
        {
            ItemManagement im = new ItemManagement();
            ViewBag.Type = Type;
            ViewBag.Statuses = Tools.Statuses();
            ViewBag.F_MenuId = id;
            if (string.IsNullOrEmpty(FromDate)) FromDate = "1370/01/01";
            if (string.IsNullOrEmpty(ToDate)) ToDate = "1450/01/01";
            DateTime From; DateTime To;
            Tools.GetJalaliDateReturnDateTime(FromDate + " 00:00:00", out From);
            Tools.GetJalaliDateReturnDateTime(ToDate + " 00:00:00", out To);
            FilterItemDataModel model = new FilterItemDataModel() { FromTime = From, MenuId = id, PageNumber = 1, PageSize = 1000000, search = SearchText, ToTime = To, Searchtype = SearchType ?? default(int), sortby = sortby, type = Type };
            ViewBag.Type = Type; ViewBag.lang = lang;
            ViewBag.SearchText = SearchText; ViewBag.sortby = sortby; ViewBag.profile = profile; ViewBag.lang = lang; ViewBag.id = id;

            return View(await im.ListItem(Token, Type, model));
        }


        [PageTittleAttributeActionFilter(Function = "Form_Add")]
        [AuthLog]
        public ActionResult Add(string Token)
        {
            ViewBag.Languages = Tools.LanguagesCombo(Token);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PageTittleAttributeActionFilter(Function = "Form_Add")]
        [AuthLog]
        public async Task<ActionResult> Add(MenuDataModel model, HttpPostedFileBase MyFile, string Token)
        {
            model.Type = "Form";
            ModelState["Type"].Errors.Clear();
            if (ModelState.IsValid)
            {
                MenuManagement menu = new MenuManagement();
                string scale = await menu.AddMenu(model, MyFile, Token, ViewBag.UserName as string);
                if (scale == "OK")
                {
                    TempData["Notification"] = "success";
                    return RedirectToAction("List", "FormBuilder");
                }
            }
            ViewBag.Languages = Tools.LanguagesCombo(Token);
            ViewBag.Notification = "danger";
            return View(model);
        }
        [PageTittleAttributeActionFilter(Function = "Form_Edit")]
        [AuthLog]
        public async Task<ActionResult> Edit(int ID, string Token)
        {
            MenuManagement Form = new MenuManagement();
            var detail = await Form.DetailMenu(ID, Token);
            ViewBag.Languages = Tools.LanguagesCombo(Token, detail.Language);
            return View(detail);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PageTittleAttributeActionFilter(Function = "Form_Edit")]
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
                    return RedirectToAction("List", "FormBuilder");
                }
            }
            ViewBag.Languages = Tools.LanguagesCombo(Token, model.Language);
            ViewBag.Notification = "danger";
            return View(model);
        }

        [HttpPost]
        [AuthLog]
        public async Task<ActionResult> ChangeStatusForm(int ID, string Token)
        {
            MenuManagement Form = new MenuManagement();
            string scale = await Form.ChangeStatusMenu(ID, Token);
            if (scale == "OK")
                TempData["Notification"] = "success";
            else
                TempData["Notification"] = "danger";
            return RedirectToAction("List", "FormBuilder");
        }
        [HttpPost]
        [AuthLog]
        public async Task<ActionResult> DeleteForm(int ID, string Token)
        {
            MenuManagement Form = new MenuManagement();
            string scale = await Form.DeleteMenu(ID, Token);
            if (scale == "OK")
                TempData["Notification"] = "success";
            else
                TempData["Notification"] = "danger";
            return RedirectToAction("List", "FormBuilder");
        }
        [HttpPost]
        [AuthLog]
        public async Task<ActionResult> DeleteFormCascade(int ID, string Token)
        {
            MenuManagement Form = new MenuManagement();
            string scale = await Form.DeleteMenuCascade(ID, Token);
            if (scale == "OK")
                TempData["Notification"] = "success";
            else
                TempData["Notification"] = "danger";
            return RedirectToAction("List", "FormBuilder");
        }

        [AuthLog]
        public ActionResult FormDetail(int ItemID, string Type, string Token)
        {
            ViewBag.PrePath = Tools.ReturnPathPhysicalMode("ItemImagePath", "WebsiteAddress", "ItemDetail", "");
            ItemManagement im = new ItemManagement();
            ViewBag.Type = Type;
            var model = Task.Run(() => im.ItemDetail(ItemID, Token)).Result;
            TempData["PrePath"] = Tools.ReturnPathPhysicalMode("FormFilesPath", "WebsiteAddress", "ListForms", Tools.F_UserName(Token));
            return PartialView(model);
        }

        [AuthLog]
        [HttpPost]
        public async Task<ActionResult> ChangeStateForm(int ItemID, string Description, string SubmitedState, string Token, string Type,int F_MenuID)
        {
            ItemManagement im = new ItemManagement();
            var model = new { id = ItemID, State = SubmitedState, Description = Description };
            string scale = await im.ChangeStateItem(model, Token);
            TempData["Notification"] = scale == "OK" ? "success" : "danger";
            return RedirectToAction("ListForms", "FormBuilder", new { Type = Type,id=F_MenuID });
        }
    }
}