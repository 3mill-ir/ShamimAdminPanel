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
    public class AttributeGroupeController : Controller
    {
        [PageTittleAttributeActionFilter(Function = "AttributeGroupe_List")]
        [AuthLog]
        public async Task<ActionResult> List(string Token,int ID)
        {
            AttributeGroupeManagement AttributeGroupe = new AttributeGroupeManagement();
            ViewBag.Notification = TempData["Notification"];
            ViewBag.F_MenuID = ID;
            return View(await AttributeGroupe.ListAttributeGroupe(Token,ID));
        }
        [PageTittleAttributeActionFilter(Function = "AttributeGroupe_Add")]
        [AuthLog]
        public ActionResult Add(string Token,int ID=-1, int F_AttributeGroup_ID =0)
        {
            AttributeGroupeManagement AttributeGroupe = new AttributeGroupeManagement();
            ViewBag.F_AttributeGroupeIDs = AttributeGroupe.F_AttributeGroupeIDs(Token, ID, F_AttributeGroup_ID);
            return View(new AttributeGroupNew() {F_MenuID= ID,F_AttributeGroupID=F_AttributeGroup_ID});
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PageTittleAttributeActionFilter(Function = "AttributeGroupe_Add")]
        [AuthLog]
        public async Task<ActionResult> Add(AttributeGroupNew model, HttpPostedFileBase MyFile, string Token)
        {
            AttributeGroupeManagement AttributeGroupe = new AttributeGroupeManagement();
            ViewBag.F_AttributeGroupeIDs = AttributeGroupe.F_AttributeGroupeIDs(Token, model.F_MenuID??default(int));
            string scale = await AttributeGroupe.AddAttributeGroupe(model, MyFile, Token);
            if (scale == "OK")
            {
                TempData["Notification"] = "success";
                return RedirectToAction("List", "AttributeGroupe",new {ID=model.F_MenuID });
            }
            else
            {
                ViewBag.Notification = "danger";
                return View(model);
            }
        }

        [PageTittleAttributeActionFilter(Function = "AttributeGroupe_Edit")]
        [AuthLog]
        public async Task<ActionResult> Edit(int ID, string Token,int F_MenuID)
        {
            ViewBag.PrePath = Tools.ReturnPath("AttributeGroupeImages", Tools.F_UserName(Token), "Edit");
            AttributeGroupeManagement AttributeGroupe = new AttributeGroupeManagement();
            var detail = await AttributeGroupe.DetailAttributeGroupe(ID, Token);
            TempData["F_MenuID"] = F_MenuID;
            return View(detail);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PageTittleAttributeActionFilter(Function = "AttributeGroupe_Edit")]
        [AuthLog]
        public async Task<ActionResult> Edit(AttributeGroupNew model, HttpPostedFileBase MyFile, string Token)
        {
            ViewBag.PrePath = Tools.ReturnPath("AttributeGroupeImages", Tools.F_UserName(Token), "Edit");
            ViewBag.Languages = Tools.LanguagesCombo(Token);
            AttributeGroupeManagement AttributeGroupe = new AttributeGroupeManagement();
            string scale = await AttributeGroupe.EditAttributeGroupe(model, MyFile, Token);
            if (scale == "OK")
            {
                TempData["Notification"] = "success";
                return RedirectToAction("List", "AttributeGroupe", new { ID = TempData["F_MenuID"] });
            }
            else
            {
                ViewBag.Notification = "danger";
                return View(model);
            }
        }

        [HttpPost]
        [AuthLog]
        public async Task<ActionResult> DeleteAttributeGroupe(int ID, string Token,int F_MenuID)
        {
            AttributeGroupeManagement AttributeGroupe = new AttributeGroupeManagement();
            string scale = await AttributeGroupe.DeleteAttributeGroupe(ID, Token);
            if (scale == "OK")
                TempData["Notification"] = "success";
            else
                TempData["Notification"] = "danger";
            return RedirectToAction("List", "AttributeGroupe",new {ID= F_MenuID });
        }
    }
}