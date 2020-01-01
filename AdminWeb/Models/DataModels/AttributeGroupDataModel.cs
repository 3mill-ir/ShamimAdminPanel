using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdminWeb.Models.DataModels
{
    public class AttributeGroupDataModel
    {
        public AttributeGroupDataModel()
        {
            Attribute = new List<AttributeDataModel>();
            AttributeGroup1 = new List<AttributeGroupDataModel>();
        }
        [Display(Name = "شناسه")]
        public int ID { get; set; }
        [Display(Name = "نام")]
        public string Name { get; set; }
        [Display(Name = "وزن")]
        public Nullable<double> Weight { get; set; }
        public string Image { get; set; }
        public Nullable<int> F_AttributeGroupID { get; set; }
        [Display(Name = "عمق")]
        public Nullable<int> Depth { get; set; }
        [Display(Name = "دسته بندی")]
        public Nullable<int> F_MenuID { get; set; }
        public List<AttributeDataModel> Attribute { get; set; }
        public List<AttributeGroupDataModel> AttributeGroup1 { get; set; }
        public AttributeGroupDataModel AttributeGroup2 { get; set; }
    }
    public class ListAttributeGroupDataModel
    {
        public List<AttributeGroupDataModel> AttributeList { get; set; }
        public ListAttributeGroupDataModel()
        {
            AttributeList = new List<AttributeGroupDataModel>();
        }
    }
}