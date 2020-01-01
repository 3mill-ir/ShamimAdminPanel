using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdminWeb.Models.DataModels
{
    public class TelegramModel
    {
        [Display(ResourceType = typeof(Resource.Resource), Name = "TelegramModel_Caption")]
        [Required(ErrorMessageResourceType = typeof(Resource.Resource), ErrorMessageResourceName = "View_ValidationError")]
        public string Caption { get; set; }
        [Display(ResourceType = typeof(Resource.Resource), Name = "TelegramModel_Path")]
        [Required(ErrorMessageResourceType = typeof(Resource.Resource), ErrorMessageResourceName = "View_ValidationError")]
        public string Path { get; set; }
    }

    public class TelegramModelFromList
    {
        public string Caption { get; set; }
        public string Path { get; set; }
        public string Url { get; set; }
        public int page { get; set; }
        public int Language { get; set; }
        public int F_MenuId { get; set; }
    }
}