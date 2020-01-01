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
    public class ShoppingCartManagement
    {
        public async Task<ShopPagedList> ShopList(string ShopStatus, int pageNumber, int pageSize, string Token)
        {
            var Result =await Tools.GetObjectFromRequestAsync(ConfigurationManager.AppSettings["APIAddress"] + "/api/Shop/GetListShopForAdmin?ShopStatus=" + ShopStatus + "&pageNumber=" + pageNumber + "&pageSize=" + pageSize, Token, null);
            var Object = JsonConvert.DeserializeObject<ShopPagedList>(Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            return Object != null ? Object : new ShopPagedList();
        }
        public async Task<Shop> ShopDetail(string profile, int ShopID, string Token)
        {
            var Result =await Tools.GetObjectFromRequestAsync(ConfigurationManager.AppSettings["APIAddress"] + "/api/Shop/GetShopDetail?profile=" + profile + "&ShopID=" + ShopID, Token, null);
            var Object = JsonConvert.DeserializeObject<Shop>(Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            return Object != null ? Object : new Shop();
        }
        public async Task<string> AddShop(List<ShopItem> model, string profile, string Token)
        {
            string Result = await Tools.SendRequestToUrlGetObjectAsync(model, ConfigurationManager.AppSettings["APIAddress"] + "/api/Shop/PostShop?profile=" + profile, Token, HttpMethod.Post);
            var Object = JsonConvert.DeserializeObject<string>(Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            return Object != null ? Object : "";
        }

        public async Task<string> EditShopItem(List<ShopItem> model, string Token)
        {
            string Result = await Tools.SendRequestToUrlGetObjectAsync(model, ConfigurationManager.AppSettings["APIAddress"] + "/api/Shop/PutEditShopItem", Token, HttpMethod.Put);
            var Object = JsonConvert.DeserializeObject<string>(Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            return Object != null ? Object : "";
        }

        public async Task<string> EditShop(Shop model, string Token)
        {
            string Result = await Tools.SendRequestToUrlGetObjectAsync(model, ConfigurationManager.AppSettings["APIAddress"] + "/api/Shop/PutEditShop", Token, HttpMethod.Put);
            var Object = JsonConvert.DeserializeObject<string>(Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            return Object != null ? Object : "";
        }

        public async Task<string> ChangeStateShop(Shop model, string Token)
        {
            string Result = await Tools.SendRequestToUrlGetObjectAsync(model, ConfigurationManager.AppSettings["APIAddress"] + "/api/Shop/PostChangeShopState", Token, HttpMethod.Post);
            var Object = JsonConvert.DeserializeObject<string>(Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            return Object != null ? Object : "";
        }

    }
}