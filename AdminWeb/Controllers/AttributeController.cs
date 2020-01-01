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
    public class AttributeController : Controller
    {
        [PageTittleAttributeActionFilter(Function = "Attribute_List")]
        [AuthLog]
        public async Task<ActionResult> List(string Token, int ID)
        {
            AttributeManagement Attribute = new AttributeManagement();
            ViewBag.Notification = TempData["Notification"];
            return View(await Attribute.ListAttribute(Token, ID));
        }
        [PageTittleAttributeActionFilter(Function = "Attribute_ListAttributeItemAttributes")]
        [AuthLog]
        public async Task<ActionResult> ListAttributeItemAttributes(string Token, int ID,int F_AttributeGroupID)
        {
            AttributeManagement Attribute = new AttributeManagement();
            ViewBag.F_AttributeGroupID = F_AttributeGroupID;
            ViewBag.Notification = TempData["Notification"];
            return View(await Attribute.ListAttributeItemAttributes(Token, ID));
        }
        [PageTittleAttributeActionFilter(Function = "Attribute_Add")]
        [AuthLog]
        public ActionResult Add(string Token, int ID = 0, int F_AttributeItemID = 0)
        {
            ViewBag.ComponentTypes = Tools.ComponentTypes();
            return View(new AttributeDataModel() { F_AttributeGroupID = ID, F_AttributeItemID = F_AttributeItemID });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PageTittleAttributeActionFilter(Function = "Attribute_Add")]
        [AuthLog]
        public async Task<ActionResult> Add(AttributeDataModel model, HttpPostedFileBase MyFile, string Token)
        {
            AttributeManagement Attribute = new AttributeManagement();
            ViewBag.ComponentTypes = Tools.ComponentTypes();
            string scale = await Attribute.AddAttribute(model, MyFile, Token);
            if (scale == "OK")
            {
                TempData["Notification"] = "success";
                if (model.F_AttributeItemID == null || model.F_AttributeItemID == 0 )
                    return RedirectToAction("List", "Attribute", new { ID = model.F_AttributeGroupID });
                else
                    return RedirectToAction("ListAttributeItemAttributes", "Attribute", new { ID = model.F_AttributeItemID, F_AttributeGroupID=model.F_AttributeGroupID });
            }
            else
            {
                ViewBag.Notification = "danger";
                return View(model);
            }
        }

        [PageTittleAttributeActionFilter(Function = "Attribute_Edit")]
        [AuthLog]
        public async Task<ActionResult> Edit(int ID, string Token)
        {
            ViewBag.PrePath = Tools.ReturnPath("AttributeIcons", Tools.F_UserName(Token), "Edit");
            AttributeManagement Attribute = new AttributeManagement();
            var detail = await Attribute.DetailAttribute(ID, Token);
            ViewBag.ComponentTypes = Tools.ComponentTypes();
            return View(detail);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PageTittleAttributeActionFilter(Function = "Attribute_Edit")]
        [AuthLog]
        public async Task<ActionResult> Edit(AttributeDataModel model, HttpPostedFileBase MyFile, string Token)
        {
            ViewBag.PrePath = Tools.ReturnPath("AttributeIcons", Tools.F_UserName(Token), "Edit");
            ViewBag.ComponentTypes = Tools.ComponentTypes();
            AttributeManagement Attribute = new AttributeManagement();
            string scale = await Attribute.EditAttribute(model, MyFile, Token);
            if (scale == "OK")
            {
                TempData["Notification"] = "success";
                return RedirectToAction("List", "Attribute", new { ID = model.F_AttributeGroupID });
            }
            else
            {
                ViewBag.Notification = "danger";
                return View(model);
            }
        }

        [HttpPost]
        [AuthLog]
        public async Task<ActionResult> DeleteAttribute(int ID, string Token, int F_AttributeGroupID)
        {
            AttributeManagement Attribute = new AttributeManagement();
            string scale = await Attribute.DeleteAttribute(ID, Token);
            if (scale == "OK")
                TempData["Notification"] = "success";
            else
                TempData["Notification"] = "danger";
            return RedirectToAction("List", "Attribute", new { ID = F_AttributeGroupID });
        }
    }
}