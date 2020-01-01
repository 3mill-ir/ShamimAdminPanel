using AdminWeb.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace AdminWeb.Models.BLL
{
    public class ScriptManagement
    {
        public ScriptManagement(string profile)
        {
            F_UserName = profile;
            Path = Tools.ReturnPathPhysicalMode("ScriptPath", F_UserName, "ScriptManagement()");
        }
        string Path { get; set; }
        string F_UserName { get; set; }
        public void EditScripts(ScriptsModel model)
        {
            List<ScriptsModel> list = new List<ScriptsModel>();
            list.AddRange(LoadScripts());
            var FoundedObejct = list.FirstOrDefault(u => u.DisplayPlace == model.DisplayPlace);
            if (FoundedObejct.Script != model.Script || FoundedObejct.ScriptName != model.ScriptName)
            {
                FoundedObejct.ScriptName = model.ScriptName;
                FoundedObejct.Script = model.Script;
                SaveChangesScripts(list);
            }
        }
        public List<ScriptsModel> LoadScripts()
        {
            List<ScriptsModel> OBj = new List<ScriptsModel>();
            if (!System.IO.File.Exists(Path + "/" + F_UserName + "_Scripts.xml"))
            {
                InitiateScripts();
            }
            var serializer = new XmlSerializer(typeof(List<ScriptsModel>));
            using (var reader = XmlReader.Create(Path + "/" + F_UserName + "_Scripts.xml"))
            {
                OBj = (List<ScriptsModel>)serializer.Deserialize(reader);
                return OBj;
            }
        }
        private void SaveChangesScripts(List<ScriptsModel> model)
        {
            var serializer = new XmlSerializer(model.GetType());
            using (var writer = XmlWriter.Create(Path + "/" + F_UserName + "_Scripts.xml"))
            {
                serializer.Serialize(writer, model);
            }
        }
        private void InitiateScripts()
        {
            List<ScriptsModel> list = new List<ScriptsModel>();
            ScriptsModel LM = new ScriptsModel();
            LM.DisplayPlace = "اسکریپت فوتر";
            LM.ScriptName = "اوقات شرعی";
            LM.Script = "<script src=\"http://prayer.horuph.com/frame/?color0=FFFFFF&color1=FFFFFF&bgcolor=274472&inbgcolor=1F4C8C&az=1&tz=0&bdcolor=274472&border=1&curved=7&city=2-1\" type=\"text/javascript\" language=\"javascript\"></script>";
            list.Add(LM);
            ScriptsModel LM2 = new ScriptsModel();
            LM2.DisplayPlace = "اسکریپت اول صفحات";
            LM2.ScriptName = "تصویر روز";
            LM2.Script = "<script type=\"text/javascript\" src=\"http://pichAk.Net/blogcod/random-photos/religious/random.js\"></script>";
            list.Add(LM2);
            ScriptsModel LM3 = new ScriptsModel();
            LM3.DisplayPlace = "اسکریپت دوم صفحات";
            LM3.ScriptName = "سخن روز";
            LM3.Script = "<iframe class=\"disabledRuzSpeech\" src=\"http://hadis.toolsir.com/hadis.php?color=000000&amp;bg=FFFFFF&amp;bc=FFFFFF&amp;m=1,2,3,4,5,6,7,8,9,10,11,12,13,14,&amp;time=none\" scrolling=\"no\" frameborder=\"0\" hspace=\"0\" align=\"center\" width=\"154\" height=\"160\" style=\"border:1px solid #FFFFFF;-webkit-border-radius: 4px;-moz-border-radius: 4px;border-radius: 4px;\"></iframe>";
            list.Add(LM3);
            SaveChangesScripts(list);
        }


        #region user
        public List<ScriptsModel> UserLoadScripts()
        {
            List<ScriptsModel> OBj = new List<ScriptsModel>();
            if (!System.IO.File.Exists(Path + "/" + F_UserName + "_Scripts.xml"))
            {
                UserInitiateScripts();
            }
            var serializer = new XmlSerializer(typeof(List<ScriptsModel>));
            using (var reader = XmlReader.Create(Path + "/" + F_UserName + "_Scripts.xml"))
            {
                OBj = (List<ScriptsModel>)serializer.Deserialize(reader);
                return OBj;
            }
        }

        private void UserSaveChangesScripts(List<ScriptsModel> model)
        {
            var serializer = new XmlSerializer(model.GetType());
            using (var writer = XmlWriter.Create(Path + "/" + F_UserName + "_Scripts.xml"))
            {
                serializer.Serialize(writer, model);
            }
        }

        private void UserInitiateScripts()
        {
            List<ScriptsModel> list = new List<ScriptsModel>();
            ScriptsModel LM = new ScriptsModel();
            LM.DisplayPlace = "اسکریپت فوتر";
            LM.Script = "<script src=\"http://prayer.horuph.com/frame/?color0=FFFFFF&color1=FFFFFF&bgcolor=274472&inbgcolor=1F4C8C&az=1&tz=0&bdcolor=274472&border=1&curved=7&city=2-1\" type=\"text/javascript\" language=\"javascript\"></script>";
            list.Add(LM);
            ScriptsModel LM2 = new ScriptsModel();
            LM2.DisplayPlace = "اسکریپت اول صفحات";
            LM2.Script = "<script type=\"text/javascript\" src=\"http://pichAk.Net/blogcod/random-photos/religious/random.js\"></script>";
            list.Add(LM2);
            ScriptsModel LM3 = new ScriptsModel();
            LM3.DisplayPlace = "اسکریپت دوم صفحات";
            LM3.Script = "<iframe class=\"disabledRuzSpeech\" src=\"http://hadis.toolsir.com/hadis.php?color=000000&amp;bg=FFFFFF&amp;bc=FFFFFF&amp;m=1,2,3,4,5,6,7,8,9,10,11,12,13,14,&amp;time=none\" scrolling=\"no\" frameborder=\"0\" hspace=\"0\" align=\"center\" width=\"154\" height=\"160\" style=\"border:1px solid #FFFFFF;-webkit-border-radius: 4px;-moz-border-radius: 4px;border-radius: 4px;\"></iframe>";
            list.Add(LM3);
            UserSaveChangesScripts(list);
        }
        #endregion
    }
}