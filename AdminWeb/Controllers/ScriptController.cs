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
    public class ScriptController : Controller
    {
        [PageTittleAttributeActionFilter(Function = "EditScripts_Script")]
        [AuthLog]
        public ActionResult EditScripts(string Token)
        {
            ViewBag.Notification = TempData["JSNotifyMsg"];
            ScriptManagement LM = new ScriptManagement(Tools.F_UserName(Token));
            return View(LM.LoadScripts());
        }
        [HttpPost]
        [AuthLog]
        public ActionResult EditScriptsPost(ScriptsModel model, string Token)
        {
            TempData["JSNotifyMsg"] = "success";
            ScriptManagement LM = new ScriptManagement(Tools.F_UserName(Token));
            LM.EditScripts(model);
            return RedirectToAction("EditScripts", "Script");
        }
    }
}