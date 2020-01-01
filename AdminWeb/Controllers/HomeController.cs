using System;
using System.Web.Mvc;
using AdminWeb.Models.DataModels;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using AdminWeb.CustomFilters;
using System.Configuration;

namespace AdminWeb.Controllers
{
    public class HomeController : Controller
    {
        [PageTittleAttributeActionFilter(Function = "Index_Home")]
        [AuthLog]
        public ActionResult Index()
        {
            return View();
        }
  

        [HttpPost]
        [AuthLog]
        public async Task<ActionResult> ChangeConfigs(PageConfigsModel model, string Token)
        {
            using (var client = new HttpClient())
            {
                HttpRequestMessage req = new HttpRequestMessage();
                req.Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                req.RequestUri = new Uri(ConfigurationManager.AppSettings["APIAddress"] + "/api/AdminPanelConfigs/PostAdminPanelConfig");
                req.Method = HttpMethod.Post;
                req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                HttpResponseMessage response = await client.SendAsync(req);
                var temp = response.StatusCode;
                return RedirectToAction("Index");
            }
        }

        [AuthLog]
        public ActionResult GetConfigs(string Token)
        {
            using (var client = new HttpClient())
            {
               
                    HttpRequestMessage req = new HttpRequestMessage();
                    req.RequestUri = new Uri( ConfigurationManager.AppSettings["APIAddress"]+"/api/AdminPanelConfigs/GetAdminPanelConfig");
                    req.Method = HttpMethod.Get;
                    req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                    HttpResponseMessage response = Task.Run(() => client.SendAsync(req)).Result;
                    var temp = response.StatusCode;
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string result = Task.Run(() => response.Content.ReadAsStringAsync()).Result;
                        var model = JsonConvert.DeserializeObject<PageConfigsModel>(result);
                        return PartialView(model);
                    }
                
            }
            var moddel = new PageConfigsModel();
            moddel.BackGround = "#DCE1E4"; moddel.BoxedLayout = false; moddel.FixedSidebar = true; moddel.FixedTopbar = true; moddel.SidebaronHover = false; moddel.SubmenuonHover = false; moddel.Theme = "sdtd"; moddel.Color = "#2B2E33";
            return PartialView(moddel);
        }
    }
}