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
    public class TicketController : Controller
    {
        [PageTittleAttributeActionFilter(Function = "Ticket_TicketManagement")]
        [AuthLog]
        public ActionResult Index()
        {
            return View();
        }
        [AuthLog]
        public ActionResult TicketAccessory(string Token)
        {
            TicketManagement tk = new TicketManagement();
            return PartialView(tk.TicketAccessory(Token));
        }

        [HttpPost]
        [AuthLog]
        public ActionResult TicketList(string Token, string currentFilter, string searchString, string TicketStatus, int page = 1)
        {
            TicketManagement Ticket = new TicketManagement();
            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;
            ViewBag.CurrentFilter = searchString;
            ViewBag.CurrentStatus = TicketStatus;
            ViewBag.mypage = page;
            var temp = Ticket.TicketList(Token, currentFilter, searchString, TicketStatus, page);
            int total = temp.Total;
            var pagedList = new StaticPagedList<TicketModel>(temp.Tickets, page, 10, total);
            return PartialView(pagedList);
        }
        [AuthLog]
        public ActionResult TicketDetail(string F_TicketID, string Token)
        {
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "انتخاب", Value = "" });
            li.Add(new SelectListItem { Text = "در وضعیت انتظار", Value = "در وضعیت انتظار" });
            li.Add(new SelectListItem { Text = "در حال بررسی", Value = "در حال بررسی" });
            li.Add(new SelectListItem { Text = "پاسخ داده شده", Value = "پاسخ داده شده" });
            ViewData["TicketStatus"] = li;
            TicketManagement tk = new TicketManagement();
            var model = tk.TicketDetail(F_TicketID, Token);
            if (model != null)
                ViewBag.Status = tk.TicketBrief(model.FirstOrDefault().F_Ticket_Id, Token).Status;
            return PartialView(tk.TicketDetail(F_TicketID, Token));
        }
        [HttpPost]
        [AuthLog]
        public ActionResult TicketResponse(TicketOutBoxModel model, int F_LastTicketInboxId, string Token)
        {

            if (string.IsNullOrEmpty(model.Content_One))
                ModelState.AddModelError("Content_One", "لطفاً فیلد های خالی را پر نمایید");
            if (ModelState.IsValid)
            {
                TicketManagement tk = new TicketManagement();
                tk.AddTicketOutBox(model, Token);
                ViewBag.F_LastTicketId = F_LastTicketInboxId;
                ViewBag.Notification = true;
            }
            else
                ViewBag.Notification = false;
            return PartialView(model);
        }

        [HttpPost]
        [AuthLog]
        public ActionResult TicketChangeStatus(TicketModel model, int F_TicketId, string Token)
        {
            try
            {
                model.ID = F_TicketId;
                TicketManagement ticket = new TicketManagement();
                ticket.ChangeTicketStatus(model, Token);
                ViewBag.Notification = true;
            }
            catch
            {
                ViewBag.Notification = false;
            }

            return PartialView();
        }

        //#region TicketChart
        //[AuthLog]
        //public ActionResult EditTicketChart(string Token)
        //{
        //    TicketManagement TK = new TicketManagement(Tools.F_UserName());
        //    var listItems = new List<SelectListItem>();
        //    listItems.Add(new SelectListItem { Text = "نمودار دایره ای", Value = "PieChart" });
        //    listItems.Add(new SelectListItem { Text = "نمودار حلقه ای", Value = "DoughnutChart" });
        //    listItems.Add(new SelectListItem { Text = "نمودار میله ای", Value = "BarChart" });
        //    ViewBag.ChartTypes = listItems;
        //    return View(TK.LoadTicketChart());
        //}
        //[HttpPost]
        //[AuthLog]
        //public ActionResult EditTicketChart(TicketChartModel model, string Token)
        //{
        //    TicketManagement TK = new TicketManagement(Tools.F_UserName(Token));
        //    TK.EditTicketChart(model);
        //    return RedirectToAction("EditTicketChart", "Tickets");
        //}
        //#endregion
    }
}