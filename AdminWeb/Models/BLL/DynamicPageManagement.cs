using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AdminWeb.Models.DataModels;
using System.Net.Http;
using PagedList;
using PagedList.Mvc;
using Newtonsoft.Json;
using System.Configuration;

namespace AdminWeb.Models.BLL
{
    public class DynamicPageManagement
    {
        public async Task<ListPostDataModel> ListPost(FilterPostDatamodel model, string Token)
        {
            string Result = await Tools.SendRequestToUrlGetObjectAsync(model,  ConfigurationManager.AppSettings["APIAddress"]+"/api/Post/postListPosts", Token, HttpMethod.Post);
            var Object = JsonConvert.DeserializeObject<ListPostDataModel>(Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            return Object != null ? Object : new ListPostDataModel();
        }

        public async System.Threading.Tasks.Task<PostDataModel> DetailPost(int ID, string Token)
        {
            var Result = await Tools.GetObjectFromRequestAsync( ConfigurationManager.AppSettings["APIAddress"]+"/api/Post/GetPostDetails?id=" + ID, Token, new PostDataModel());
            var Object = JsonConvert.DeserializeObject<PostDataModel>(Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            return Object != null ? Object : new PostDataModel();

        }
        public async System.Threading.Tasks.Task<int> AddPost(PostDataModel model, HttpPostedFileBase MyFile, string Token,string F_UserName)
        {
            string ImgStatus = Tools.ImageSave(MyFile, "DynamicPageImages", F_UserName);
            if (ImgStatus != "NotSaved")
                model.ImagePath = ImgStatus;
            return Int32.Parse(await Tools.SendRequestToUrlGetObjectAsync(model,  ConfigurationManager.AppSettings["APIAddress"]+"/api/Post/AddPost", Token, HttpMethod.Post));
        }

        public async System.Threading.Tasks.Task<string> EditPost(PostDataModel model, HttpPostedFileBase MyFile, string Token,string F_UserName)
        {
            string ImgStatus = Tools.ImageSave(MyFile, "DynamicPageImages", F_UserName);
            if (ImgStatus != "NotSaved")
                model.ImagePath = ImgStatus;
            var result = await Tools.SendRequestToUrl(model,  ConfigurationManager.AppSettings["APIAddress"]+"/api/Post/PutPost?id=" + model.ID, Token, HttpMethod.Put);
            if (result == System.Net.HttpStatusCode.OK)
                return "OK";
            return "NOK";

        }

        public async System.Threading.Tasks.Task<string> DeletePost(int ID, string Token)
        {
            var result = await Tools.SendRequestToUrl(null,  ConfigurationManager.AppSettings["APIAddress"]+"/api/Post/DeletePost?id=" + ID, Token, HttpMethod.Delete);
            if (result == System.Net.HttpStatusCode.OK)
                return "OK";
            return "NOK";
        }

        public async System.Threading.Tasks.Task<string> ChangeStatusPost(int ID, string Token)
        {
            var result = await Tools.SendRequestToUrl(null,  ConfigurationManager.AppSettings["APIAddress"]+"/api/post/DeleteStstus?id=" + ID, Token, HttpMethod.Delete);
            if (result == System.Net.HttpStatusCode.OK)
                return "OK";
            return "NOK";
        }
    }
}