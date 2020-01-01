using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminWeb.Models.DataModels
{
    public class AspNetRoles
    {
        public AspNetRoles()
        {
            this.UserRollMapping = new List<UserRollMapping>();
            this.Roll_Mapping_RollGroupe = new List<Roll_Mapping_RollGroupe>();
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public virtual List<UserRollMapping> UserRollMapping { get; set; }
        public virtual List<Roll_Mapping_RollGroupe> Roll_Mapping_RollGroupe { get; set; }
    }
    public class UserRollMapping
    {
        public int ID { get; set; }
        public string F_UserID { get; set; }
        public string F_RollID { get; set; }
        public string F_RollGroupeID { get; set; }

        public virtual RollGroupe RollGroupe { get; set; }
        public virtual AspNetRoles AspNetRoles { get; set; }
        public virtual AspNetUsers AspNetUsers { get; set; }
    }

    public class RollGroupe
    {
        public RollGroupe()
        {
            this.Roll_Mapping_RollGroupe = new HashSet<Roll_Mapping_RollGroupe>();
            this.UserRollMapping = new HashSet<UserRollMapping>();
        }
        public string ID { get; set; }
        public string GroupeName { get; set; }
        public virtual ICollection<Roll_Mapping_RollGroupe> Roll_Mapping_RollGroupe { get; set; }
        public virtual ICollection<UserRollMapping> UserRollMapping { get; set; }
    }

    public class AspNetUsers
    {
        public AspNetUsers()
        {
            this.UserRollMapping = new List<UserRollMapping>();
        }
        public string Id { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public Nullable<System.DateTime> LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }
        public string RootUserID { get; set; }
        public string AccountType { get; set; }
        public virtual List<UserRollMapping> UserRollMapping { get; set; }
    }

    public class Roll_Mapping_RollGroupe
    {
        public int ID { get; set; }
        public string F_RollGroupeID { get; set; }
        public string F_RollID { get; set; }

        public virtual RollGroupe RollGroupe { get; set; }
        public virtual AspNetRoles AspNetRoles { get; set; }
    }
}