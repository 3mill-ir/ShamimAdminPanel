using AdminWeb.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminWeb.CustomFilters
{
    public class PageTittleAttributeActionFilter : ActionFilterAttribute
    {
        public string Function { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            System.Resources.ResourceManager rm = Resource.Resource.ResourceManager;
            filterContext.Controller.ViewBag.PageTittle_Tittle = rm.GetString(Function + "_Tittle_PageTittle");
            filterContext.Controller.ViewBag.PageTittle_Description = rm.GetString(Function + "_Description_PageTittle");
            filterContext.Controller.ViewBag.PageTittle_ContactUS = rm.GetString(Function + "_ContactUs_PageTittle");
            NameValueCollection section = (NameValueCollection)ConfigurationManager.GetSection("PagetittleSection");
            var tempPath = new List<string>(section[Function + "_PathLog"].Split(new char[] { ';' }));
            List<PageTitle> PathLog = new List<PageTitle>();
            foreach (var temp in tempPath)
            {
                if (!string.IsNullOrEmpty(temp))
                {
                    string[] path = temp.Split(',');
                    string _arguman = null;
                    if (path.Count() == 5 && path[4] != "null")
                    {
                        string[] argumans = path[4].Split('-');
                        for (int i = 0; i < argumans.Count(); i++)
                        {

                            if (filterContext.ActionParameters.ContainsKey(argumans[i]) && filterContext.ActionParameters[argumans[i]] != null)
                            {
                                if (_arguman == null)
                                {
                                    _arguman = "?" + string.Format("{0}={1}", argumans[i], filterContext.ActionParameters[argumans[i]].ToString());
                                }
                                else
                                {
                                    _arguman = _arguman + "&" + string.Format("{0}={1}", argumans[i], filterContext.ActionParameters[argumans[i]].ToString());
                                }
                            }
                        }
                    }
                    PathLog.Add(new PageTitle(rm.GetString(path[0]), path[1], path[2], path[3], _arguman));
                }
            }
            filterContext.Controller.ViewBag.PathLog = PathLog;
        }
    }
}