using AdminWeb.Models.DataModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace AdminWeb.Models.BLL
{
    public class ItemManagement
    {
        public async Task<List<ItemDataModel>> ListItem(string Token, string Type, FilterItemDataModel model)
        {
            var Result = await Tools.SendRequestToUrlGetObjectAsync(model, ConfigurationManager.AppSettings["APIAddress"] + "/api/Item/PostListItemsForFanbazarAdmin?type=" + Type, Token, HttpMethod.Post);
            var Object = JsonConvert.DeserializeObject<List<ItemDataModel>>(Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            return Object != null ? Object : new List<ItemDataModel>();
        }

        public async System.Threading.Tasks.Task<string> ChangeStateItem(object model, string Token)
        {
            var result = await Tools.SendRequestToUrl(model, ConfigurationManager.AppSettings["APIAddress"] + "/api/Item/PutChangeSubmitedState", Token, HttpMethod.Put);
            return result == System.Net.HttpStatusCode.OK ? "OK" : "NOK";
        }

        public async Task<ItemNew> ItemDetail(int ID, string Token)
        {
            var Result = await Tools.GetObjectFromRequestAsync(ConfigurationManager.AppSettings["APIAddress"] + "/api/Item/GetItemDetailsForFanbazarAdmin?id=" + ID, Token, new ItemNew());
            var Object = JsonConvert.DeserializeObject<ItemNew>(Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            return Object != null ? Object : new ItemNew();
        }
        public async Task<ItemNew> UserDetailItemByType(string Type, string Token, string profile)
        {
            var Result = await Tools.GetObjectFromRequestAsync(ConfigurationManager.AppSettings["APIAddress"] + "/api/Item/GetItemDetailsByType?Type=" + Type + "&username=" + profile, Token, new ItemNew());
            var Object = JsonConvert.DeserializeObject<ItemNew>(Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            return Object;
        }
    }
}