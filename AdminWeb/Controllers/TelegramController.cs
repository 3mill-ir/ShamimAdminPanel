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
    public class TelegramController : Controller
    {
        [PageTittleAttributeActionFilter(Function = "TelegramManagementPannel_Telegram")]
        [AuthLog]
        public ActionResult TelegramManagementPannel()
        {
            return View();
        }
        public PartialViewResult SendMessage()
        {
            return PartialView();
        }

        public PartialViewResult SendPhoto()
        {
            return PartialView();
        }

        public PartialViewResult SendVideo()
        {
            return PartialView();
        }

        public PartialViewResult SendVoice()
        {
            return PartialView();
        }

        public PartialViewResult SendDocument()
        {
            return PartialView();
        }

        [HttpPost]
        public PartialViewResult SendMessage(TelegramModel model)
        {
            if (model.Caption == null)
            {
                ModelState.AddModelError("Caption", @Resource.Resource.View_ValidationError);
                return PartialView(model);
            }

            TelegramBot t = new TelegramBot();
            string IsOk = t.SendMessage(model.Caption);
            if (IsOk == "true")
            {
                return PartialView("TelegramSuccess");
            }
            else
            {
                return PartialView("TelegramFailed");
            }
        }

        [HttpPost]
        public ActionResult SendPhoto(TelegramModel model, HttpPostedFileBase Path)
        {
            TelegramBot Telegram = new TelegramBot();
            if (Path == null)
            {
                ModelState.AddModelError("Path", @Resource.Resource.View_ValidationError);
                return PartialView(model);
            }

            if (model.Caption != null)
            {
                //agar tule caption bishtar az 199 character bashad be surate payami joda az tasvir ersal mishavad
                // be dalile mahdudiyyate telegram
                if (model.Caption.Length > 199)
                {
                    bool Allresult = false; ;
                    bool result1 = false;
                    bool result2 = false;
                    string temp = model.Caption;
                    model.Caption = "ادامه خبر ... 👇👇👇👇👇👇👇";
                    result1 = Telegram.SendPhoto(model, Path).Equals("true") ? true : false;
                    result2 = Telegram.SendMessage(temp).Equals("true") ? true : false;
                    if (result1 && result2)
                        Allresult = true;
                    if (Allresult)
                    {
                        return PartialView("TelegramSuccess");
                    }
                    else
                    {
                        return View("TelegramFailed");
                    }
                }

            }

            string IsOk = Telegram.SendPhoto(model, Path);
            if (IsOk == "true")
            {
                return PartialView("TelegramSuccess");
            }
            else
            {
                return PartialView("TelegramFailed");
            }
        }

        [HttpPost]
        public ActionResult SendVideo(TelegramModel model, HttpPostedFileBase Path)
        {
            TelegramBot Telegram = new TelegramBot();
            if (Path == null)
            {
                ModelState.AddModelError("Path", @Resource.Resource.View_ValidationError);
                return PartialView(model);
            }

            if (model.Caption != null)
            {
                //agar tule caption bishtar az 199 character bashad be surate payami joda az tasvir ersal mishavad
                // be dalile mahdudiyyate telegram
                if (model.Caption.Length > 199)
                {
                    bool Allresult = false; ;
                    bool result1 = false;
                    bool result2 = false;
                    string temp = model.Caption;
                    model.Caption = "ادامه خبر ... 👇👇👇👇👇👇👇";
                    result1 = Telegram.SendVideo(model, Path).Equals("true") ? true : false;
                    result2 = Telegram.SendMessage(temp).Equals("true") ? true : false;
                    if (result1 && result2)
                        Allresult = true;
                    if (Allresult)
                    {
                        return PartialView("TelegramSuccess");
                    }
                    else
                    {
                        return View("TelegramFailed");
                    }
                }

            }

            string IsOk = Telegram.SendVideo(model, Path);
            if (IsOk == "true")
            {
                return PartialView("TelegramSuccess");
            }
            else
            {
                return PartialView("TelegramFailed");
            }

        }

        [HttpPost]
        public ActionResult SendVoice(TelegramModel model, HttpPostedFileBase Path)
        {
            TelegramBot Telegram = new TelegramBot();
            if (Path == null)
            {
                ModelState.AddModelError("Path", @Resource.Resource.View_ValidationError);
                return PartialView(model);
            }
            bool Allresult = false; ;
            bool result1 = false;
            bool result2 = false;
            string temp = model.Caption;
            model.Caption = "ادامه خبر ... 👇👇👇👇👇👇👇";
            result1 = Telegram.SendVoice(model, Path).Equals("true") ? true : false;
            result2 = Telegram.SendMessage(temp).Equals("true") ? true : false;
            if (result1 && result2)
                Allresult = true;
            if (Allresult)
            {
                return PartialView("TelegramSuccess");
            }
            else
            {
                return View("TelegramFailed");
            }
        }

        [HttpPost]
        public ActionResult SendDocument(TelegramModel model, HttpPostedFileBase Path)
        {
            TelegramBot Telegram = new TelegramBot();
            if (Path == null)
            {
                ModelState.AddModelError("Path", @Resource.Resource.View_ValidationError);
                return PartialView(model);
            }


            bool Allresult = false; ;
            bool result1 = false;
            bool result2 = false;
            string temp = model.Caption;
            model.Caption = "ادامه خبر ... 👇👇👇👇👇👇👇";
            result1 = Telegram.SendDocument(model, Path).Equals("true") ? true : false;
            result2 = Telegram.SendMessage(temp).Equals("true") ? true : false;
            if (result1 && result2)
                Allresult = true;
            if (Allresult)
            {
                return PartialView("TelegramSuccess");
            }
            else
            {
                return View("TelegramFailed");
            }

        }
        public ActionResult SentContentToTelegram(string Type, string Token)
        {
            TelegramModel model = new TelegramModel();
            model.Caption = TempData["Caption"].ToString();
            model.Path = TempData["ImgPath"].ToString();
            string tempURL = System.Configuration.ConfigurationManager.AppSettings["WebSiteURL"] + TempData["URL4Chanel"].ToString();
            TelegramBot Telegram = new TelegramBot();
            //agar tule caption bishtar az 199 character bashad be surate payami joda az tasvir ersal mishavad
            // be dalile mahdudiyyate telegram
            if ((model.Caption.Length + tempURL.Length) > 199)
            {
                string temp = model.Caption;
                model.Caption = "ادامه خبر ... 👇👇👇👇👇👇👇" + "\n" + tempURL;
                Telegram.SendContent(model, Token);
                Telegram.SendMessage(temp);
            }
            else
            {
                model.Caption = model.Caption + "\n" + "ادامه خبر ... 👇👇👇👇👇👇👇" + "\n" + tempURL;
                Telegram.SendContent(model, Token);
            }
            return RedirectToAction("SelectListPost", "Post", new { SubCatId = Type });
        }

        [AuthLog]
        public ActionResult SentContentToTelegramFromList(TelegramModelFromList model, string Token)
        {
            TelegramModel _model = new TelegramModel();
            string result = "";
            _model.Caption = model.Caption;
            _model.Path = model.Path;
            TelegramBot Telegram = new TelegramBot();
            if ((_model.Caption.Length + model.Url.Length) > 150)
            {

                string temp = _model.Caption;
                _model.Caption = "ادامه خبر ... 👇👇👇👇👇👇👇" + "\n" + model.Url;
                result = Telegram.SendContent(_model, Token);
                result = Telegram.SendMessage(temp);
            }
            else
            {
                _model.Caption = _model.Caption + "\n" + "ادامه خبر ... 👇👇👇👇👇👇👇" + "\n" + model.Url;
                result = Telegram.SendContent(_model, Token);
            }
            TempData["Notification"] = result != "NOK" ? "success" : "تصویر پیوست موجود نیست !";
            return RedirectToAction("PostList", "DynamicPage");
        }
    }
}