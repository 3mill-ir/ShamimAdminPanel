using AdminWeb.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace AdminWeb.Models.BLL
{
    public class SliderManagement
    {
        public SliderManagement(string profile)
        {
            F_UserName = profile;
            Path = Tools.ReturnPathPhysicalMode("SliderPath", F_UserName, "SliderManagement()");
        }
        string Path { get; set; }
        string F_UserName { get; set; }

        public List<SliderModel> LoadSlider()
        {
            List<SliderModel> OBj = new List<SliderModel>();
            if (!System.IO.File.Exists(Path + "/" + F_UserName + "_Slider.xml"))
            {
                InitiateSlider();
            }
            var serializer = new XmlSerializer(typeof(List<SliderModel>));
            using (var reader = XmlReader.Create(Path + "/" + F_UserName + "_Slider.xml"))
            {
                OBj = (List<SliderModel>)serializer.Deserialize(reader);
                return OBj;
            }
        }

        public void EditSlider(SliderModel model, HttpPostedFileBase Img,string F_UserName)
        {
            List<SliderModel> list = new List<SliderModel>();

            list.AddRange(LoadSlider());
            var FoundedObejct = list.FirstOrDefault(u => u.ID == model.ID);
            if (Img != null)
            {
                if (!string.IsNullOrEmpty(FoundedObejct.Img))
                {
                    string SpPath = Path + "/" + FoundedObejct.Img;
                    if (System.IO.File.Exists(SpPath))
                        System.IO.File.Delete(SpPath);
                }
                FoundedObejct.Img = Tools.ImageSave(Img, "SliderPath", F_UserName); 
            }
            FoundedObejct.Title = model.Title;
            FoundedObejct.Priority = model.Priority;
            FoundedObejct.Link = model.Link;
            FoundedObejct.Description = model.Description;
       
            SaveChangesSlider(list);
        }

        public bool ChangeDisplaySlider(int ID)
        {
            List<SliderModel> list = new List<SliderModel>();
            list.AddRange(LoadSlider());
            var FoundedObejct = list.FirstOrDefault(u => u.ID == ID);
            if (FoundedObejct != null)
            {
                FoundedObejct.Display = !FoundedObejct.Display;
                SaveChangesSlider(list);
                return true;
            }
            else
                return false;
        }

        private void SaveChangesSlider(List<SliderModel> model)
        {

            var serializer = new XmlSerializer(model.GetType());
            using (var writer = XmlWriter.Create(Path + "/" + F_UserName + "_Slider.xml"))
            {
                serializer.Serialize(writer, model);
            }
        }

        private void InitiateSlider()
        {
            string webURL = System.Configuration.ConfigurationManager.AppSettings["WebSiteURL"];
            List<SliderModel> list = new List<SliderModel>();
            for (int i = 0; i < 4; i++)
            {
                SliderModel sm = new SliderModel();
                sm.ID = i;
                sm.Priority = i;
                sm.Display = false;
                sm.Title = "تیتر آزمایشی" + (i + 1);
                sm.Description = "محتوای خلاصه آزمایشی" + (i + 1);
                sm.Link = webURL;
                sm.Img = "";
                list.Add(sm);
            }
            SaveChangesSlider(list);
        }
    }
}