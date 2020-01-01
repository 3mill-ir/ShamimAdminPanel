using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace AdminWeb.Models.DataModels
{
    public class TicketListDataModel
    {
        public TicketListDataModel()
        {
            Tickets = new List<TicketModel>();
        }
        public List<TicketModel> Tickets { get; set; }
        public int Total { get; set; }
    }
}