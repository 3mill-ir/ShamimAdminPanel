using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace AdminWeb.Models.DataModels
{
    public class FanBazarDataModel
    {
        public FanBazarDataModel()
        {
            AttributeGoups = new List<AttributeGroupDataModel>();
        }
        public List<AttributeGroupDataModel> AttributeGoups { get; set; }
        public ItemDataModel Item { get; set; }
    }


    public class ItemNew
    {
        //public ItemNew()
        //{
        //    this.AttributeValue = new Collection<AttributeValueNew>();
        //}

        public int ID { get; set; }
        public string Price { get; set; }
        public string Stock { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public Nullable<System.DateTime> CreatedDateOnUTC { get; set; }
        public string SubmitedState { get; set; }
        public Nullable<int> F_MenuID { get; set; }
        public string F_UserID { get; set; }
        public string Type { get; set; }
        public Nullable<int> NumberOfVisitors { get; set; }
        public string AdminDescription { get; set; }

        //  public ICollection<AttributeValueNew> AttributeValue { get; set; }
        public MenuNew Menu { get; set; }
    }


    public class MenuNew
    {
        public MenuNew()
        {
            this.Menu1 = new Collection<MenuNew>();
            this.AttributeGroup = new Collection<AttributeGroupNew>();
            //     this.Item = new Collection<ItemNew>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string F_UserID { get; set; }
        public string Image { get; set; }
        public string Type { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTittle { get; set; }
        public string MetaSeoName { get; set; }
        public Nullable<int> F_MenuID { get; set; }

        public Collection<MenuNew> Menu1 { get; set; }
        public MenuNew Menu2 { get; set; }
        public Collection<AttributeGroupNew> AttributeGroup { get; set; }
        //   public Collection<ItemNew> Item { get; set; }
    }


    public class AttributeGroupNew
    {
        public AttributeGroupNew()
        {
            this.Attribute = new Collection<AttributeNew>();
            this.AttributeGroup1 = new Collection<AttributeGroupNew>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<double> Weight { get; set; }
        public string Image { get; set; }
        public Nullable<int> F_AttributeGroupID { get; set; }
        public Nullable<int> Depth { get; set; }
        public Nullable<int> F_MenuID { get; set; }

        public Collection<AttributeNew> Attribute { get; set; }
        public Collection<AttributeGroupNew> AttributeGroup1 { get; set; }

    }


    public partial class AttributeNew
    {
        public AttributeNew()
        {
            this.AttributeValue = new Collection<AttributeValueNew>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<double> Weight { get; set; }
        public Nullable<int> F_AttributeGroupID { get; set; }
        public string ComponentType { get; set; }
        public Nullable<int> F_AttributeItemID { get; set; }
        public string Icon { get; set; }
        public string TextColor { get; set; }

        //  public AttributeGroupNew AttributeGroup { get; set; }
        public Collection<AttributeValueNew> AttributeValue { get; set; }
    }

    public class AttributeValueNew
    {
        public int ID { get; set; }
        public string Value { get; set; }
        public Nullable<int> F_AttributeID { get; set; }
        public Nullable<int> F_AttributeItemID { get; set; }
        public Nullable<int> F_ItemID { get; set; }

        //  public AttributeNew Attribute { get; set; }
        // public ItemNew Item { get; set; }
    }
}