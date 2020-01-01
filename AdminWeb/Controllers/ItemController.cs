using AdminWeb.CustomFilters;
using AdminWeb.Models.BLL;
using AdminWeb.Models.DataModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AdminWeb.Controllers
{
    public class ItemController : Controller
    {
        [AuthLog]
        [PageTittleAttributeActionFilter(Function = "Item_ListItem")]
        public async Task<ActionResult> ListItem(string Token, string Type, string profile, string FromDate, string ToDate, string SearchText = "", string sortby = "", int? PageSize = 10, int? SearchType = 1, int? page = 1, int id = 0, string lang = "FA")
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

        [AuthLog]
        [HttpPost]
        public async Task<ActionResult> ChangeStateItem(int ItemID, string Description, string SubmitedState, string Token,string Type)
        {
            ItemManagement im = new ItemManagement();
            var model = new { id = ItemID, state = SubmitedState, description = Description };
            string scale = await im.ChangeStateItem(model, Token);
            TempData["Notification"] = scale == "OK" ? "success" : "danger";
            return RedirectToAction("ListItem", "Item",new {Type=Type });
        }

        [AuthLog]
        public ActionResult ItemDetail(int ItemID, string Type, string Token)
        {
            ViewBag.PrePath = Tools.ReturnPathPhysicalMode("ItemImagePath", "WebsiteAddress", "ItemDetail","");
            ItemManagement im = new ItemManagement();
            ViewBag.Type = Type;
            var model = Task.Run(() => im.ItemDetail(ItemID, Token)).Result;
            return PartialView(model);
        }
    }
}