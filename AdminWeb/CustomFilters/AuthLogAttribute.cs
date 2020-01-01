using AdminWeb.Models;
using AdminWeb.Models.BLL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AdminWeb.CustomFilters
{
    public class AuthLogAttribute : ActionFilterAttribute
    {
        public string Roles { get; set; }
        public AuthLogAttribute()
        {
            View = "NotAuthorized";
        }

        public AuthLogAttribute(string InRoles)
        {
            View = "AccessDenied";
            Roles = InRoles;
        }

        public string View { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            IsUserAuthorized(filterContext);
        }
        private void IsUserAuthorized(ActionExecutingContext filterContext)
        {
            var vr = new ViewResult();
            bool RoleAuth = false;
            string Token = filterContext.HttpContext.Request.Cookies["tokenKey"] != null ? filterContext.HttpContext.Request.Cookies["tokenKey"].Value : "";
            //if (filterContext.Result == null)
            //    return;
            using (var client = new HttpClient())
            {
                HttpRequestMessage req = new HttpRequestMessage();
                req.RequestUri = new Uri( ConfigurationManager.AppSettings["APIAddress"]+"/api/account/isAuthorized");
                req.Method = HttpMethod.Post;
                req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                HttpResponseMessage response = Task.Run(() => client.SendAsync(req)).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    RollManagement roll = new RollManagement();
                    var UserRolls = roll.AllRollsOfUser(Token);
                  var _acc= JsonConvert.DeserializeObject<AccountAuthorizeModel>(Task.Run(() => response.Content.ReadAsStringAsync()).Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                    filterContext.Controller.ViewBag.UserName = _acc.UserName;
                    filterContext.Controller.TempData["UserRolls"] = UserRolls;
                    filterContext.Controller.ViewBag.RootUserName = AdminWeb.Models.BLL.Tools.F_UserName(Token);
                    filterContext.ActionParameters["Token"] = Token;
                    filterContext.Controller.ViewBag.Token = Token;
                    if (!string.IsNullOrEmpty(Roles))
                    {
                        if (UserRolls.Count>0)
                        {
                          var  RoleArray = Roles.Split(',');
                            if (UserRolls.Any(u => Array.Exists(RoleArray, s => s.Equals(u.Name))))
                            {
                                RoleAuth = true;
                            }
                        }
                    }
                    else
                    {
                        RoleAuth = true;
                    }
               
                }
                if (string.IsNullOrEmpty(Token) || response.StatusCode != HttpStatusCode.OK)
                {
                    View = View;
                    vr.ViewName = View;
                    var result = vr;
                    filterContext.Result = result;
                }
                else if (RoleAuth == false)
                {
                    View = "AccessDenied";
                    vr.ViewName = View;
                    var result = vr;
                    filterContext.Result = result;
                }
                //vr.TempData = filterContext.Controller.TempData;
                //vr.ViewData = filterContext.Controller.ViewData;
                //filterContext.Result = vr;
                //var vr = new ViewResult();

            }
        }
    }
}