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
    public class AttributeItemManagement
    {
        public async System.Threading.Tasks.Task<List<AttributeItemDataModel>> ListAttributeItem(string Token, int ID)
        {
            var Result = await Tools.GetObjectFromRequestAsync(ConfigurationManager.AppSettings["APIAddress"] + "/api/Attribute/GetAttributeItem?id=" + ID, Token, new List<AttributeItemDataModel>());
            var Object = JsonConvert.DeserializeObject<List<AttributeItemDataModel>>(Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            return Object != null ? Object : new List<AttributeItemDataModel>();
        }

        public async System.Threading.Tasks.Task<AttributeItemDataModel> DetailAttributeItem(int ID, string Token)
        {
            var Result = await Tools.GetObjectFromRequestAsync(ConfigurationManager.AppSettings["APIAddress"] + "/api/AttributeItem/GetAttributeItemDetails?id=" + ID, Token, new AttributeItemDataModel());
            var Object = JsonConvert.DeserializeObject<AttributeItemDataModel>(Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            return Object != null ? Object : new AttributeItemDataModel();

        }
        public async System.Threading.Tasks.Task<string> AddAttributeItem(AttributeItemDataModel model, string Token)
        {
            var result = await Tools.SendRequestToUrl(model, ConfigurationManager.AppSettings["APIAddress"] + "/api/AttributeItem/PostAttributeItem", Token, HttpMethod.Post);
            if (result == System.Net.HttpStatusCode.OK)
                return "OK";
            return "NOK";
        }

        public async System.Threading.Tasks.Task<string> EditAttributeItem(AttributeItemDataModel model, string Token)
        {
            var result = await Tools.SendRequestToUrl(model, ConfigurationManager.AppSettings["APIAddress"] + "/api/AttributeItem/PutAttributeItem", Token, HttpMethod.Put);
            if (result == System.Net.HttpStatusCode.OK)
                return "OK";
            return "NOK";

        }

        public async System.Threading.Tasks.Task<string> DeleteAttributeItem(int ID, string Token)
        {
            var result = await Tools.SendRequestToUrl(null, ConfigurationManager.AppSettings["APIAddress"] + "/api/AttributeItem/DeleteAttributeItem?id=" + ID, Token, HttpMethod.Delete);
            if (result == System.Net.HttpStatusCode.OK)
                return "OK";
            return "NOK";
        }
    }
}