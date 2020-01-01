using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdminWeb.Models.DataModels
{
    public class TicketAccessoryDataModel
    {
        public int UnResponseTicketCount { get; set; }
        public int ToResponseTicketCount { get; set; }
        public int ResponseTicketCount { get; set; }
        public int AllTicketCount { get; set; }
    }
    public class TicketModel
    {
        [Required(ErrorMessageResourceType = typeof(Resource.Resource), ErrorMessageResourceName = "View_ValidationError")]
        public int ID { get; set; }
        public string TrackingCode { get; set; }
        public string ID_STR { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resource.Resource), ErrorMessageResourceName = "View_ValidationError")]
        public string Status { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resource.Resource), ErrorMessageResourceName = "View_ValidationError")]
        public DateTime LastUpdateOnUtc { get; set; }
        public string LastUpdateOnUtcJalali { get; set; }
        public string TicketInbox_brief { get; set; }
        public int CountInbox { get; set; }
        public int CountOutbox { get; set; }
        public int CountInboxMedia { get; set; }
        public List<TicketInboxModel> TicketInbox { get; set; }
    }
    public class Ticket
    {
        public Ticket()
        {
            TicketInbox = new List<TicketInbox>();
        }

        public int ID { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> LastUpdatedOnUTC { get; set; }
        public string TrackingCode { get; set; }
        public string F_UserID { get; set; }
        public List<TicketInbox> TicketInbox { get; set; }
    }
    public class TicketInbox
    {
        public TicketInbox()
        {
            this.TicketInboxMedia = new List<TicketInboxMedia>();
            this.TicketLog = new List<TicketLog>();
            this.TicketOutBox = new List<TicketOutBox>();
        }
        public int ID { get; set; }
        public string TicketContent { get; set; }
        public Nullable<System.DateTime> CreatedOnUTC { get; set; }
        public string TicketType { get; set; }
        public string TicketFrom { get; set; }
        public Nullable<int> F_TicketID { get; set; }
        public Ticket Ticket { get; set; }
        public List<TicketInboxMedia> TicketInboxMedia { get; set; }
        public List<TicketLog> TicketLog { get; set; }
        public List<TicketOutBox> TicketOutBox { get; set; }
    }
    public class TicketInboxMedia
    {
        public int ID { get; set; }
        public string MediaPath { get; set; }
        public string MediaType { get; set; }
        public Nullable<int> F_TicketInboxID { get; set; }
        public TicketInbox TicketInbox { get; set; }
    }

    public class TicketLog
    {
        public int ID { get; set; }
        public string Address { get; set; }
        public string Device { get; set; }
        public Nullable<System.DateTime> CreatedOnUTC { get; set; }
        public Nullable<int> F_TicketInboxID { get; set; }
        public TicketInbox TicketInbox { get; set; }
    }

    public class TicketOutBox
    {
        public int ID { get; set; }
        public string Content_One { get; set; }
        public Nullable<bool> IsRead { get; set; }
        public string SMSText { get; set; }
        public string SMSStatusOne { get; set; }
        public string SMSStatusTwo { get; set; }
        public Nullable<System.DateTime> CreatedOnUTC { get; set; }
        public Nullable<int> F_TicketInboxID { get; set; }
        public string UserRole { get; set; }
        public virtual TicketInbox TicketInbox { get; set; }
    }
    public class TicketInboxModel
    {
        public TicketInboxModel()
        {
            TicketOutbox = new List<TicketOutBoxModel>();
            TicketInboxMedia = new List<TicketInboxMediaModel>();
        }
        public int ID { get; set; }
        public string TicketContent { get; set; }
        public DateTime CreatedOnUTC { get; set; }
        public string CreatedOnUTCJalali { get; set; }
        public string TicketType { get; set; }
        public string TicketFrom { get; set; }
        public int F_Ticket_Id { get; set; }
        public List<TicketOutBoxModel> TicketOutbox { get; set; }
        public List<TicketInboxMediaModel> TicketInboxMedia { get; set; }
    }
    public class TicketOutBoxModel
    {
        public int ID { get; set; }
        public string Content_One { get; set; }
        public bool isRead { get; set; }
        public string SMSText { get; set; }
        public string SMSStatusOne { get; set; }
        public string SMSStatusTwo { get; set; }
        public DateTime CreatedOnUTC { get; set; }
        public string CreatedOnUTCJalali { get; set; }
        public int F_TicketInbox_Id { get; set; }
        public string UserRole { get; set; }
    }

    public class TicketInboxMediaModel
    {
        public int ID { get; set; }
        public string MediaPath { get; set; }
        public string MediaType { get; set; }
        public int F_TicketInbox_Id { get; set; }
    }
}