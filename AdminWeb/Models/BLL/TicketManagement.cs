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
    public class TicketManagement
    {
        public TicketAccessoryDataModel TicketAccessory(string Token)
        {
            var Result = Tools.GetObjectFromRequest(ConfigurationManager.AppSettings["APIAddress"] + "/api/Ticket/GetTicketAccessory", Token, null);
            var Object = JsonConvert.DeserializeObject<TicketAccessoryDataModel>(Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            return Object != null ? Object : new TicketAccessoryDataModel();
        }
        public TicketListDataModel TicketList(string Token, string currentFilter, string searchString, string TicketStatus, int page = 1)
        {
            var Result = Tools.GetObjectFromRequest(ConfigurationManager.AppSettings["APIAddress"] + "/api/Ticket/GetTicketList?page=" + page + "&currentFilter=" + currentFilter + "&searchString=" + searchString + "&TicketStatus=" + TicketStatus, Token, null);
            var Object = JsonConvert.DeserializeObject<TicketListDataModel>(Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            return Object != null ? Object : new TicketListDataModel();
        }
        public List<TicketInboxModel> TicketDetail(string TicketID, string Token)
        {
            var Result = Tools.GetObjectFromRequest(ConfigurationManager.AppSettings["APIAddress"] + "/api/Ticket/GetTicketDetail?F_TicketId=" + TicketID, Token, null);
            var Object = JsonConvert.DeserializeObject<List<TicketInboxModel>>(Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            return Object != null ? Object : new List<TicketInboxModel>();
        }
        public List<TicketInboxModel> TicketStatus(string TicketID, string Token)
        {
            var Result = Tools.GetObjectFromRequest(ConfigurationManager.AppSettings["APIAddress"] + "/api/Ticket/GetTicketDetail?TicketId=" + TicketID, Token, null);
            var Object = JsonConvert.DeserializeObject<List<TicketInboxModel>>(Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            return Object != null ? Object : new List<TicketInboxModel>();
        }

        public TicketModel TicketBrief(int TicketID,string Token)
        {
            var Result = Tools.GetObjectFromRequest(ConfigurationManager.AppSettings["APIAddress"] + "/api/Ticket/GetTicketStatus?F_TicketId=" + TicketID, Token, null);
            var Object = JsonConvert.DeserializeObject<TicketModel>(Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            return Object != null ? Object : new TicketModel();
        }

        public void AddTicketOutBox(TicketOutBoxModel model, string Token)
        {
            var Result = Tools.SendRequestToUrl(model,ConfigurationManager.AppSettings["APIAddress"] + "/api/Ticket/PostTicketResponse?F_LastTicketInboxId=" + model.F_TicketInbox_Id, Token, HttpMethod.Post);
        }

        public void ChangeTicketStatus(TicketModel model, string Token)
        {
            var Result = Tools.SendRequestToUrl(model,ConfigurationManager.AppSettings["APIAddress"] + "/api/Ticket/PostTicketChangeStatus?F_TicketId=" + model.ID, Token, HttpMethod.Post);
        }
    }
}