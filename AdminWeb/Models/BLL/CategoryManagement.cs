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
    public class CategoryManagement
    {
        public async System.Threading.Tasks.Task<List<MenuDataModel>> ListCategory(string Token, List<string> type,List<string> lang, string username)
        {
            var typelist = HttpUtility.ParseQueryString("");
            type.ForEach(s => typelist.Add("type", s));
            var langlist = HttpUtility.ParseQueryString("");
            lang.ForEach(s => langlist.Add("lang", s));
            var Result = await Tools.GetObjectFromRequestAsync(ConfigurationManager.AppSettings["APIAddress"] + "/api/menues/GetMenuAll?username=" + username + "&status=*&" + typelist, Token, new List<MenuDataModel>());
            var Object = JsonConvert.DeserializeObject<List<MenuDataModel>>(Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            Object.RemoveAll(item => item == null);
            return Object != null ? Object : new List<MenuDataModel>();
        }

        public async System.Threading.Tasks.Task<MenuDataModel> DetailCategory(int ID, string Token)
        {
            var Result = await Tools.GetObjectFromRequestAsync(ConfigurationManager.AppSettings["APIAddress"] + "/api/menues/GetMenu?id=" + ID, Token, new MenuDataModel());
            var Object = JsonConvert.DeserializeObject<MenuDataModel>(Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            return Object != null ? Object : new MenuDataModel();

        }
        public async System.Threading.Tasks.Task<string> AddCategory(MenuDataModel model, HttpPostedFileBase MyFile, string Token)
        {
            string ImgStatus = Tools.ImageSave(MyFile, "CategoryImages", Tools.F_UserName(Token));
            if (ImgStatus != "NotSaved")
                model.Image = ImgStatus;
            var result = await Tools.SendRequestToUrl(model, ConfigurationManager.AppSettings["APIAddress"] + "/api/menues/PostMenu", Token, HttpMethod.Post);
            if (result == System.Net.HttpStatusCode.OK)
                return "OK";
            return "NOK";
        }

        public async System.Threading.Tasks.Task<string> EditCategory(MenuDataModel model, HttpPostedFileBase MyFile, string Token)
        {
            string ImgStatus = Tools.ImageSave(MyFile, "CategoryImages", Tools.F_UserName(Token));
            if (ImgStatus != "NotSaved")
                model.Image = ImgStatus;
            var result = await Tools.SendRequestToUrl(model, ConfigurationManager.AppSettings["APIAddress"] + "/api/menues/PutMenu?id=", Token, HttpMethod.Put);
            if (result == System.Net.HttpStatusCode.OK)
                return "OK";
            return "NOK";

        }

        public async System.Threading.Tasks.Task<string> ChangeStatusCategory(int ID, string Token)
        {
            var result = await Tools.SendRequestToUrl(null, ConfigurationManager.AppSettings["APIAddress"] + "/api/menues/Putchangestatus?id=" + ID, Token, HttpMethod.Put);
            if (result == System.Net.HttpStatusCode.OK)
                return "OK";
            return "NOK";
        }

        public async System.Threading.Tasks.Task<string> DeleteCategory(int ID, string Token)
        {
            var result = await Tools.SendRequestToUrl(null, ConfigurationManager.AppSettings["APIAddress"] + "/api/menues/DeleteMenu?id=" + ID, Token, HttpMethod.Delete);
            if (result == System.Net.HttpStatusCode.OK)
                return "OK";
            return "NOK";
        }

        public async System.Threading.Tasks.Task<string> DeleteCategoryCascade(int ID, string Token)
        {
            var result = await Tools.SendRequestToUrl(null, ConfigurationManager.AppSettings["APIAddress"] + "/api/menues/DeleteMenucascade?id=" + ID, Token, HttpMethod.Delete);
            if (result == System.Net.HttpStatusCode.OK)
                return "OK";
            return "NOK";
        }
    }
}