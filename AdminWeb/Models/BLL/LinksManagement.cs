using AdminWeb.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace AdminWeb.Models.BLL
{
    public class LinksManagement
    {
        public LinksManagement(string profile)
        {
            F_UserName = profile;
            Path = Tools.ReturnPathPhysicalMode("LinksPath", F_UserName, "LinksManagement()");
        }
        string Path { get; set; }
        string F_UserName { get; set; }


        public void EditLink(LinkModel model, string Token, HttpPostedFileBase Img = null)
        {
            List<LinkModel> list = new List<LinkModel>();
            list.AddRange(LoadLinks());
            var FoundedObejct = list.FirstOrDefault(u => u.ID == model.ID);
            FoundedObejct.Link = model.Link;
            if (FoundedObejct.Type == "Footer")
                FoundedObejct.Text = model.Text;
            if (Img != null)
            {
                

                if (FoundedObejct.Image != "Rahbari.jpg")
                {
                    if (System.IO.File.Exists(Path + FoundedObejct.Image))
                        System.IO.File.Delete(Path + FoundedObejct.Image);
                }
                FoundedObejct.Image = Tools.ImageSave(Img, "LinksPath", F_UserName);
            }
            SaveChangesLinks(list);
        }
        public List<LinkModel> LoadLinks()
        {


            List<LinkModel> OBj = new List<LinkModel>();
            if (!System.IO.File.Exists(Path + "/" + F_UserName + "_Links.xml"))
            {
                InitiateLinks();
            }
            var serializer = new XmlSerializer(typeof(List<LinkModel>));
            using (var reader = XmlReader.Create(Path + "/" + F_UserName + "_Links.xml"))
            {
                OBj = (List<LinkModel>)serializer.Deserialize(reader);

                return OBj;
            }
        }

        private void SaveChangesLinks(List<LinkModel> model)
        {


            System.IO.Directory.CreateDirectory(Path);
            var serializer = new XmlSerializer(model.GetType());
            using (var writer = XmlWriter.Create(Path + "/" + F_UserName + "_Links.xml"))
            {
                serializer.Serialize(writer, model);
            }

        }

        private void InitiateLinks()
        {
            List<LinkModel> list = new List<LinkModel>();
            for (int i = 0; i < 4; i++)
            {
                LinkModel LM = new LinkModel();
                LM.ID = i;
                LM.Link = "http://www.leader.ir/fa";
                LM.Type = "Main";
                LM.Image = "Rahbari.jpg";
                list.Add(LM);
            }
            for (int i = 4; i < 9; i++)
            {
                LinkModel LM = new LinkModel();
                LM.ID = i;
                LM.Link = "http://www.leader.ir/fa";
                LM.Type = "Footer";
                LM.Text = "پایگاه اطلاع رسانی مقام معظم رهبری";
                list.Add(LM);
            }
            SaveChangesLinks(list);
        }

        #region user
        public List<LinkModel> UserLoadLinks()
        {

            List<LinkModel> OBj = new List<LinkModel>();
            if (!System.IO.File.Exists(Path + "/" + F_UserName + "_Links.xml"))
            {
                UserInitiateLinks(F_UserName);
            }
            var serializer = new XmlSerializer(typeof(List<LinkModel>));
            using (var reader = XmlReader.Create(Path + "/" + F_UserName + "_Links.xml"))
            {
                OBj = (List<LinkModel>)serializer.Deserialize(reader);
                return OBj;
            }
        }

        private void UserSaveChangesLinks(List<LinkModel> model)
        {



            var serializer = new XmlSerializer(model.GetType());
            using (var writer = XmlWriter.Create(Path + "/" + F_UserName + "_Links.xml"))
            {
                serializer.Serialize(writer, model);
            }
        }

        private void UserInitiateLinks(string profile)
        {
            List<LinkModel> list = new List<LinkModel>();
            for (int i = 0; i < 4; i++)
            {
                LinkModel LM = new LinkModel();
                LM.ID = i;
                LM.Link = "http://www.leader.ir/fa";
                LM.Type = "Main";
                LM.Image = "Rahbari.jpg";
                list.Add(LM);
            }
            for (int i = 4; i < 9; i++)
            {
                LinkModel LM = new LinkModel();
                LM.ID = i;
                LM.Link = "http://www.leader.ir/fa";
                LM.Type = "Footer";
                LM.Text = "پایگاه اطلاع رسانی مقام معظم رهبری";
                list.Add(LM);
            }
            UserSaveChangesLinks(list);
        }
        #endregion
    }
}