using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminWeb.Models.DataModels
{
    public class PageTitle
    {
        public PageTitle(string _Pagetitle, string _ActionName, string _ControllerName, string _HtmlAttribute, string _RouteValue)
        {
            Pagetitle = _Pagetitle;
            ActionName = _ActionName;
            ControllerName = _ControllerName;
            RouteValue = _RouteValue;
            HtmlAttribute = _HtmlAttribute;
        }
        public string Pagetitle { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string RouteValue { get; set; }
        public string HtmlAttribute { get; set; }
    }
}