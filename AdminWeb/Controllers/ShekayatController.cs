using AdminWeb.CustomFilters;
using AdminWeb.Models.BLL;
using AdminWeb.Models.DataModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminWeb.Controllers
{
    public class ShekayatController : Controller
    {
        [PageTittleAttributeActionFilter(Function = "Shekayat_ShekayatManagement")]
        [AuthLog]
        public ActionResult Index()
        {
            return View();
        }
        [AuthLog]
        public ActionResult ShekayatAccessory(string Token)
        {
            ShekayatManagement tk = new ShekayatManagement();
            return PartialView(tk.ShekayatAccessory(Token));
        }

        [HttpPost]
        [AuthLog]
        public ActionResult ShekayatList(string Token, string currentFilter, string searchString, string ShekayatStatus, int page = 1)
        {
            ShekayatManagement Shekayat = new ShekayatManagement();
            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;
            ViewBag.CurrentFilter = searchString;
            ViewBag.CurrentStatus = ShekayatStatus;
            ViewBag.mypage = page;
            var temp = Shekayat.ShekayatList(Token, currentFilter, searchString, ShekayatStatus, page);
            int total = temp.Total;
            var pagedList = new StaticPagedList<Shekayat>(temp.Shekayats, page, 10, total);
            return PartialView(pagedList);
        }
        [AuthLog]
        public ActionResult ShekayatDetail(int F_ShekayatID, string Token)
        {
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "انتخاب", Value = "" });
            li.Add(new SelectListItem { Text = "در وضعیت انتظار", Value = "در وضعیت انتظار" });
            li.Add(new SelectListItem { Text = "در حال بررسی", Value = "در حال بررسی" });
            li.Add(new SelectListItem { Text = "پاسخ داده شده", Value = "پاسخ داده شده" });
            ViewData["ShekayatStatus"] = li;
            ShekayatManagement tk = new ShekayatManagement();
            var model = tk.ShekayatDetail(F_ShekayatID, Token);
            if (model != null)
                ViewBag.Status = model.Status;
            return PartialView(model);
        }
        [HttpPost]
        [AuthLog]
        public ActionResult ShekayatResponse(ShekayatInOutboxDataModel model, int F_ShekayatId, string Token)
        {

            if (string.IsNullOrEmpty(model.Text))
                ModelState.AddModelError("Text", "لطفاً فیلد های خالی را پر نمایید");
            if (ModelState.IsValid)
            {
                model.F_ShekayatID = F_ShekayatId;
                ShekayatManagement tk = new ShekayatManagement();
                tk.AddShekayatOutBox(model, Token);
                ViewBag.F_LastShekayatId = F_ShekayatId;
                ViewBag.Notification = true;
            }
            else
                ViewBag.Notification = false;
            return PartialView(model);
        }

        [HttpPost]
        [AuthLog]
        public ActionResult ShekayatChangeStatus(Shekayat model, int F_ShekayatId, string Token)
        {
            try
            {
                model.ID = F_ShekayatId;
                ShekayatManagement Shekayat = new ShekayatManagement();
                Shekayat.ChangeShekayatStatus(model, Token);
                ViewBag.Notification = true;
            }
            catch
            {
                ViewBag.Notification = false;
            }

            return PartialView();
        }
    }
}