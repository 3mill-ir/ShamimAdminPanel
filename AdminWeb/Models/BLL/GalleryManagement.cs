using AdminWeb.Models.DataModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace AdminWeb.Models.BLL
{
    public class GalleryManagement
    {
        public GalleryManagement(string profile)
        {
            F_UserName = profile;
            FullPath = Tools.ReturnPathPhysicalMode("GalleryPath", F_UserName, "GalleryManagement()");
            Path = Tools.ReturnPath("GalleryPath", F_UserName, "GalleryManagement()");
        }
        string FullPath { get; set; }
        string F_UserName { get; set; }
        string Path { get; set; }



        #region Admin
        public bool AddImage(GalleryModelAdmin model, HttpPostedFileBase Img, string Token)
        {

            string path = FullPath + model.Type;
            if (System.IO.Directory.Exists(path))
            {
                model.Path = Tools.FileSave(Img, FullPath + model.Type+"\\");
                List<GalleryModelAdmin> list = new List<GalleryModelAdmin>();
                list.AddRange(LoadPhotos(Token));
                model.ID = (list.Count > 0) ? model.ID = list.Max(u => u.ID) + 1 : 0;
                list.Add(model);
                SaveChangesImages(list,Token);
                return true;
            }
            return false;
        }
        public void DeleteImage(int ID, string Token)
        {
            List<GalleryModelAdmin> list = new List<GalleryModelAdmin>();
            list.AddRange(LoadPhotos(Token));
            var Found = list.FirstOrDefault(u => u.ID == ID);
            string F_UserName = Tools.F_UserName(Token);
            string path = FullPath + Found.Type + "/" + Found.Path;
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            list.Remove(Found);
            SaveChangesImages(list,Token);
        }

        public void EditImage(GalleryModelAdmin model, string Token)
        {
            List<GalleryModelAdmin> list = new List<GalleryModelAdmin>();
            list.AddRange(LoadPhotos(Token));
            var Found = list.FirstOrDefault(u => u.ID == model.ID);
            if (Found != null)
            {
                Found.Text = model.Text;
                Found.Type = model.Type;
       
                string path = FullPath;
                if (System.IO.File.Exists(path + model.BackupType + "/" + model.Path))
                    System.IO.File.Move(path + model.BackupType + "/" + model.Path, path + model.Type + "/" + model.Path);
                SaveChangesImages(list,Token);
            }
        }
        public void SaveChangesImages(List<GalleryModelAdmin> model, string Token)
        {
            var serializer = new XmlSerializer(model.GetType());
            string F_UserName = Tools.F_UserName(Token);
            string path = FullPath;
            using (var writer = XmlWriter.Create(path + "/" + F_UserName + "_ImagesFile.xml"))
            {
                serializer.Serialize(writer, model);
            }
        }
        public List<GalleryModelAdmin> LoadPhotos(string Token)
        {
            string F_UserName = Tools.F_UserName(Token);
            List<GalleryModelAdmin> OBj = new List<GalleryModelAdmin>();
            string path = FullPath + F_UserName + "_ImagesFile.xml";
            if (System.IO.File.Exists(path))
            {
                var serializer = new XmlSerializer(typeof(List<GalleryModelAdmin>));
                using (var reader = XmlReader.Create(path))
                {
                    OBj = (List<GalleryModelAdmin>)serializer.Deserialize(reader);
                    return OBj;
                }
            }
            else
            {
                return OBj;
            }
        }

        public List<GalleryListModel> LoadGalleryListPhotos(string Token)
        {
            List<GalleryListModel> Result = new List<GalleryListModel>();
            List<GalleryModelAdmin> OBj = new List<GalleryModelAdmin>();
            FolderManagement FM = new FolderManagement(F_UserName);
            OBj.AddRange(LoadPhotos(Token));
            if (OBj.Count > 0)
            {
                foreach (var q in FM.LoadListFolders())
                {
                    GalleryListModel gal = new GalleryListModel();
                    gal.FolderName = q;
                    foreach (var item in OBj.Where(u => u.Type == q))
                    {
                        item.Path = Tools.ReturnPath("GalleryPath", F_UserName, "LoadGalleryListPhotos()") + item.Type + "/" + item.Path;
                        item.BackupType = Tools.GetFileType(item.Path.Substring(item.Path.LastIndexOf('.') + 1));
                        gal.Folders.Add(item);
                    }
                    Result.Add(gal);
                }
            }
            return Result;
        }

        #endregion



        #region user
        public List<GalleryModelAdmin> GetGalleryByFolderName(string FolderName, int pageNumber, int pageSize, out int total)
        {
            // sub string baraye in ast ke alamate ~ az avvale masir hazf shavad ta ax ha nemayesh dade shavand
            var temp = UserLoadPhotos().Where(u => u.Type == FolderName).ToPagedList(pageNumber, pageSize);
            total = temp.TotalItemCount;
            foreach (var item in temp)
            {
                item.Path = item.Type + "/" + item.Path;
            }
            return temp.ToList();
        }

        public List<GalleryModelAdmin> UserLoadPhotos()
        {
            List<GalleryModelAdmin> OBj = new List<GalleryModelAdmin>();
            string path = FullPath + F_UserName + "_ImagesFile.xml";
            if (System.IO.File.Exists(path))
            {
                var serializer = new XmlSerializer(typeof(List<GalleryModelAdmin>));
                using (var reader = XmlReader.Create(path))
                {
                    OBj = (List<GalleryModelAdmin>)serializer.Deserialize(reader);
                    return OBj;
                }
            }
            else
                return OBj;
        }
        #endregion
    }
}