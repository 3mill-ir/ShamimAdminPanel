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
    public class CommentManagement
    {
        public async Task<List<CommentDataModel>> ListComments(string Token,int ID)
        {
            string Result = "";
            if (ID==-1)
             Result = await Tools.GetObjectFromRequestAsync( ConfigurationManager.AppSettings["APIAddress"]+"/api/comment/GetComments", Token, new List<CommentDataModel>());
            else
                Result = await Tools.GetObjectFromRequestAsync( ConfigurationManager.AppSettings["APIAddress"]+"/api/post/GetPostComment?id="+ID, Token, new List<CommentDataModel>());
            var Object = JsonConvert.DeserializeObject<List<CommentDataModel>>(Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            return Object != null ? Object : new List<CommentDataModel>();
        }
        public async Task<string> ChangeStatusComments(int PollQuestionId, string Token)
        {
            var result = await Tools.SendRequestToUrl(null,  ConfigurationManager.AppSettings["APIAddress"]+"/api/Comment/DeleteChangeDisplayComment?id=" + PollQuestionId, Token, HttpMethod.Delete);
            if (result == System.Net.HttpStatusCode.OK)
                return "OK";
            return "NOK";
        }
    }
}