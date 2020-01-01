using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdminWeb.Models.DataModels
{
    public class AttributeItemDataModel
    {
        public AttributeItemDataModel()
        {
            Attribute = new List<AttributeDataModel>();
        }
        [Display(Name = "شناسه")]
        public int ID { get; set; }
        [Display(Name = "نام")]
        public string Name { get; set; }
        public Nullable<int> F_AttributeID { get; set; }
        public List<AttributeDataModel> Attribute { get; set; }
    }
}