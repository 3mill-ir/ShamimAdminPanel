using AdminWeb.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace AdminWeb.Models.BLL
{
    public class PopUpManagement
    {
        public PopUpManagement(string profile)
        {
            F_UserName = profile;
            Path = Tools.ReturnPathPhysicalMode("PopUpPath", F_UserName, "PopUpManagement()");
        }
        string Path { get; set; }
        string F_UserName { get; set; }


        public void EditPopUp(PopUpModel model, string Token, HttpPostedFileBase Img = null)
        {
            var FoundedObejct = LoadPopUp();
            FoundedObejct.Link = model.Link;
            FoundedObejct.PopUpText = model.PopUpText;
            FoundedObejct.Status = model.Status;
            if (Img != null)
                FoundedObejct.PopUpMedia = Tools.ImageSave(Img, "PopUpPath", F_UserName);
            SaveChangesPopUp(FoundedObejct);
        }
        public PopUpModel LoadPopUp()
        {
            PopUpModel OBj = new PopUpModel();
            if (!System.IO.File.Exists(Path + "/" + F_UserName + "_PopUp.xml"))
                InitiatePopUp();
            var serializer = new XmlSerializer(typeof(PopUpModel));
            using (var reader = XmlReader.Create(Path + "/" + F_UserName + "_PopUp.xml"))
            {
                return (PopUpModel)serializer.Deserialize(reader);
            }
        }

        private void SaveChangesPopUp(PopUpModel model)
        {
            System.IO.Directory.CreateDirectory(Path);
            var serializer = new XmlSerializer(model.GetType());
            using (var writer = XmlWriter.Create(Path + "/" + F_UserName + "_PopUp.xml"))
            {
                serializer.Serialize(writer, model);
            }
        }

        private void InitiatePopUp()
        {
            PopUpModel LM = new PopUpModel();
            LM.Link = "";
            LM.Status = false;
            LM.PopUpMedia = "";
            LM.PopUpText = "";
            SaveChangesPopUp(LM);
        }

        #region user
        public PopUpModel UserLoadPopUp()
        {
            PopUpModel OBj = new PopUpModel();
            if (!System.IO.File.Exists(Path + "/" + F_UserName + "_PopUp.xml"))
            {
                UserInitiatePopUp(F_UserName);
            }
            var serializer = new XmlSerializer(typeof(PopUpModel));
            using (var reader = XmlReader.Create(Path + "/" + F_UserName + "_PopUp.xml"))
            {
                OBj = (PopUpModel)serializer.Deserialize(reader);
                return OBj;
            }
        }

        private void UserSaveChangesPopUp(PopUpModel model)
        {
            var serializer = new XmlSerializer(model.GetType());
            using (var writer = XmlWriter.Create(Path + "/" + F_UserName + "_PopUp.xml"))
            {
                serializer.Serialize(writer, model);
            }
        }

        private void UserInitiatePopUp(string profile)
        {
            PopUpModel LM = new PopUpModel();
            LM.Link = "";
            LM.Status = false;
            LM.PopUpMedia = "";
            LM.PopUpText = "";
            SaveChangesPopUp(LM);
        }
        #endregion
    }
}