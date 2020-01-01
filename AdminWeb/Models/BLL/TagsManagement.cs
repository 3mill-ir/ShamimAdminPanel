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
    public class TagsManagement
    {
        public async Task<string> ListTag(string Token)
        {
            var Result = await Tools.GetObjectFromRequestAsync( ConfigurationManager.AppSettings["APIAddress"]+"/api/tag/GetTag1", Token, new List<MenuDataModel>());
            var Object = JsonConvert.DeserializeObject<List<TagDataModel>>(Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            string Answer = "";
            foreach (var item in Object)
            {
                Answer += item.Text + ",";
            }
            return Answer;
        }

        public async System.Threading.Tasks.Task<string> AddTag(string Tags, int PostId, string Token)
        {
            TagDataListModel model = new TagDataListModel();
            if (!string.IsNullOrEmpty(Tags))
            {
                foreach (var item in Tags.Split(','))
                {
                    var m = new TagDataModel();
                    m.Text = item;
                    model.ListTag.Add(m);
                }
            }
            var result = await Tools.SendRequestToUrl(model,  ConfigurationManager.AppSettings["APIAddress"]+"/api/Tag/addtags?id=" + PostId, Token, HttpMethod.Post);
            if (result == System.Net.HttpStatusCode.OK)
                return "OK";
            return "NOK";
        }

        public async System.Threading.Tasks.Task<string> EditTag(string Tags, int PostId, string Token)
        {
            TagDataListModel model = new TagDataListModel();
            if (!string.IsNullOrEmpty(Tags))
            {
                foreach (var item in Tags.Split(','))
                {
                    var m = new TagDataModel();
                    m.Text = item;
                    model.ListTag.Add(m);
                }
            }
            var result = await Tools.SendRequestToUrl(model,  ConfigurationManager.AppSettings["APIAddress"]+"/api/Post/PutPostTags?id=" + PostId, Token, HttpMethod.Put);
            if (result == System.Net.HttpStatusCode.OK)
                return "OK";
            return "NOK";

        }

        public async System.Threading.Tasks.Task<string> GetPostTags(int PostId, string Token)
        {
            string Answer = "";
            var Result = await Tools.GetObjectFromRequestAsync( ConfigurationManager.AppSettings["APIAddress"]+"/api/Post/GetPostTags?id=" + PostId, Token, new List<MenuDataModel>());
            var Object = JsonConvert.DeserializeObject<List<TagDataModel>>(Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            Answer = String.Join(",", Object.Select(u=>u.Text).ToList());
            return Answer;
        }

    }
}