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
    public class GalleryController : Controller
    {
        [AuthLog]
        [PageTittleAttributeActionFilter(Function = "AddPhoto_Gallery")]
        public ActionResult AddFile(string Token)
        {
            FolderManagement FM = new FolderManagement(Tools.F_UserName(Token));
            ViewBag.ListFolders = FM.LoadListFolders();
            return View();
        }
        [HttpPost]
        [AuthLog]
        [PageTittleAttributeActionFilter(Function = "AddPhoto_Gallery")]
        public ActionResult AddFile(GalleryModelAdmin model, HttpPostedFileBase AttachmentFile, string Token)
        {
            GalleryManagement GM = new GalleryManagement(Tools.F_UserName(Token));
            if (AttachmentFile == null)
            {
                ViewBag.Notification = "لطفاً عکس مور نظر را انتخاب کنید.";
                FolderManagement FM = new FolderManagement(Tools.F_UserName(Token));
                ViewBag.ListFolders = FM.LoadListFolders();
                return View(model);
            }
            if (GM.AddImage(model, AttachmentFile,Token))
                TempData["JSNotifyMsg"] = "success";
            else
                TempData["JSNotifyMsg"] = "error";
            return RedirectToAction("ListFile");
        }
        [AuthLog]
        [PageTittleAttributeActionFilter(Function = "ListPhoto_Gallery")]
        public ActionResult ListFile(string Token)
        {
            ViewBag.Notification = TempData["JSNotifyMsg"];
            string F_UserName = Tools.F_UserName(Token);
            GalleryManagement GM = new GalleryManagement(F_UserName);
            ViewBag.F_PipoUserId = F_UserName;
            return View(GM.LoadGalleryListPhotos(Token));
        }
        [AuthLog]
        [PageTittleAttributeActionFilter(Function = "EditPhoto_Gallery")]
        public ActionResult EditFile(int PhotoID, string Token)
        {
            string F_UserName = Tools.F_UserName(Token);
            GalleryManagement GM = new GalleryManagement(F_UserName);
            FolderManagement FM = new FolderManagement(F_UserName);
            ViewBag.F_PipoUserId = F_UserName;
            var listItems = new List<SelectListItem>();
            foreach (var item in FM.LoadListFolders())
            {
                listItems.Add(new SelectListItem { Text = item, Value = item });
            }
            ViewBag.Types = listItems;
            var photo = GM.LoadPhotos(Token).FirstOrDefault(u => u.ID == PhotoID);
            if (photo == null)
            {
                return View("~/Views/Shared/NotFoundFailed.cshtml");
            }
            ViewBag.PhotoPath = Tools.ReturnPath("GalleryPath", F_UserName, "EditFile()") + "/" + photo.Type + "/" + photo.Path;
            return View(photo);

        }
        [HttpPost]
        [AuthLog]
        [PageTittleAttributeActionFilter(Function = "EditPhoto_Gallery")]
        public ActionResult EditFile(GalleryModelAdmin model, string Token)
        {
            TempData["JSNotifyMsg"] = "success";
            GalleryManagement GM = new GalleryManagement(ViewBag.UserName);
            GM.EditImage(model,Token);
            return RedirectToAction("ListFile");

        }
        [HttpPost]
        [AuthLog]
        public ActionResult DeleteFile(int FileID, string Token)
        {
            GalleryManagement GAL = new GalleryManagement(Tools.F_UserName(Token));
            GAL.DeleteImage(FileID,Token);
            return RedirectToAction("ListFile");
        }





        #region Folder
        [AuthLog]
        [PageTittleAttributeActionFilter(Function = "ManageFolder_Gallery")]
        public ActionResult ManageFolder(string Token)
        {
            FolderManagement FM = new FolderManagement(Tools.F_UserName(Token));
            return View(FM.ManageListFolders(Token));
        }

        [HttpPost]
        [AuthLog]
        [PageTittleAttributeActionFilter(Function = "AddFolder_Gallery")]
        public ActionResult AddFolder(FolderModel model, string Token)
        {
            FolderManagement FM = new FolderManagement(Tools.F_UserName(Token));
            FM.AddFolder(model.FolderName);
            return RedirectToAction("ManageFolder");
        }
        [PageTittleAttributeActionFilter(Function = "EditFolder_Gallery")]
        [AuthLog]
        public ActionResult EditFolder(string FolderName)
        {
            FolderModel model = new FolderModel();
            model.FolderName = FolderName;
            return View(model);
        }
        [HttpPost]
        [AuthLog]
        [PageTittleAttributeActionFilter(Function = "EditFolder_Gallery")]
        public ActionResult EditFolder(FolderModel model, string Token)
        {
            FolderManagement FM = new FolderManagement(Tools.F_UserName(Token));
            FM.EditFolder(model,Token);
            return RedirectToAction("ManageFolder");
        }
        [HttpPost]
        [AuthLog]
        public ActionResult DeleteFolder(string FolderName, string Token)
        {
            FolderManagement FM = new FolderManagement(Tools.F_UserName(Token));
            FM.DeleteFolders(FolderName,Token);
            return RedirectToAction("ManageFolder");
        }
        #endregion
    }
}