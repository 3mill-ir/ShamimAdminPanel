using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdminWeb.Models.DataModels
{
    public class AttributeDataModel
    {
        public AttributeDataModel()
        {
            AttributeItem1 = new List<AttributeItemDataModel>();
        }
        [Display(Name = "شناسه")]
        public int ID { get; set; }
        [Display(Name = "نام ویژگی")]
        public string Name { get; set; }
        [Display(Name = "وزن")]
        public Nullable<double> Weight { get; set; }
        public Nullable<int> F_AttributeGroupID { get; set; }
        [Display(Name = "نوع ورودی")]
        public string ComponentType { get; set; }
        public Nullable<int> F_AttributeItemID { get; set; }
        [Display(Name = "آیکون")]
        public string Icon { get; set; }
        [Display(Name = "رنگ متن")]
        public string TextColor { get; set; }
        [Display(Name = "مقدار")]
        public string Value { get; set; }
        [Display(Name = "باید تکمیل شود")]
        public bool Required { get; set; }
        public List<AttributeItemDataModel> AttributeItem1 { get; set; }
    }
}