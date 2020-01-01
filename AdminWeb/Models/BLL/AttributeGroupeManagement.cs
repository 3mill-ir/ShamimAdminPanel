using AdminWeb.Models.DataModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AdminWeb.Models.BLL
{
    public class AttributeGroupeManagement
    {
        public SelectList F_AttributeGroupeIDs(string Token, int ID, int SelectedID = 0)
        {
            pipo = new List<SelectListItem>();
            var Result = Task.Run(() => Tools.GetObjectFromRequestAsync(ConfigurationManager.AppSettings["APIAddress"] + "/api/AttributeGroup/GetAttributeGroups?menuid=" + ID, Token, new List<AttributeGroupNew>())).Result;
            var Object = JsonConvert.DeserializeObject<List<AttributeGroupNew>>(Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            pipo.Add(new SelectListItem() { Text = "انتخاب گروه ویژگی های سطح بالاتر ...", Value = "" });
            AGHelper(Object, "");
            return new SelectList(pipo, "Value", "Text", SelectedID);
        }
        private List<SelectListItem> pipo;
        private void AGHelper(List<AttributeGroupNew> model, string Name)
        {
            foreach (var item in model)
            {
                item.Name = Name + item.Name + " > ";
                pipo.Add(new SelectListItem() { Text = item.Name.Substring(0,item.Name.Length-2), Value = item.ID.ToString() });
                AGHelper(item.AttributeGroup1.ToList(), item.Name);
            }
        }
        public async System.Threading.Tasks.Task<List<AttributeGroupNew>> ListAttributeGroupe(string Token, int F_MenuID)
        {
            var Result = await Tools.GetObjectFromRequestAsync(ConfigurationManager.AppSettings["APIAddress"] + "/api/AttributeGroup/GetAttributeGroups?menuid=" + F_MenuID, Token, new List<AttributeGroupNew>());
            var Object = JsonConvert.DeserializeObject<List<AttributeGroupNew>>(Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            return Object != null ? Object : new List<AttributeGroupNew>();
        }

        public async System.Threading.Tasks.Task<AttributeGroupNew> DetailAttributeGroupe(int ID, string Token)
        {
            var Result = await Tools.GetObjectFromRequestAsync(ConfigurationManager.AppSettings["APIAddress"] + "/api/AttributeGroup/GetAttributeGroupDetails?id=" + ID, Token, new AttributeGroupDataModel());
            var Object = JsonConvert.DeserializeObject<AttributeGroupNew>(Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            return Object != null ? Object : new AttributeGroupNew();

        }
        public async System.Threading.Tasks.Task<string> AddAttributeGroupe(AttributeGroupNew model, HttpPostedFileBase MyFile, string Token)
        {
            string ImgStatus = Tools.ImageSave(MyFile, "AttributeGroupeImages", Tools.F_UserName(Token));
            if (ImgStatus != "NotSaved")
                model.Image = ImgStatus;
            var result = await Tools.SendRequestToUrl(model, ConfigurationManager.AppSettings["APIAddress"] + "/api/AttributeGroup/PostAttributeGroup", Token, HttpMethod.Post);
            if (result == System.Net.HttpStatusCode.OK)
                return "OK";
            return "NOK";
        }

        public async System.Threading.Tasks.Task<string> EditAttributeGroupe(AttributeGroupNew model, HttpPostedFileBase MyFile, string Token)
        {
            string ImgStatus = Tools.ImageSave(MyFile, "AttributeGroupeImages", Tools.F_UserName(Token));
            if (ImgStatus != "NotSaved")
                model.Image = ImgStatus;
            var result = await Tools.SendRequestToUrl(model, ConfigurationManager.AppSettings["APIAddress"] + "/api/AttributeGroup/PutAttributeGroup", Token, HttpMethod.Put);
            if (result == System.Net.HttpStatusCode.OK)
                return "OK";
            return "NOK";

        }

        public async System.Threading.Tasks.Task<string> DeleteAttributeGroupe(int ID, string Token)
        {
            var result = await Tools.SendRequestToUrl(null, ConfigurationManager.AppSettings["APIAddress"] + "/api/AttributeGroup/DeleteAttributeGroup?id=" + ID, Token, HttpMethod.Delete);
            if (result == System.Net.HttpStatusCode.OK)
                return "OK";
            return "NOK";
        }
    }
}