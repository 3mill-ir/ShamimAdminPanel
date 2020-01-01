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
    public class DynamicPageController : Controller
    {
        [PageTittleAttributeActionFilter(Function = "DynamicPage_PostList")]
        [AuthLog]
        public async Task<ActionResult> PostList( string Token, string FromDate, string ToDate, string SearchText = "",string sortby= "CreatedOnUTC Descending", string Lang = "FA", int? F_MenuID = null, int? PageSize = 10, int? Type = 1, int? page = 1)
        {
            var Langs = Tools.LanguagesCombo(Token);
            ViewBag.F_MenuIDs = Tools.F_MenuIDs(ViewBag.RootUserName as string,new List<string> {"DynamicPost" } ,Langs.Select(u=>u.Value).ToList(), Token,null,1);
            ViewBag.Languages = Langs;
            ViewBag.Notification = TempData["Notification"];
            if (string.IsNullOrEmpty(FromDate))
                FromDate = "1370/01/01";
            if (string.IsNullOrEmpty(ToDate))
                ToDate = "1450/01/01";
            DateTime From; DateTime To;
            Tools.GetJalaliDateReturnDateTime(FromDate + " 00:00:00", out From);
            Tools.GetJalaliDateReturnDateTime(ToDate + " 00:00:00", out To);
            FilterPostDatamodel model = new FilterPostDatamodel() { FromTime = From, Language = Lang, MenuId = F_MenuID ?? default(int), PageNumber = page ?? default(int), PageSize = PageSize ?? default(int), search = SearchText, ToTime = To, type = Type ?? default(int),sortby=sortby };
            DynamicPageManagement DPM = new DynamicPageManagement();
            var result = await DPM.ListPost(model, Token);
            int total = result.count;
            var pagedList = new StaticPagedList<PostDataModel>(result.List, page ?? default(int), PageSize ?? default(int), total);
            ViewBag.Total = total;
            ViewBag.PageSize = PageSize;
            ViewBag.Type = Type;
            ViewBag.FromDate = FromDate;
            ViewBag.ToDate = ToDate;
            ViewBag.page = page;
            ViewBag.SearchText = SearchText;
            ViewBag.Lang = Lang;
            ViewBag.F_MenuID = F_MenuID;
            ViewBag.sortby = sortby;
            return View(pagedList);
        }
    
        

        [PageTittleAttributeActionFilter(Function = "DynamicPage_PostCreate")]
        [AuthLog]
        public ActionResult PostCreate(string Token)
        {
            string F_UserName = ViewBag.RootUserName;
            ViewBag.UserImageFolder = Tools.ReturnPath("DynamicPageContentImages", F_UserName, "PostCreate()");
            var Langs = Tools.LanguagesCombo(Token);
            ViewBag.F_MenuIDs = Tools.F_MenuIDs(F_UserName as string, new List<string> { "DynamicPost" }, Langs.Select(u => u.Value).ToList(), Token,null,null);
            ViewBag.Languages = Langs;
            return View();
        }
        [HttpPost]
        [AuthLog]
        [PageTittleAttributeActionFilter(Function = "DynamicPage_PostCreate")]
        public async Task<ActionResult> PostCreate(PostDataModel model, HttpPostedFileBase MyFile, string Token)
        {
            string F_UserName = ViewBag.RootUserName;

            if (ModelState.IsValid)
            {

                model.Detail = Tools.SaveHtmlDetail(model.Detail, F_UserName);
                DynamicPageManagement dpm = new DynamicPageManagement();
                int scale = await dpm.AddPost(model, MyFile, Token, F_UserName);
                if (scale > 0)
                {
                    TagsManagement tm = new TagsManagement();
                    await tm.AddTag(model.Tags, scale, Token);
                    TempData["Notification"] = "success";
                    return RedirectToAction("PostList", "DynamicPage");
                }
            }

            ViewBag.UserImageFolder = Tools.ReturnPath("DynamicPageContentImages", F_UserName, "PostCreate()");
            var Langs = Tools.LanguagesCombo(Token);
                ViewBag.F_MenuIDs = Tools.F_MenuIDs(F_UserName, new List<string> { "DynamicPost" }, Langs.Select(u => u.Value).ToList(), Token,null,null);
                ViewBag.Languages = Langs;
                ViewBag.Notification = "danger";
                return View(model);
        }


        [PageTittleAttributeActionFilter(Function = "DynamicPage_PostEdit")]
        [AuthLog]
        public async Task<ActionResult> PostEdit(int ID, string Token)
        {
            TagsManagement tm = new TagsManagement();
            DynamicPageManagement dpm = new DynamicPageManagement();
            var model = await dpm.DetailPost(ID, Token);
            model.Tags = await tm.GetPostTags(ID, Token);
            string F_UserName = ViewBag.RootUserName;
            model.HTMLname = model.Detail;
            model.Detail = Tools.GetHtmldetail(model.HTMLname, F_UserName);
            ViewBag.UserImageFolder = Tools.ReturnPath("DynamicPageContentImages", F_UserName, "PostCreate()");
            var Langs = Tools.LanguagesCombo(Token);
            ViewBag.F_MenuIDs = Tools.F_MenuIDs(F_UserName, new List<string> { "DynamicPost" }, new List<string> { model.Language}, Token,model.F_MenuID,null);
            ViewBag.Languages = Langs;

            return View(model);
        }
        [HttpPost]
        [PageTittleAttributeActionFilter(Function = "DynamicPage_PostEdit")]
        [AuthLog]
        public async Task<ActionResult> PostEdit(PostDataModel model, HttpPostedFileBase MyFile, string Token,string PostJalaliDate)
        {
            string F_UserName = ViewBag.RootUserName;
            DateTime start = new DateTime();
            if (Tools.GetJalaliDateReturnDateTime(PostJalaliDate, out start))
                model.CreatedOnUTC = start;
            if (ModelState.IsValid)
            {
                model.Detail = Tools.SaveHtmlDetail(model.Detail, F_UserName,model.HTMLname);
                DynamicPageManagement dpm = new DynamicPageManagement();
                string scale = await dpm.EditPost(model, MyFile, Token, F_UserName);
                TagsManagement tm = new TagsManagement();
                await tm.EditTag(model.Tags, model.ID, Token);
                if (scale == "OK")
                {
                    TempData["Notification"] = "success";
                    return RedirectToAction("PostList", "DynamicPage");
                }
            }

            ViewBag.UserImageFolder = Tools.ReturnPath("DynamicPageContentImages", F_UserName, "PostCreate()");
            var Langs = Tools.LanguagesCombo(Token);
            ViewBag.F_MenuIDs = Tools.F_MenuIDs(F_UserName, new List<string> { "DynamicPost" }, new List<string> { model.Language}, Token, model.F_MenuID,null);
            ViewBag.Languages = Langs;

            ViewBag.Notification = "danger";
                return View(model);
           
        }

        [HttpPost]
        [AuthLog]
        public async Task<ActionResult> PostChangeStatus(int ID, int Page, string Token)
        {
            DynamicPageManagement dpm = new DynamicPageManagement();
            string scale = await dpm.ChangeStatusPost(ID, Token);
            if (scale == "OK")
                TempData["Notification"] = "success";
            else
                TempData["Notification"] = "danger";
            return RedirectToAction("PostList", "DynamicPage");
        }


        [HttpPost]
        [AuthLog]
        public async Task<ActionResult> PostDelete(int ID, int Page, string Token)
        {
            DynamicPageManagement dpm = new DynamicPageManagement();
            string scale = await dpm.DeletePost(ID, Token);
            if (scale == "OK")
                TempData["Notification"] = "success";
            else
                TempData["Notification"] = "danger";
            return RedirectToAction("PostList", "DynamicPage");
        }

        //[HttpPost]
        //[AuthLog]
        //public async Task<ActionResult> PostEdit(int ID, int Page, string Token)
        //{
        //    DynamicPageManagement dpm = new DynamicPageManagement();
        //    string scale = await dpm.DeletePost(ID, Token);
        //    if (scale == "OK")
        //        TempData["Notification"] = "success";
        //    else
        //        TempData["Notification"] = "danger";
        //    return RedirectToAction("PostList", "DynamicPage");
        //}

    }
}