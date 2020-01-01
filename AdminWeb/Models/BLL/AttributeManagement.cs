using AdminWeb.Models.DataModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace AdminWeb.Models.BLL
{
    public class AttributeManagement
    {
        public async System.Threading.Tasks.Task<List<AttributeDataModel>> ListAttribute(string Token, int ID)
        {
            var Result = await Tools.GetObjectFromRequestAsync(ConfigurationManager.AppSettings["APIAddress"] + "/api/AttributeGroup/GetAttributeGroupAttributes?id=" + ID, Token, new List<AttributeDataModel>());
            var Object = JsonConvert.DeserializeObject<List<AttributeDataModel>>(Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            return Object != null ? Object : new List<AttributeDataModel>();
        }
        public async System.Threading.Tasks.Task<List<AttributeDataModel>> ListAttributeItemAttributes(string Token, int ID)
        {
            var Result = await Tools.GetObjectFromRequestAsync(ConfigurationManager.AppSettings["APIAddress"] + "/api/AttributeItem/GetAttributeItemAttributes?id=" + ID, Token, new List<AttributeDataModel>());
            var Object = JsonConvert.DeserializeObject<List<AttributeDataModel>>(Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            return Object != null ? Object : new List<AttributeDataModel>();
        }

        public async System.Threading.Tasks.Task<AttributeDataModel> DetailAttribute(int ID, string Token)
        {
            var Result = await Tools.GetObjectFromRequestAsync(ConfigurationManager.AppSettings["APIAddress"] + "/api/Attribute/GetAttributeDetail?id=" + ID, Token, new AttributeDataModel());
            var Object = JsonConvert.DeserializeObject<AttributeDataModel>(Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            return Object != null ? Object : new AttributeDataModel();

        }
        public async System.Threading.Tasks.Task<string> AddAttribute(AttributeDataModel model, HttpPostedFileBase MyFile, string Token)
        {
            if (model.F_AttributeGroupID == 0) model.F_AttributeGroupID = null;
            if (model.F_AttributeItemID == 0) model.F_AttributeItemID = null;
            string ImgStatus = Tools.ImageSave(MyFile, "AttributeIcons", Tools.F_UserName(Token));
            if (ImgStatus != "NotSaved")
                model.Icon = ImgStatus;
            var result = await Tools.SendRequestToUrl(model, ConfigurationManager.AppSettings["APIAddress"] + "/api/Attribute/PostAttribute", Token, HttpMethod.Post);
            if (result == System.Net.HttpStatusCode.OK)
                return "OK";
            return "NOK";
        }

        public async System.Threading.Tasks.Task<string> EditAttribute(AttributeDataModel model, HttpPostedFileBase MyFile, string Token)
        {
            string ImgStatus = Tools.ImageSave(MyFile, "AttributeIcons", Tools.F_UserName(Token));
            if (ImgStatus != "NotSaved")
                model.Icon = ImgStatus;
            var result = await Tools.SendRequestToUrl(model, ConfigurationManager.AppSettings["APIAddress"] + "/api/Attribute/PutAttribute", Token, HttpMethod.Put);
            if (result == System.Net.HttpStatusCode.OK)
                return "OK";
            return "NOK";

        }

        public async System.Threading.Tasks.Task<string> DeleteAttribute(int ID, string Token)
        {
            var result = await Tools.SendRequestToUrl(null, ConfigurationManager.AppSettings["APIAddress"] + "/api/Attribute/DeleteAttribute?id=" + ID, Token, HttpMethod.Delete);
            if (result == System.Net.HttpStatusCode.OK)
                return "OK";
            return "NOK";
        }
    }
}