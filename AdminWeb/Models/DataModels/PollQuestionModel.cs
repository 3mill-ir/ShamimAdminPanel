using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdminWeb.Models.DataModels
{
    public class PollQuestionModel
    {
        public PollQuestionModel()
        {
            PollAnswer = new List<PollAnswerDataModel>();
        }


        [Display(ResourceType = typeof(Resource.Resource), Name = "PollQuestion_ID")]

        public int ID { get; set; }


        [Display(ResourceType = typeof(Resource.Resource), Name = "PollQuestion_Question")]
        public string Question { get; set; }


        [Display(ResourceType = typeof(Resource.Resource), Name = "PollQuestion_CreatedOnUTC")]
        public Nullable<DateTime> CreatedOnUTC { get; set; }


        [Display(ResourceType = typeof(Resource.Resource), Name = "PollQuestion_StartedOnUTC")]
        public string StartDateOnUTCHelper { get; set; }

        [Display(ResourceType = typeof(Resource.Resource), Name = "PollQuestion_EndDateUTC")]
        public string EndDateOnUTCHelper { get; set; }

        [Display(ResourceType = typeof(Resource.Resource), Name = "PollQuestion_StartedOnUTC")]
        public Nullable<DateTime> StartDateOnUTC { get; set; }
        [Display(ResourceType = typeof(Resource.Resource), Name = "PollQuestion_EndDateUTC")]
        public Nullable<DateTime> EndDateOnUTC { get; set; }

        [Display(ResourceType = typeof(Resource.Resource), Name = "PollQuestion_AnswerBoxes")]
        public List<PollAnswerDataModel> PollAnswer { get; set; }


   
        public Nullable<bool> isDeleted { get; set; }

        [Display(ResourceType = typeof(Resource.Resource), Name = "PollQuestion_Active")]
        public bool Active { get; set; }

        [Display(ResourceType = typeof(Resource.Resource), Name = "PollQuestion_ChartType")]
        public string ChartType { get; set; }
        public string F_UserID { get; set; }

    
    }

    public class PollAnswerDataModel
    {
        public PollAnswerDataModel()
        {
            PollLog = new List<PollLogDataModel>();
        }
        public string IPAddress { get; set; }
        public string Device { get; set; }

        public virtual List<PollLogDataModel> PollLog { get; set; }

        [Display(ResourceType = typeof(Resource.Resource), Name = "PollAnswer_ID")]

        public int ID { get; set; }


        [Display(ResourceType = typeof(Resource.Resource), Name = "PollAnswer_Text")]
        public string Text { get; set; }


        [Display(ResourceType = typeof(Resource.Resource), Name = "PollAnswer_AnswerKey")]
        public int AnswerKey { get; set; }


        [Display(ResourceType = typeof(Resource.Resource), Name = "PollAnswer_Score")]
        public int Score { get; set; }


        [Display(ResourceType = typeof(Resource.Resource), Name = "PollAnswer_F_PollQuestion_Id")]
        public Nullable<int> F_PollQuestionID { get; set; }

        [Display(ResourceType = typeof(Resource.Resource), Name = "PollAnswer_AnswerColor")]
        public string Color { get; set; }
    }

    public class PollLogDataModel
    {
        public int ID { get; set; }
        public string IPAddress { get; set; }
        public string F_UserID { get; set; }
        public string Device { get; set; }
        public Nullable<System.DateTime> CreatedOnUTC { get; set; }
        public Nullable<int> F_PollAnswerID { get; set; }
    }
}