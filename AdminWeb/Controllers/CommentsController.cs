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
    public class CommentsController : Controller
    {
        // GET: Comments
        [AuthLog]
        [PageTittleAttributeActionFilter(Function = "Comments_DynamicPageComments")]
        public async Task<ActionResult> DynamicPageComments(string Token, int ID = -1)
        {
            ViewBag.F_MenuID = ID;
            ViewBag.Notification = TempData["Notification"];
            CommentManagement cm = new CommentManagement();
            return View(await cm.ListComments(Token, ID));
        }
        [AuthLog]
        [HttpPost]
        public async Task<ActionResult> CommentsChangeStatus(string Token, int ID, int F_MenuID)
        {
            CommentManagement cm = new CommentManagement();
            if (await cm.ChangeStatusComments(ID, Token) == "NotFound") { TempData["Notification"] = "danger"; return View("~/Views/Shared/NotFoundFailed.cshtml"); }
            TempData["Notification"] = "success";
            return RedirectToAction("DynamicPageComments", "Comments", new { ID = F_MenuID });
        }
    }
}