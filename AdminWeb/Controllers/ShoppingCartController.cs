using AdminWeb.CustomFilters;
using AdminWeb.Models.BLL;
using AdminWeb.Models.DataModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AdminWeb.Controllers
{
    public class ShoppingCartController : Controller
    {
        [PageTittleAttributeActionFilter(Function = "ShoppingCart_List")]
        [AuthLog]
        public async Task<ActionResult> List(string Token)
        {
            ShoppingCartManagement sm = new ShoppingCartManagement();
            ViewBag.Notification = TempData["Notification"];
            ViewBag.ShopStates = Tools.ShopStates();
            return View(await sm.ShopList("",1,1000000,Token));
        }

        //[PageTittleAttributeActionFilter(Function = "ShoppingCart_Edit")]
        //[AuthLog]
        //public async Task<ActionResult> Edit(int ShopID, string Token)
        //{
        //    string profile = Tools.F_UserName(Token);
        //    ViewBag.PrePath = Tools.ReturnPathPhysicalMode("ItemImagePath", "WebAddress", "ListItem", "");
        //    ShoppingCartManagement sm = new ShoppingCartManagement();
        //    var detail = await sm.ShopDetail(profile,ShopID, Token);
        //    ViewBag.ShopStates = Tools.ShopStates();
        //    return View(detail);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[PageTittleAttributeActionFilter(Function = "ShoppingCart_Edit")]
        //[AuthLog]
        //public async Task<ActionResult> Edit(Shop model, string Token)
        //{
        //    ViewBag.ShopStates = Tools.ShopStates();
        //    ShoppingCartManagement sm = new ShoppingCartManagement();
        //    string scale = await sm.EditShop(model, Token);
        //    if (scale == "OK")
        //    {
        //        TempData["Notification"] = "success";
        //        return RedirectToAction("List", "ShoppingCart");
        //    }
        //    else
        //    {
        //        ViewBag.Notification = "danger";
        //        return View(model);
        //    }
        //}

        [AuthLog]
        public ActionResult ShopDetail(int ShopID, string Token)
        {
            string profile = Tools.F_UserName(Token);
            ViewBag.PrePath = Tools.ReturnPathPhysicalMode("ItemImagePath", "WebsiteAddress", "ItemDetail", "");
            ShoppingCartManagement sm = new ShoppingCartManagement();
            var model = Task.Run(() => sm.ShopDetail(profile,ShopID,Token)).Result;
            var Result = Tools.GetObjectFromRequest(ConfigurationManager.AppSettings["APIAddress"] + "/api/account/GetUserDetailsByUserID?userId="+model.F_UserID, Token,null);
            TempData["Account"] = JsonConvert.DeserializeObject<UserInformationDataModel>(Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            return PartialView(model);
        }

        [AuthLog]
        [HttpPost]
        public ActionResult ShopChangeState(int ShopID, string Token,string State)
        {
            string profile = Tools.F_UserName(Token);
            ShoppingCartManagement sm = new ShoppingCartManagement();
            string scale = Task.Run(() => sm.ChangeStateShop(new Shop() { ID = ShopID, State = State }, Token)).Result;
            TempData["Notification"] = scale == "OK" ? "success" : "danger";
            return RedirectToAction("List", "ShoppingCart");
        }
    }
}