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
    public class UsersManagement
    {
        public async Task<List<ProfileRegisterDataModel>> ListUser(string Token)
        {
            var Result = await Tools.GetObjectFromRequestAsync(ConfigurationManager.AppSettings["APIAddress"] + "/api/Utility/GetFanbazarUsers", Token, new List<ProfileRegisterDataModel>());
            var Object = JsonConvert.DeserializeObject<List<ProfileRegisterDataModel>>(Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            return Object != null ? Object : new List<ProfileRegisterDataModel>();
        }

        public async Task<List<ProfileRegisterDataModel>> ListUserForAdmin(string Token)
        {
            var Result = await Tools.GetObjectFromRequestAsync(ConfigurationManager.AppSettings["APIAddress"] + "/api/account/GetListUsers", Token, new List<ProfileRegisterDataModel>());
            var Object = JsonConvert.DeserializeObject<List<ProfileRegisterDataModel>>(Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            return Object != null ? Object : new List<ProfileRegisterDataModel>();
        }

        public async Task<ProfileRegisterDataModel> UserDetail(int ID, string Token)
        {
            var Result = await Tools.GetObjectFromRequestAsync(ConfigurationManager.AppSettings["APIAddress"] + "/api/account/GetUserDetailsUser?id=" + ID, Token, new ProfileRegisterDataModel());
            var Object = JsonConvert.DeserializeObject<ProfileRegisterDataModel>(Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            return Object != null ? Object : new ProfileRegisterDataModel();
        }

        public async System.Threading.Tasks.Task<string> EditUser(UserInformationDataModel model, string Token)
        {
            var result = await Tools.SendRequestToUrl(model, ConfigurationManager.AppSettings["APIAddress"] + "/api/account/PutProfileRegister", Token, HttpMethod.Put);
            if (result == System.Net.HttpStatusCode.OK)
                return "OK";
            return "NOK";
        }

        public async System.Threading.Tasks.Task<string> ChangePasswordUser(ChangePasswordBindingModel model, string Token)
        {
            var result = await Tools.SendRequestToUrl(model, ConfigurationManager.AppSettings["APIAddress"] + "/api/account/ChangePassword", Token, HttpMethod.Post);
            if (result == System.Net.HttpStatusCode.OK)
                return "OK";
            return "NOK";
        }

    }
}