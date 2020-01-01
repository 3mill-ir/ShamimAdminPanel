using AdminWeb.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace AdminWeb.Models.BLL
{
    public class PollAnswerManagement
    {
        public int AddPollAnswer(PollQuestionModel model)
        {
            return 1;
        }

        private void AddPollAnswerEditHelper(string AnswerText, int F_PollQuestion_Id, int MaxKeyValue)
        {

        }

        public int EditPollAnswer(PollQuestionModel model)
        {
            return 1;
        }
        public async Task<string> DeletePollAnswer(int PollAnswerId, int PollQuestionID, string Token)
        {
            var result = await Tools.SendRequestToUrl(null, ConfigurationManager.AppSettings["APIAddress"] + "/api/PollQuestion/DeletePollAnswer?PollAnswerId=" + PollAnswerId+ "&PollQuestionID=" + PollQuestionID, Token, HttpMethod.Delete);
            if (result == System.Net.HttpStatusCode.OK)
                return "OK";
            return "NOK";
        }
    

    }
}