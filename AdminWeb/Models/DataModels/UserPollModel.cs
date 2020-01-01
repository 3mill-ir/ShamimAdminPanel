using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminWeb.Models.DataModels
{
    public class UserPollModel
    {
        public UserPollModel()
        {
            AnswerBox = new List<PollAnswerDataModel>();
        }
        public int ID { get; set; }
        public int SelectedPollAnswerID { get; set; }
        public string QuestionText { get; set; }
        public string ChartType { get; set; }
        public List<PollAnswerDataModel> AnswerBox { get; set; }
    }
}