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
    public class RollManagement
    {
        public async System.Threading.Tasks.Task<List<AspNetRoles>> ListRoll(string Token)
        {
            var Result = await Tools.GetObjectFromRequestAsync(ConfigurationManager.AppSettings["APIAddress"] + "/api/Roll/GetListRoll", Token, new List<AspNetRoles>());
            var Object = JsonConvert.DeserializeObject<List<AspNetRoles>>(Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            return Object != null ? Object : new List<AspNetRoles>();
        }
        public async System.Threading.Tasks.Task<string> AddRoll(AspNetRoles model, string Token)
        {
            var result = await Tools.SendRequestToUrl(model, ConfigurationManager.AppSettings["APIAddress"] + "/api/Roll/PostRoll", Token, HttpMethod.Post);
            if (result == System.Net.HttpStatusCode.OK)
                return "OK";
            return "NOK";
        }
        public async System.Threading.Tasks.Task<string> DeleteRoll(string ID, string Token)
        {
            var result = await Tools.SendRequestToUrl(null, ConfigurationManager.AppSettings["APIAddress"] + "/api/Roll/DeleteRoll?ID=" + ID, Token, HttpMethod.Delete);
            if (result == System.Net.HttpStatusCode.OK)
                return "OK";
            return "NOK";
        }

        public async System.Threading.Tasks.Task<List<RollGroupe>> ListRollGroup(string Token)
        {
            var Result = await Tools.GetObjectFromRequestAsync(ConfigurationManager.AppSettings["APIAddress"] + "/api/Roll/GetListRollGroupe", Token, new List<RollGroupe>());
            var Object = JsonConvert.DeserializeObject<List<RollGroupe>>(Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            return Object != null ? Object : new List<RollGroupe>();
        }

        public async System.Threading.Tasks.Task<List<AspNetRoles>> ListRollsOfRollGroup(string RollGroupID, string Token)
        {
            var Result = await Tools.GetObjectFromRequestAsync(ConfigurationManager.AppSettings["APIAddress"] + "/api/Roll/GetListRollOfRollGroup?RollGroupID=" + RollGroupID, Token, new List<AspNetRoles>());
            var Object = JsonConvert.DeserializeObject<List<AspNetRoles>>(Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            return Object != null ? Object : new List<AspNetRoles>();
        }
        public async System.Threading.Tasks.Task<string> AddRollGroup(RollGroupe model, string Token)
        {
            var result = await Tools.SendRequestToUrl(model, ConfigurationManager.AppSettings["APIAddress"] + "/api/Roll/PostRollGroupe", Token, HttpMethod.Post);
            if (result == System.Net.HttpStatusCode.OK)
                return "OK";
            return "NOK";
        }
        public async System.Threading.Tasks.Task<string> DeleteRollGroup(string ID, string Token)
        {
            var result = await Tools.SendRequestToUrl(null, ConfigurationManager.AppSettings["APIAddress"] + "/api/Roll/DeleteRollGroupe?ID=" + ID, Token, HttpMethod.Delete);
            if (result == System.Net.HttpStatusCode.OK)
                return "OK";
            return "NOK";
        }

        public async System.Threading.Tasks.Task<string> AssignRollToRollGroup(List<AspNetRoles> model, string RollGroupeID, string Token)
        {
            var result = await Tools.SendRequestToUrl(model, ConfigurationManager.AppSettings["APIAddress"] + "/api/Roll/PostAsignRollToRollGroupe?RollGroupeID=" + RollGroupeID, Token, HttpMethod.Post);
            if (result == System.Net.HttpStatusCode.OK)
                return "OK";
            return "NOK";
        }

        public async System.Threading.Tasks.Task<string> AssignRollToUser(List<AspNetRoles> model, string F_UserID, string Token)
        {
            var result = await Tools.SendRequestToUrl(model, ConfigurationManager.AppSettings["APIAddress"] + "/api/Roll/PostAsignRollToUser?F_UserID=" + F_UserID, Token, HttpMethod.Post);
            if (result == System.Net.HttpStatusCode.OK)
                return "OK";
            return "NOK";
        }

        public async System.Threading.Tasks.Task<string> AssignRollGroupToUser(List<RollGroupe> model, string F_UserID, string Token)
        {
            var result = await Tools.SendRequestToUrl(model, ConfigurationManager.AppSettings["APIAddress"] + "/api/Roll/PostAsignRollGroupToUser?F_UserID=" + F_UserID, Token, HttpMethod.Post);
            if (result == System.Net.HttpStatusCode.OK)
                return "OK";
            return "NOK";
        }


        public async System.Threading.Tasks.Task<List<RollGroupe>> ListRollGroupOfUser(string Token, string UserID)
        {
            var Result = await Tools.GetObjectFromRequestAsync(ConfigurationManager.AppSettings["APIAddress"] + "/api/Roll/GetListRollGroupOfUser?UserID=" + UserID, Token, new List<RollGroupe>());
            var Object = JsonConvert.DeserializeObject<List<RollGroupe>>(Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            return Object != null ? Object : new List<RollGroupe>();
        }

        public async System.Threading.Tasks.Task<List<AspNetRoles>> ListRollOfUser(string Token, string UserID)
        {
            var Result = await Tools.GetObjectFromRequestAsync(ConfigurationManager.AppSettings["APIAddress"] + "/api/Roll/GetListRollOfUser?UserID=" + UserID, Token, new List<RollGroupe>());
            var Object = JsonConvert.DeserializeObject<List<AspNetRoles>>(Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            return Object != null ? Object : new List<AspNetRoles>();
        }

        public List<AspNetRoles> AllRollsOfUser(string Token)
        {
            var Result = Tools.GetObjectFromRequest(ConfigurationManager.AppSettings["APIAddress"] + "/api/Roll/GetAllRollsForUser", Token, new List<RollGroupe>());
            var Object = JsonConvert.DeserializeObject<List<AspNetRoles>>(Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            return Object != null ? Object : new List<AspNetRoles>();
        }

        public async System.Threading.Tasks.Task<bool?> IsRollAuthorizedForUser(string Token, string UserID, string RollName)
        {
            var Result = await Tools.GetObjectFromRequestAsync(ConfigurationManager.AppSettings["APIAddress"] + "/api/Roll/GetRollAuthorizedForUser?UserID=" + UserID + "&RollName=" + RollName, Token, null);
            var Object = JsonConvert.DeserializeObject<bool?>(Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            return Object != null ? Object : false;
        }
    }
}