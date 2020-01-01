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
    public class ShekayatManagement
    {
        public TicketAccessoryDataModel ShekayatAccessory(string Token)
        {
            var Result = Tools.GetObjectFromRequest(ConfigurationManager.AppSettings["APIAddress"] + "/api/Shekayat/GetShekayatAccessory", Token, null);
            var Object = JsonConvert.DeserializeObject<TicketAccessoryDataModel>(Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            return Object != null ? Object : new TicketAccessoryDataModel();
        }
        public ShekayatListDataModel ShekayatList(string Token, string currentFilter, string searchString, string ShekayatStatus, int page = 1)
        {
            var Result = Tools.GetObjectFromRequest(ConfigurationManager.AppSettings["APIAddress"] + "/api/Shekayat/GetShekayatList?page=" + page + "&currentFilter=" + currentFilter + "&searchString=" + searchString + "&ShekayatStatus=" + ShekayatStatus, Token, null);
            var Object = JsonConvert.DeserializeObject<ShekayatListDataModel>(Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            return Object != null ? Object : new ShekayatListDataModel();
        }
        public Shekayat ShekayatDetail(int ShekayatID, string Token)
        {
            var Result = Tools.GetObjectFromRequest(ConfigurationManager.AppSettings["APIAddress"] + "/api/Shekayat/GetShekayatDetail?F_ShekayatId=" + ShekayatID, Token, null);
            var Object = JsonConvert.DeserializeObject<Shekayat>(Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            return Object != null ? Object : new Shekayat();
        }
        public List<ShekayatInOutboxDataModel> ShekayatStatus(string ShekayatID, string Token)
        {
            var Result = Tools.GetObjectFromRequest(ConfigurationManager.AppSettings["APIAddress"] + "/api/Shekayat/GetShekayatStatus?ShekayatId=" + ShekayatID, Token, null);
            var Object = JsonConvert.DeserializeObject<List<ShekayatInOutboxDataModel>>(Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            return Object != null ? Object : new List<ShekayatInOutboxDataModel>();
        }
        public Shekayat ShekayatBrief(int ShekayatID, string Token)
        {
            var Result = Tools.GetObjectFromRequest(ConfigurationManager.AppSettings["APIAddress"] + "/api/Shekayat/GetShekayatStatus?F_ShekayatId=" + ShekayatID, Token, null);
            var Object = JsonConvert.DeserializeObject<Shekayat>(Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            return Object != null ? Object : new Shekayat();
        }
        public void AddShekayatOutBox(ShekayatInOutboxDataModel model, string Token)
        {
            var Result = Tools.SendRequestToUrl(model, ConfigurationManager.AppSettings["APIAddress"] + "/api/Shekayat/PostShekayatResponse?F_LastShekayatInboxId=" + model.F_ShekayatID, Token, HttpMethod.Post);
        }
        public void ChangeShekayatStatus(Shekayat model, string Token)
        {
            var Result = Tools.SendRequestToUrl(model, ConfigurationManager.AppSettings["APIAddress"] + "/api/Shekayat/PostShekayatChangeStatus?F_ShekayatId=" + model.ID, Token, HttpMethod.Post);
        }
    }
}