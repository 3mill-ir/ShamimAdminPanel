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
    public class AttributeItemController : Controller
    {
        [PageTittleAttributeActionFilter(Function = "AttributeItem_List")]
        [AuthLog]
        public async Task<ActionResult> List(string Token, int ID, int F_AttributeGroupID)
        {
            AttributeItemManagement AttributeItem = new AttributeItemManagement();
            ViewBag.Notification = TempData["Notification"];
            ViewBag.F_AttributeGroupID = F_AttributeGroupID;
            return View(await AttributeItem.ListAttributeItem(Token, ID));
        }
        [PageTittleAttributeActionFilter(Function = "AttributeItem_Add")]
        [AuthLog]
        public ActionResult Add(string Token, int ID, int F_AttributeGroupID)
        {
            ViewBag.F_AttributeGroupID = F_AttributeGroupID;
            return View(new AttributeItemDataModel() { F_AttributeID = ID });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PageTittleAttributeActionFilter(Function = "AttributeItem_Add")]
        [AuthLog]
        public async Task<ActionResult> Add(AttributeItemDataModel model, string Token, int F_AttributeGroupID)
        {
            AttributeItemManagement AttributeItem = new AttributeItemManagement();
            string scale = await AttributeItem.AddAttributeItem(model, Token);
            if (scale == "OK")
            {
                TempData["Notification"] = "success";
                return RedirectToAction("List", "AttributeItem", new { ID = model.F_AttributeID, F_AttributeGroupID = F_AttributeGroupID });
            }
            else
            {
                ViewBag.Notification = "danger";
                return View(model);
            }
        }

        [PageTittleAttributeActionFilter(Function = "AttributeItem_Edit")]
        [AuthLog]
        public async Task<ActionResult> Edit(int ID, string Token, int F_AttributeGroupID)
        {
            ViewBag.F_AttributeGroupID = F_AttributeGroupID;
            AttributeItemManagement AttributeItem = new AttributeItemManagement();
            var detail = await AttributeItem.DetailAttributeItem(ID, Token);
            return View(detail);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PageTittleAttributeActionFilter(Function = "AttributeItem_Edit")]
        [AuthLog]
        public async Task<ActionResult> Edit(AttributeItemDataModel model, string Token, int F_AttributeGroupID)
        {
            AttributeItemManagement AttributeItem = new AttributeItemManagement();
            string scale = await AttributeItem.EditAttributeItem(model, Token);
            if (scale == "OK")
            {
                TempData["Notification"] = "success";
                return RedirectToAction("List", "AttributeItem", new { ID = model.F_AttributeID, F_AttributeGroupID = F_AttributeGroupID });
            }
            else
            {
                ViewBag.Notification = "danger";
                return View(model);
            }
        }

        [HttpPost]
        [AuthLog]
        public async Task<ActionResult> DeleteAttributeItem(int ID, string Token, int F_AttributeID, int F_AttributeGroupID)
        {
            AttributeItemManagement AttributeItem = new AttributeItemManagement();
            string scale = await AttributeItem.DeleteAttributeItem(ID, Token);
            TempData["Notification"] = scale == "OK" ? "success" : "danger";
            return RedirectToAction("List", "AttributeItem", new { ID = F_AttributeID, F_AttributeGroupID = F_AttributeGroupID });
        }
    }
}