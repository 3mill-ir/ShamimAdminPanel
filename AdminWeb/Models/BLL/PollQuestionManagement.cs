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
    public class PollQuestionManagement
    {
        public async Task<string> AddPollQuestion(PollQuestionModel model, DateTime start, DateTime end,string Token)
        {
            model.StartDateOnUTC = start;
            model.EndDateOnUTC = end;
            var result = await Tools.SendRequestToUrl(model,  ConfigurationManager.AppSettings["APIAddress"]+"/api/pollquestion/PostQuestion", Token, HttpMethod.Post);
            if (result == System.Net.HttpStatusCode.OK)
                return "OK";
            return "NOK";
        }

        public async Task<string> EditPollQuestion(PollQuestionModel model, DateTime start, DateTime end,string Token)
        {
            model.StartDateOnUTC = start;
            model.EndDateOnUTC = end;
            var result = await Tools.SendRequestToUrl(model, ConfigurationManager.AppSettings["APIAddress"] + "/api/PollQuestion/PutQuestion?id=" + model.ID, Token, HttpMethod.Put);
            if (result == System.Net.HttpStatusCode.OK)
                return "OK";
            return "NOK";
        }

        public async Task<string> DeletePollQuestion(int PollQuestionId,string Token)
        {
            var result = await Tools.SendRequestToUrl(null,  ConfigurationManager.AppSettings["APIAddress"]+ "/api/PollQuestion/DeletePoll?PollQuestionID=" + PollQuestionId, Token, HttpMethod.Delete);
            if (result == System.Net.HttpStatusCode.OK)
                return "OK";
            return "NOK";
        }

        public UserPollModel UserPollHandler(string profile)
        {
                return null;
        }

        //Liste tamamiye nazarsanjihaye ta behal sabt shode (Makhsuse admin)
        public async Task<List<PollQuestionModel>> ListPollQuestion(string Token)
        {
            string Result = await Tools.GetObjectFromRequestAsync( ConfigurationManager.AppSettings["APIAddress"]+"/api/Pollquestion/GetPollquestions", Token, new List<PollQuestionModel>());
            var Object = JsonConvert.DeserializeObject<List<PollQuestionModel>>(Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            return Object != null ? Object : new List<PollQuestionModel>();
        }

        public async Task<PollQuestionModel> PollQuestionDetail(int PollQuestionId,string Token)
        {
            var Result = await Tools.GetObjectFromRequestAsync( ConfigurationManager.AppSettings["APIAddress"]+"/api/Pollquestion/GetPollQuestion?id=" + PollQuestionId, Token, new PollQuestionModel());
            var Object = JsonConvert.DeserializeObject<PollQuestionModel>(Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            return Object != null ? Object : new PollQuestionModel();
        }

    }
}