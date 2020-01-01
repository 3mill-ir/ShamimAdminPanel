using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminWeb.Models.DataModels
{
    public class ListPostDataModel
    {
        public ListPostDataModel()
        {
            List = new List<PostDataModel>();
        }
        public int count { get; set; }
        public List<PostDataModel> List { get; set; }
    }
    public class PostDataModel
    { 
        public PostDataModel()
        {
            Comments1 = new List<CommentDataModel>();
        }
        [Display(Name = "شناسه")]
        public int ID { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessageResourceType = typeof(Resource.Resource), ErrorMessageResourceName = "View_ValidationError")]
        public string Tittle { get; set; }


        [Display(Name = "خلاصه")]
        public string Description { get; set; }


   [AllowHtml]
        [Display(Name = "جزئیات")]
        [Required(ErrorMessageResourceType = typeof(Resource.Resource), ErrorMessageResourceName = "View_ValidationError")]
        public string Detail { get; set; }


        [Display(Name = "تصویر مطلب")]
        public string ImagePath { get; set; }




        [Required(ErrorMessageResourceType = typeof(Resource.Resource), ErrorMessageResourceName = "View_ValidationError")]
        [Display(Name = "وضعیت")]
        public Nullable<bool> Status { get; set; }


        public string F_UserID { get; set; }


        [Required(ErrorMessageResourceType = typeof(Resource.Resource), ErrorMessageResourceName = "View_ValidationError")]
        [Display(Name = "دسته بندی")]
        public Nullable<int> F_MenuID { get; set; }


        [Display(Name = "تعداد بازدید ها")]
        public Nullable<int> NumberOfVisitors { get; set; }
        [Display(Name = "تعداد کامنت ها")]
        public Nullable<int> NumberOfComments { get; set; }
        [Display(Name = "تعداد لایک ها")]
        public Nullable<int> NumberOfLikes { get; set; }
        [Display(Name = "تعداد دیس لایک ها")]
        public Nullable<int> NumberOfDislikes { get; set; }
        [Display(Name = "زمان ایجاد")]
        public Nullable<System.DateTime> CreatedOnUTC { get; set; }
        [Display(Name = "زمان آخرین ویرایش")]
        public Nullable<System.DateTime> EditedDateOnUTC { get; set; }
        [Display(Name = "توضیحات")]
        public string MetaDescription { get; set; }
        [Display(Name = "عنوان مطلب")]
        public string MetaTittle { get; set; }
        [Display(Name = "آدرس سئو")]
        public string MetaSeoName { get; set; }
        [Display(Name = "متن جایگزین تصویر")]
        public string ImageAlt { get; set; }
        [Display(Name = "قابلیت نظر دهی")]
        public bool AllowComment { get; set; }

        [Display(Name = "زبان")]
        [Required(ErrorMessageResourceType = typeof(Resource.Resource), ErrorMessageResourceName = "View_ValidationError")]
        public string Language { get; set; }

        public List<CommentDataModel> Comments1 { get; set; }
        [Display(Name = "برچسب ها")]
        public string Tags { get; set; }

        public string MenuName { get; set; }

        public string HTMLname { get; set; }
    }
}