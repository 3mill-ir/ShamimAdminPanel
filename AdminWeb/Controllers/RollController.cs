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
    public class RollController : Controller
    {
        #region Roll
        [PageTittleAttributeActionFilter(Function = "Roll_List")]
        [AuthLog(Roles = "لیست دسترسی ها")]
        public async Task<ActionResult> List(string Token)
        {
            RollManagement Roll = new RollManagement();
            ViewBag.Notification = TempData["Notification"];
            return View(await Roll.ListRoll(Token));
        }
        [PageTittleAttributeActionFilter(Function = "Roll_Add")]
        [AuthLog]
        public ActionResult Add(string Token)
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PageTittleAttributeActionFilter(Function = "Roll_Add")]
        [AuthLog]
        public async Task<ActionResult> Add(AspNetRoles model, string Token)
        {
            RollManagement Roll = new RollManagement();
            string scale = await Roll.AddRoll(model, Token);
            if (scale == "OK")
            {
                TempData["Notification"] = "success";
                return RedirectToAction("List", "Roll");
            }
            else
            {
                ViewBag.Notification = "danger";
                return View(model);
            }
        }
        [HttpPost]
        [AuthLog]
        public async Task<ActionResult> DeleteRoll(string ID, string Token)
        {
            RollManagement Roll = new RollManagement();
            string scale = await Roll.DeleteRoll(ID, Token);
            if (scale == "OK")
                TempData["Notification"] = "success";
            else
                TempData["Notification"] = "danger";
            return RedirectToAction("List", "Roll");
        }
        #endregion
        #region RollGroup

        [PageTittleAttributeActionFilter(Function = "Roll_ListRollGroup")]
        [AuthLog]
        public async Task<ActionResult> ListRollGroup(string Token)
        {
            RollManagement Roll = new RollManagement();
            ViewBag.Notification = TempData["Notification"];
            return View(await Roll.ListRollGroup(Token));
        }

        [PageTittleAttributeActionFilter(Function = "Roll_ListRollsOfRollGroup")]
        [AuthLog]
        public async Task<ActionResult> ListRollsOfRollGroup(string Token, string RollGroupID)
        {
            RollManagement Roll = new RollManagement();
            ViewBag.Notification = TempData["Notification"];
            return View(await Roll.ListRollsOfRollGroup(RollGroupID, Token));
        }
        [PageTittleAttributeActionFilter(Function = "Roll_AddRollGroup")]
        [AuthLog]
        public ActionResult AddRollGroup(string Token)
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PageTittleAttributeActionFilter(Function = "Roll_AddRollGroup")]
        [AuthLog]
        public async Task<ActionResult> AddRollGroup(RollGroupe model, string Token)
        {
            RollManagement Roll = new RollManagement();
            string scale = await Roll.AddRollGroup(model, Token);
            if (scale == "OK")
            {
                TempData["Notification"] = "success";
                return RedirectToAction("ListRollGroup", "Roll");
            }
            else
            {
                ViewBag.Notification = "danger";
                return View(model);
            }
        }

        [PageTittleAttributeActionFilter(Function = "Roll_AddRollGroup")]
        [AuthLog]
        public async Task<ActionResult> AssignRollToRollGroup(string RollGroupID, string Token)
        {
            RollManagement Roll = new RollManagement();
            ViewBag.RollGroupID = RollGroupID;
            TempData["AssignedRolls"] =await Roll.ListRollsOfRollGroup(RollGroupID,Token);
            return View(await Roll.ListRoll(Token));

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PageTittleAttributeActionFilter(Function = "Roll_AddRollGroup")]
        [AuthLog]
        public async Task<ActionResult> AssignRollToRollGroup(List<AspNetRoles> model, string RollGroupID, string Token)
        {
            model.RemoveAll(item => item.Id == null);
            RollManagement Roll = new RollManagement();
            string scale = await Roll.AssignRollToRollGroup(model, RollGroupID, Token);
            TempData["Notification"] = scale == "OK" ? "success" : "danger";
            return RedirectToAction("ListRollGroup", "Roll");
        }

        [HttpPost]
        [AuthLog]
        public async Task<ActionResult> DeleteRollGroup(string ID, string Token)
        {
            RollManagement Roll = new RollManagement();
            string scale = await Roll.DeleteRollGroup(ID, Token);
            if (scale == "OK")
                TempData["Notification"] = "success";
            else
                TempData["Notification"] = "danger";
            return RedirectToAction("ListRollGroup", "Roll");
        }
        #endregion
        #region User
        [AuthLog]
        [PageTittleAttributeActionFilter(Function = "Roll_UsersManagement")]
        public async Task<ActionResult> UsersManagement(string Token)
        {
            UsersManagement UM = new UsersManagement();
            ViewBag.Notification = TempData["Notification"];
            return View(await UM.ListUserForAdmin(Token));
        }

        [PageTittleAttributeActionFilter(Function = "Roll_AssignRollToUser")]
        [AuthLog]
        public async Task<ActionResult> AssignRollToUser(string Token, string UserID)
        {
            RollManagement Roll = new RollManagement();
            ViewBag.UserID = UserID;
            TempData["AssignedRolls"] =await Roll.ListRollOfUser(Token,UserID);
            return View(await Roll.ListRoll(Token));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PageTittleAttributeActionFilter(Function = "Roll_AssignRollToUser")]
        [AuthLog]
        public async Task<ActionResult> AssignRollToUser(List<AspNetRoles> model, string F_UserID, string Token)
        {
            model.RemoveAll(item => item.Id == null);
            RollManagement Roll = new RollManagement();
            string scale = await Roll.AssignRollToUser(model, F_UserID, Token);
            TempData["Notification"] = scale == "OK" ? "success" : "danger";
            return RedirectToAction("UsersManagement", "Roll");
        }




        [PageTittleAttributeActionFilter(Function = "Roll_AssignRollGroupToUser")]
        [AuthLog]
        public async Task<ActionResult> AssignRollGroupToUser(string UserID, string Token)
        {
            RollManagement Roll = new RollManagement();
            ViewBag.UserID = UserID;
            TempData["AssignedGroupRolls"] =await Roll.ListRollGroupOfUser(Token, UserID);
            return View(await Roll.ListRollGroup(Token));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PageTittleAttributeActionFilter(Function = "Roll_AssignRollGroupToUser")]
        [AuthLog]
        public async Task<ActionResult> AssignRollGroupToUser(List<RollGroupe> model, string F_UserID, string Token)
        {
            model.RemoveAll(item => item.ID == null);
            RollManagement Roll = new RollManagement();
            string scale = await Roll.AssignRollGroupToUser(model, F_UserID, Token);
            TempData["Notification"] = scale == "OK" ? "success" : "danger";
            return RedirectToAction("UsersManagement", "Roll");
        }
        #endregion
    }
}