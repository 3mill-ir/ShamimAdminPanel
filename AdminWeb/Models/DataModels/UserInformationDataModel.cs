using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdminWeb.Models.DataModels
{
    public class UserInformationDataModel
    {
        [Display(Name = "شناسه")]
        public int ID { get; set; }
        [Display(Name = "نام")]
        public string FirstName { get; set; }
        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }
        [Display(Name = "استان محل سکونت")]
        public string Province { get; set; }
        [Display(Name = "شهر محل سکونت")]
        public string City { get; set; }
        [Display(Name = "کد ملی")]
        public string NationalID { get; set; }
        public string F_UserID { get; set; }
        public string CodeMelli { get; set; }
        public string Address { get; set; }
        public string Tell { get; set; }
        public string Email { get; set; }
        public int F_CityID { get; set; }
        public int F_StateID { get; set; }
    }
}