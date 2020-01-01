using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminWeb.Models.DataModels
{
    public class MenuDataModel
    {
        [Display( Name = "شناسه")]
        public int ID { get; set; }


        [Display(Name = "نام منو")]
        [Required(ErrorMessageResourceType = typeof(Resource.Resource), ErrorMessageResourceName = "View_ValidationError")]
        public string Name { get; set; }


        [Display(Name = "توضیحات")]
        [AllowHtml]
        public string Description { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource.Resource), ErrorMessageResourceName = "View_ValidationError")]
        [Display(Name = "وزن")]
        public double Weight { get; set; }


        [Required(ErrorMessageResourceType = typeof(Resource.Resource), ErrorMessageResourceName = "View_ValidationError")]
        [Display(Name = "وضعیت")]
        public bool Status { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource.Resource), ErrorMessageResourceName = "View_ValidationError")]
        [Display(Name = "زبان")]
        public string Language { get; set; }



        [Display(Name = "زیر منوی")]
        public Nullable<int> F_MenuID { get; set; }


        [Display(Name = "شناسه کاربر")]
        public string F_UserID { get; set; }


        [Display(Name = "عکس منو")]
        public string Image { get; set; }


        [Required(ErrorMessageResourceType = typeof(Resource.Resource), ErrorMessageResourceName = "View_ValidationError")]
        [Display(Name = "محتوای صفحه")]
        public string Type { get; set; }



        [Display(Name = "کلمات کلیدی")]
        public string MetaKeywords { get; set; }


        [Display(Name = "توضیحات")]
        public string MetaDescription { get; set; }


        [Display(Name = "عنوان صفحه")]
        public string MetaTittle { get; set; }


        [Display(Name = "آدرس سئو")]
        public string MetaSeoName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource.Resource), ErrorMessageResourceName = "View_ValidationError")]
        [Display(Name = "در فوتر نمایش داده شود")]
        public bool DisplayInFooter { get; set; }


        [Required(ErrorMessageResourceType = typeof(Resource.Resource), ErrorMessageResourceName = "View_ValidationError")]
        [Display(Name = "در ساید بار نمایش داده شود")]
        public bool DisplayInSidebar { get; set; }

    }
}