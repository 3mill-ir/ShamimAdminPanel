using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Routing;

namespace AdminWeb.Infrastructure
{
    public static class CustomHelpers
    {
        public static MvcHtmlString RawActionLink(this AjaxHelper ajaxHelper, string linkText, string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            var repID = Guid.NewGuid().ToString();
            var lnk = ajaxHelper.ActionLink(repID, actionName, controllerName, routeValues, ajaxOptions, htmlAttributes);
            return MvcHtmlString.Create(lnk.ToString().Replace(repID, linkText));
        }


        public static string MakeActiveController(this UrlHelper urlHelper, string controller)
        {
            string result = "active";

            string controllerName = urlHelper.RequestContext.RouteData.Values["controller"].ToString();

            if (!controllerName.Equals(controller, StringComparison.OrdinalIgnoreCase))
            {
                result = null;
            }

            return result;
        }


        public static string MakeActiveAction(this UrlHelper urlHelper, string action)
        {
            string result = "active";
            string actionName = urlHelper.RequestContext.RouteData.Values["action"].ToString();
            if (!actionName.Equals(action, StringComparison.OrdinalIgnoreCase))
            {
                result = null;
            }

            return result;
        }

        public static string MakeActiveActionWithType(this UrlHelper urlHelper, string action, string Type)
        {
            string result = "active";
            string actionName = urlHelper.RequestContext.RouteData.Values["action"].ToString();
            string Param = urlHelper.RequestContext.RouteData.Values["Type"] != null ? urlHelper.RequestContext.RouteData.Values["Type"].ToString() : "";
            if (!actionName.Equals(action, StringComparison.OrdinalIgnoreCase) || !Param.Equals(Type, StringComparison.OrdinalIgnoreCase))
            {
                result = null;
            }

            return result;
        }
        public static string MakeActive_All_Action(this UrlHelper urlHelper, string AllActions)
        {
            string result = "pipolevel3Menu active open";
            string actionName = urlHelper.RequestContext.RouteData.Values["action"].ToString();
            string[] actions = AllActions.Split('_');
            if (!actions.Contains(actionName, StringComparer.OrdinalIgnoreCase))
            {
                result = null;
            }
            return result;
        }

        public static string MakeActive_AllController(this UrlHelper urlHelper, string AllController)
        {
            string result = "active";

            string controllerName = urlHelper.RequestContext.RouteData.Values["controller"].ToString();
            string[] Controllers = AllController.Split('_');
            if (!Controllers.Contains(controllerName, StringComparer.OrdinalIgnoreCase))
            {
                result = null;
            }

            return result;
        }

        public static string getRandomColor(this HtmlHelper helper, int seed)
        {
            var random = new Random(seed);
            var color = String.Format("#{0:X6}", random.Next(0x1000000));
            return color;
        }
    }
}