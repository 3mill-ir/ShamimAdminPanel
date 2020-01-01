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
    public class PageManagement
    {
        public  List<PageDataModel> ListPage(string Token)
        {
            var Result = Task.Run(() => Tools.GetObjectFromRequestAsync(ConfigurationManager.AppSettings["APIAddress"] + "/api/Page/getPages", Token, new List<PageDataModel>())).Result;
            var Object = JsonConvert.DeserializeObject<List<PageDataModel>>(Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            Object.RemoveAll(item => item == null);

            return Object;
        }

        public async System.Threading.Tasks.Task<PageDataModel> DetailPage(int ID, string Token)
        {
            var Result = await Tools.GetObjectFromRequestAsync( ConfigurationManager.AppSettings["APIAddress"]+"/api/Page/getPage?menuid=" + ID, Token, new PageDataModel());
            var Object = JsonConvert.DeserializeObject<PageDataModel>(Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            return Object != null ? Object : new PageDataModel();
        }

        public async System.Threading.Tasks.Task<string> EditPage(PageDataModel model, string Token)
        {
            var result = await Tools.SendRequestToUrl(model,  ConfigurationManager.AppSettings["APIAddress"]+ "/api/Page/PutPostPage?menuid=" + model.ID, Token, HttpMethod.Put);
            if (result == System.Net.HttpStatusCode.OK)
            {
                return "OK";
            }
            return "NOK";

        }

    }
}