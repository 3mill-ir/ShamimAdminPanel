using AdminWeb.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace AdminWeb.Models.BLL
{
    public class FolderManagement
    {
        public FolderManagement(string profile)
        {
            F_UserName = profile;
            Path = Tools.ReturnPathPhysicalMode("GalleryPath", F_UserName, "FolderManagement()");
        }
        string Path { get; set; }
        string F_UserName { get; set; }



        #region Admin
        public void AddFolder(string FolderName)
        {

            string path = Path + FolderName;
            if (!System.IO.Directory.Exists(path))
            {
                List<string> plan = new List<string>();
                plan = LoadListFolders();
                plan.Add(FolderName);
                SaveChangesFolders(plan);
                //chon SaveChangesFolders baraye edit ham estefade mishavad tabe CreateFolder ro inja seda zadim
                CreateFolder(FolderName);
            }
        }

        public List<string> LoadListFolders()
        {

            List<string> OBj = new List<string>();
            string path = Path + F_UserName + "_FoldersFile.xml";
            if (System.IO.File.Exists(path))
            {
                var serializer = new XmlSerializer(typeof(List<string>));
                using (var reader = XmlReader.Create(path))
                {
                    OBj = (List<string>)serializer.Deserialize(reader);
                    return OBj;
                }
            }
            else
            {
                return OBj;
            }
        }

        //ba file e modiriyate Folder ha sarokar arad
        private void SaveChangesFolders(List<string> InsertedPlans)
        {
            var serializer = new XmlSerializer(InsertedPlans.GetType());


            string path = Path + F_UserName + "_FoldersFile.xml";
            using (var writer = XmlWriter.Create(path))
            {
                serializer.Serialize(writer, InsertedPlans);
            }
        }

        //Folder haye jadid ra misazad
        private void CreateFolder(string FolderName)
        {
            string path = Path + FolderName;

            System.IO.Directory.CreateDirectory(path);

        }

        public void EditFolder(FolderModel FM,string Token)
        {
            List<string> model = new List<string>();
            model = LoadListFolders();
            var FoundedObject = model.FirstOrDefault(w => w == FM.FolderName);
            if (FoundedObject != null)
            {
                model.Remove(FoundedObject);
                model.Add(FM.InsertedFolderName);
                SaveChangesFolders(model);
            }
            string path = Path;
            if (System.IO.Directory.Exists(path + FM.FolderName))
                System.IO.Directory.Move(path + FM.FolderName, path + FM.InsertedFolderName);
            List<GalleryModelAdmin> list = new List<GalleryModelAdmin>();
            GalleryManagement GM = new GalleryManagement(F_UserName);
            list.AddRange(GM.LoadPhotos(Token));
            var items = list.Where(u => u.Type == FM.FolderName);
            //If baraye in ast ke tabe SaveChangesImages bikhod ejra nashavad
            if (items.Count() > 0)
            {
                foreach (var item in items)
                {
                    item.Type = FM.InsertedFolderName;
                }
                GM.SaveChangesImages(list,Token);
            }
        }

        public void DeleteFolders(string FolderName, string Token)
        {
            List<string> list = new List<string>();
            list.AddRange(LoadListFolders());
            var Found = list.FirstOrDefault(u => u == FolderName);
            string path = Path + FolderName;
            if (System.IO.Directory.Exists(path))
            {
                //true yani pushe ra ba tamame file haye darunash pak kon
                System.IO.Directory.Delete(path, true);
                list.Remove(Found);
                SaveChangesFolders(list);

                List<GalleryModelAdmin> picList = new List<GalleryModelAdmin>();
                GalleryManagement GM = new GalleryManagement(F_UserName);
                picList.AddRange(GM.LoadPhotos(Token));
                var picFound = picList.Where(u => u.Type == FolderName).ToList();
                foreach (var pf in picFound)
                {
                    picList.Remove(pf);
                }
                GM.SaveChangesImages(picList,Token);
            }
        }


        public List<FolderModel> ManageListFolders(string Token)
        {
            List<FolderModel> model = new List<FolderModel>();
            List<string> FolderNames = new List<string>();
            FolderNames.AddRange(LoadListFolders());
            GalleryManagement GM = new GalleryManagement(F_UserName);
            List<GalleryModelAdmin> PhotoList = new List<GalleryModelAdmin>();
            PhotoList.AddRange(GM.LoadPhotos(Token));
            List<FolderModel> CountAndName = new List<FolderModel>();
            if (PhotoList.Count > 0)
            {
                var count = (from c in PhotoList group c.Type by c.Type into g select new { number = g.Count(), Type = g.Key }).ToList();
                foreach (var q in count)
                {
                    CountAndName.Add(new FolderModel { FolderName = q.Type, ExistingImagesCount = q.number });
                }
            }
            foreach (var item in FolderNames)
            {
                FolderModel m = new FolderModel();
                m.FolderName = item;
                var temp = CountAndName.FirstOrDefault(u => u.FolderName == item);
                m.ExistingImagesCount = (temp != null) ? temp.ExistingImagesCount : 0;
                model.Add(m);
            }
            return model;
        }

        #endregion

        #region user
        public List<string> UserLoadListFolders()
        {
            List<string> OBj = new List<string>();
            string path = Path + F_UserName + "_FoldersFile.xml";
            if (System.IO.File.Exists(path))
            {
                var serializer = new XmlSerializer(typeof(List<string>));
                using (var reader = XmlReader.Create(path))
                {
                    OBj = (List<string>)serializer.Deserialize(reader);
                    return OBj;
                }
            }
            else
            {
                return OBj;
            }
        }
        #endregion
    }
}