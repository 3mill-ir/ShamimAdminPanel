using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminWeb.Models.DataModels
{
    public class TicketChartModel
    {
        public string ChartType { get; set; }
        public bool ChartStatus { get; set; }
        public string WaitingColor { get; set; }
        public string CheckingColor { get; set; }
        public string RespondedColor { get; set; }
    }
}