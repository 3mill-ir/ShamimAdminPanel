using AdminWeb.Models.DataModels;
using fm.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace AdminWeb.Models.BLL
{
    public static class Tools
    {

        public static string PriceString(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                string Result = "";
                string temp = "";
                int count = 0;
                for (var i = str.Length - 1; i > -1; i--)
                {
                    temp = str[i] + "";
                    if (count == 3)
                    {
                        Result = temp + ',' + Result;
                        count = 0;
                    }
                    else
                        Result = temp + Result;
                    count++;
                }
                return Result.TrimEnd(',');
            }
            return "";
        }
        public static List<SelectListItem> MenuTypesCombo()
        {
            List<SelectListItem> Items = new List<SelectListItem>();
            Items.Add(new SelectListItem() { Text = "استاتیکی", Value = "StaticPost" });
            Items.Add(new SelectListItem() { Text = "داینامیکی", Value = "DynamicPost" });
            Items.Add(new SelectListItem() { Text = "هیچ کدام", Value = "NoneStaticDynamic" });
            return Items;
        }
        public static List<SelectListItem> FanbazarCategoryTypesCombo()
        {
            List<SelectListItem> Items = new List<SelectListItem>();
            Items.Add(new SelectListItem() { Text = "عرضه", Value = "FanbazarOffer" });
            Items.Add(new SelectListItem() { Text = "تقاضا", Value = "FanbazarDemand" });
            Items.Add(new SelectListItem() { Text = "عرضه و تقاصا", Value = "FanbazarOfferDemand" });
            Items.Add(new SelectListItem() { Text = "شرکت", Value = "FanbazarCompany" });
            return Items;
        }

        public static List<SelectListItem> GetStates()
        {
            List<SelectListItem> Items = new List<SelectListItem>();
            Items.Add(new SelectListItem() { Text = "استان محل استقرار ...", Value = "", Selected = true });
            Items.Add(new SelectListItem() { Text = "آذربایجان غربی", Value = "1" });
            Items.Add(new SelectListItem() { Text = "آذربایجان شرقی", Value = "2" });
            Items.Add(new SelectListItem() { Text = "تهران", Value = "3" });
            return Items;
        }
        public static List<SelectListItem> GetCity(int ID = 0)
        {
            List<SelectListItem> Items = new List<SelectListItem>();
            if (ID == 0)
                Items.Add(new SelectListItem() { Text = "شهر محل استقرار ...", Value = "", Selected = true });
            if (ID == 1)
            {
                Items.Add(new SelectListItem() { Text = "ارومیه", Value = "1" });
                Items.Add(new SelectListItem() { Text = "مهاباد", Value = "2" });
                Items.Add(new SelectListItem() { Text = "اشنویه", Value = "3" });
            }
            else if (ID == 2)
            {
                Items.Add(new SelectListItem() { Text = "تبریز", Value = "4" });
            }
            else if (ID == 3)
                Items.Add(new SelectListItem() { Text = "تهران", Value = "5" });
            return Items;
        }
        public static SelectList ChartTypes(string selected = null)
        {
            List<SelectListItem> Items = new List<SelectListItem>();
            Items.Add(new SelectListItem { Text = "نمودار دایره ای", Value = "PieChart" });
            Items.Add(new SelectListItem { Text = "نمودار حلقه ای", Value = "DoughnutChart" });
            Items.Add(new SelectListItem { Text = "نمودار میله ای", Value = "BarChart" });
            return new SelectList(Items, "Value", "Text", selected);
        }
        public static string GetHtmldetail(string ContentFour, string F_UserName)
        {
            string path = Tools.ReturnPathPhysicalMode("DetailHTMLFilePath", F_UserName, "GetHtmldetail()");

            try
            {
                return System.IO.File.ReadAllText(path + ContentFour);
            }
            catch
            {
                return "خطا در عملیات پردازش متن";
            }


        }
        public static string SaveHtmlDetail(string ContentFour, string F_UserName, string FileName = null)
        {

            string path = ReturnPathPhysicalMode("DetailHTMLFilePath", F_UserName, "SaveDetail()");
            string RandomValueString;

            string extension = ".html";
            string curFile;
            if (!string.IsNullOrEmpty(FileName))
            {
                if (File.Exists(path + FileName))
                {
                    File.Delete(path + FileName);
                }
            }
            Random rnd = new Random();
            do
            {

                const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                RandomValueString = new string(Enumerable.Repeat(chars, 12)
                 .Select(s => s[rnd.Next(s.Length)]).ToArray());
                curFile = path + RandomValueString + extension;  //Your path

            } while (File.Exists(curFile));

            System.IO.File.WriteAllText(curFile, ContentFour, Encoding.UTF8);
            return RandomValueString + extension;
        }

        public static List<SelectListItem> Statuses()
        {
            List<SelectListItem> Items = new List<SelectListItem>();
            Items.Add(new SelectListItem { Text = "تایید", Value = "Accepted" });
            Items.Add(new SelectListItem { Text = "در حال بررسی", Value = "Review" });
            Items.Add(new SelectListItem { Text = "رویت شده", Value = "Seen" });
            Items.Add(new SelectListItem { Text = "ارسال شده", Value = "Sent" });
            Items.Add(new SelectListItem { Text = "مردود", Value = "Reject" });
            return Items;
        }


        public static SelectList ComponentTypes()
        {
            List<SelectListItem> Items = new List<SelectListItem>();
            Items.Add(new SelectListItem { Text = "تکست باکس", Value = "TextBox" });
            Items.Add(new SelectListItem { Text = "تکست باکس بزرگ", Value = "RichTextBox" });
            Items.Add(new SelectListItem { Text = "چک باکس", Value = "CheckBox" });
            Items.Add(new SelectListItem { Text = "چک باکس (کلید)", Value = "CheckBox2" });
            Items.Add(new SelectListItem { Text = "چک باکس (چند انتخابی)", Value = "MultiCheckBox" });
            Items.Add(new SelectListItem { Text = "عدد", Value = "Number" });
            Items.Add(new SelectListItem { Text = "رادیو باتن", Value = "RadioButton" });
            Items.Add(new SelectListItem { Text = "کومبو باکس", Value = "ComboBox" });
            Items.Add(new SelectListItem { Text = "تاریخ", Value = "DateTime" });
            Items.Add(new SelectListItem { Text = "فایل یا تصویر", Value = "Media" });
            Items.Add(new SelectListItem { Text = "متن توضیحی", Value = "InfText" });
            return new SelectList(Items, "Value", "Text");
        }

        public static SelectList ShopStates()
        {
            List<SelectListItem> Items = new List<SelectListItem>();
            //Items.Add(new SelectListItem { Text = "سفارش تایید نشده", Value = "Initiated" });
            Items.Add(new SelectListItem { Text = "در صف بررسی", Value = "Ordered" });
            Items.Add(new SelectListItem { Text = "در حال آماده سازی سفارش", Value = "Processing" });
            Items.Add(new SelectListItem { Text = "ارسال مرسوله", Value = "Delivered" });
            Items.Add(new SelectListItem { Text = "سفارش تحویل داده شده", Value = "Recieved" });
            return new SelectList(Items, "Value", "Text");
        }



        public static SelectList LanguagesCombo(string Token, string selected = null)
        {
            var Result = Task.Run(() => Tools.GetObjectFromRequestAsync(ConfigurationManager.AppSettings["APIAddress"] + "/api/Utility/getLanguages", Token, new List<LanguageDataModel>())).Result;
            var Object = JsonConvert.DeserializeObject<List<LanguageDataModel>>(Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            List<SelectListItem> Items = new List<SelectListItem>();
            foreach (var item in Object)
                Items.Add(new SelectListItem() { Text = item.LanguageName + "-" + item.Language, Value = item.Language });
            return new SelectList(Items, "Value", "Text", selected);
        }

        public static string ReturnPath(string ConfigPath, string F_UserName, string Caller)
        {
            NameValueCollection section = (NameValueCollection)ConfigurationManager.GetSection("UsersFoldersPath");
            string Path = string.Format(section[ConfigPath], F_UserName);
            MakeVaidPath(Path, Caller, false, F_UserName);
            return Path;
        }

        public static string ReturnPathPhysicalMode(string ConfigPath, string DomainAddress, string Caller, string F_UserName = "")
        {
            NameValueCollection section = (NameValueCollection)ConfigurationManager.GetSection("UsersFoldersPath");
            string Path = ConfigurationManager.AppSettings[DomainAddress] + string.Format(section[ConfigPath], F_UserName);
            return Path;
        }

        public static string ImageSave_Gallery(HttpPostedFileBase Content_Two, string PathForSave)
        {
            string path = PathForSave;
            string extension = Path.GetExtension(Content_Two.FileName);
            string curFile = "";
            string RandomValueString;
            Random rnd = new Random();
            do
            {

                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                RandomValueString = new string(Enumerable.Repeat(chars, 12)
                 .Select(s => s[rnd.Next(s.Length)]).ToArray());
                curFile = path + "/" + RandomValueString + extension;  //Your path
            } while (File.Exists(curFile));
            WebImage img = new WebImage(Content_Two.InputStream);
            string newextension = img.ImageFormat;
            if (newextension.ToLower() == "jpeg")
            {
                newextension = "jpg";
            }
            if (img.Width < 790 || img.Height < 460)
            {
                int wi;
                int hi;
                // maintain the aspect ratio despite the thumbnail size parameters
                if (img.Width > img.Height)
                {
                    wi = 790;
                    hi = (int)(img.Height * ((decimal)790 / img.Width));
                }
                else
                {
                    hi = 460;
                    wi = (int)(img.Width * ((decimal)460 / img.Height));
                }
                img.Resize(wi, hi);
            }
            img.Save(path + "/" + RandomValueString + "." + newextension);

            return RandomValueString + "." + newextension;
        }

        public static string ReturnPathPhysicalMode(string ConfigPath, string F_UserName, string Caller)
        {
            NameValueCollection section = (NameValueCollection)ConfigurationManager.GetSection("UsersFoldersPath");
            string Path = HttpContext.Current.Server.MapPath("~" + string.Format(section[ConfigPath], F_UserName));
            MakeVaidPath(Path, Caller, true, F_UserName);
            return Path;
        }

        public static void MakeVaidPath(string Path, string Caller, bool isPhysicalPath, string F_UserName)
        {
            if (!isPhysicalPath)
            {
                Path = HttpContext.Current.Server.MapPath(Path);
            }
            if (!System.IO.Directory.Exists(Path))
            {
                System.IO.Directory.CreateDirectory(Path);
                PipoLog(string.Format("Log : Directory Not Exist <<{0}>> Called by {1} At Time {2} For User {3}", Path, Caller, DateTime.Now, F_UserName));
            }
        }
        public static void PipoLog(string Content)
        {
            System.IO.File.AppendAllText(HttpContext.Current.Server.MapPath("~/App_Data/PipoLog/PipoLog.txt"), Content + Environment.NewLine);
        }

        public static List<SelectListItem> MenuWeightsCombo()
        {
            List<SelectListItem> Weights = new List<SelectListItem>();
            for (int i = 0; i < 10; i++)
                Weights.Add(new SelectListItem() { Text = i + "", Value = i + "" });
            return Weights;
        }


        //public static List<SelectListItem> F_MenuIDs(string Token)
        //{
        //    List<SelectListItem> F_MenuIDs = new List<SelectListItem>();
        //    var Result = Task.Run(() => Tools.GetObjectFromRequestAsync(ConfigurationManager.AppSettings["APIAddress"] + "/api/menues/GetMenuAll", Token, new List<MenuDataModel>())).Result;
        //    var Object = JsonConvert.DeserializeObject<List<MenuDataModel>>(Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
        //    Object.RemoveAll(item => item == null);
        //    F_MenuIDs.Add(new SelectListItem() { Text = "انتخاب زیر منو ...", Value = "" });
        //    foreach (var item in Object)
        //    {
        //        F_MenuIDs.Add(new SelectListItem() { Text = item.Name, Value = item.ID + "" });
        //    }
        //    return F_MenuIDs;
        //}

        public static SelectList F_CategoryIDs(string lang, string Token)
        {
            List<SelectListItem> F_CategoryIDs = new List<SelectListItem>();
            try
            {
                var Result = Task.Run(() => Tools.GetObjectFromRequestAsync(ConfigurationManager.AppSettings["APIAddress"] + "/api/Menues/GetFanbazarMenuAll?type=OfferFanbazar", Token, new List<MenuDataModel>())).Result;
                var Object = JsonConvert.DeserializeObject<List<MenuDataModel>>(Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                Object.RemoveAll(item => item == null);
                F_CategoryIDs.Add(new SelectListItem() { Text = "انتخاب دسته بندی سطح بالاتر ...", Value = "" });
                foreach (var item in Object)
                    F_CategoryIDs.Add(new SelectListItem() { Text = item.Name, Value = item.ID + "" });
                return new SelectList(F_CategoryIDs, "Value", "Text");
            }
            catch { return Tools.F_CategoryIDs(lang,Token); }
        }

        public static SelectList F_MenuIDs(string username, List<string> type, List<string> lang, string Token, int? selected = null, int? FirstItem = null)
        {
            var typelist = HttpUtility.ParseQueryString("");
            type.ForEach(s => typelist.Add("type", s));
            var langlist = HttpUtility.ParseQueryString("");
            lang.ForEach(s => langlist.Add("lang", s));
            List<SelectListItem> F_MenuIDs = new List<SelectListItem>();
            try
            {
                var Result = Task.Run(() => Tools.GetObjectFromRequestAsync(ConfigurationManager.AppSettings["APIAddress"] + "/api/menues/GetMenuAll?username=" + username + "&status=*&" + typelist + "&" + langlist, Token, new List<MenuDataModel>())).Result;
                var Object = JsonConvert.DeserializeObject<List<MenuDataModel>>(Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                if (FirstItem != null)
                {
                    F_MenuIDs.Add(new SelectListItem() { Text = "همه دسته بندی ها", Value = "" });
                }
                else
                {
                    F_MenuIDs.Add(new SelectListItem() { Text = "انتخاب زیر منو ...", Value = "" });
                }
                foreach (var item in Object)
                {
                    var NameObj = item;
                    string MenuName = "";
                    bool isfirst = true;
                    while (NameObj != null)
                    {
                        MenuName = isfirst ? NameObj.Name : NameObj.Name + " > " + MenuName;

                        NameObj = Object.FirstOrDefault(i => i.ID == NameObj.F_MenuID);
                        isfirst = false;
                    }
                    F_MenuIDs.Add(new SelectListItem() { Text = MenuName, Value = item.ID + "" });
                }
                return new SelectList(F_MenuIDs, "Value", "Text", selected);
            }
            catch { return Tools.F_MenuIDs(username, type, lang, Token, selected, FirstItem); }
        }

        public static List<MenuDataModel> UserMenu(string username, List<string> type, string Token)
        {
            var typelist = HttpUtility.ParseQueryString("");
            type.ForEach(s => typelist.Add("type", s));
            try
            {
                var Result = Task.Run(() => Tools.GetObjectFromRequestAsync(ConfigurationManager.AppSettings["APIAddress"] + "/api/menues/GetMenuAll?username=" + username + "&status=*&" + typelist, Token, new List<MenuDataModel>())).Result;
                var Object = JsonConvert.DeserializeObject<List<MenuDataModel>>(Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                Object.RemoveAll(item => item == null);

                return Object;
            }
            catch { return UserMenu(username, type, Token); }
        }

        public static string F_UserName(string Token)
        {
            using (var client = new HttpClient())
            {
                HttpRequestMessage req = new HttpRequestMessage();
                //req.RequestUri = new Uri(ConfigurationManager.AppSettings["APIAddress"] + "/api/account/isAuthorized");
                req.RequestUri = new Uri(ConfigurationManager.AppSettings["APIAddress"] + "/api/Account/GetRootUserProfile");
                req.Method = HttpMethod.Get;
                req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                try
                {
                    HttpResponseMessage response = Task.Run(() => client.SendAsync(req)).Result;
                    if (response.StatusCode == HttpStatusCode.OK)
                        return JsonConvert.DeserializeObject<AccountAuthorizeModel>(Task.Run(() => response.Content.ReadAsStringAsync()).Result).UserName;
                    return "NotFound";
                }
                catch { return F_UserName(Token); }
            }
        }
        public static AccountAuthorizeModel FoundUser(string Token, string UserID)
        {
            using (var client = new HttpClient())
            {
                HttpRequestMessage req = new HttpRequestMessage();
                req.RequestUri = new Uri(ConfigurationManager.AppSettings["APIAddress"] + "/api/account/GetRootUserProfile?UserID=" + UserID);
                req.Method = HttpMethod.Get;
                req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                try
                {
                    HttpResponseMessage response = Task.Run(() => client.SendAsync(req)).Result;
                    if (response.StatusCode == HttpStatusCode.OK)
                        return JsonConvert.DeserializeObject<AccountAuthorizeModel>(Task.Run(() => response.Content.ReadAsStringAsync()).Result);
                    return new AccountAuthorizeModel() { UserName = "NotFound" };
                }
                catch { return FoundUser(Token, UserID); }
            }
        }

        public static List<SelectListItem> DynamicPagesCombo(string username, List<string> type, List<string> lang, string Token)
        {
            var typelist = HttpUtility.ParseQueryString("");
            type.ForEach(s => typelist.Add("type", s));
            var langlist = HttpUtility.ParseQueryString("");
            lang.ForEach(s => langlist.Add("lang", s));
            List<SelectListItem> Parrents = new List<SelectListItem>();
            try
            {
                string Result = Task.Run(() => Tools.GetObjectFromRequestAsync(ConfigurationManager.AppSettings["APIAddress"] + "/api/menues/GetMenuAll?username=" + username + "&status=*&" + typelist + "&" + langlist, Token, new List<MenuDataModel>())).Result;
                var temp = JsonConvert.DeserializeObject<List<MenuDataModel>>(Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                temp.RemoveAll(item => item == null);
                foreach (var item in temp)
                    Parrents.Add(new SelectListItem() { Text = item.Name, Value = item.ID + "" });
                return Parrents;
            }
            catch { return DynamicPagesCombo(username, type, lang, Token); }

        }



        public static async System.Threading.Tasks.Task<HttpStatusCode> SendRequestToUrl(dynamic Obj, string Url, string Token, HttpMethod method)
        {
            using (var client = new HttpClient())
            {
                HttpRequestMessage req = new HttpRequestMessage();
                req.Content = new StringContent(JsonConvert.SerializeObject(Obj), Encoding.UTF8, "application/json");
                req.RequestUri = new Uri(Url);
                req.Method = method;
                req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                try
                {
                    HttpResponseMessage response = await client.SendAsync(req);
                    return response.StatusCode;
                }
                catch { return SendRequestToUrl(Obj, Url, Token, method); }
            }
        }


        public static async System.Threading.Tasks.Task<string> SendRequestToUrlGetObjectAsync(dynamic Obj, string Url, string Token, HttpMethod method)
        {
            using (var client = new HttpClient())
            {
                string result = "";
                HttpRequestMessage req = new HttpRequestMessage();
                req.Content = new StringContent(JsonConvert.SerializeObject(Obj), Encoding.UTF8, "application/json");
                req.RequestUri = new Uri(Url);
                req.Method = method;
                req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                try
                {
                    HttpResponseMessage response = await client.SendAsync(req);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        result = response.Content.ReadAsStringAsync().Result;
                    }
                    return result;
                }
                catch { return SendRequestToUrlGetObjectAsync(Obj, Url, Token, method); }
            }
        }

        public static string SendRequestToUrlGetObject(dynamic Obj, string Url, string Token, HttpMethod method)
        {
            using (var client = new HttpClient())
            {
                string result = "";
                HttpRequestMessage req = new HttpRequestMessage();
                req.Content = new StringContent(JsonConvert.SerializeObject(Obj), Encoding.UTF8, "application/json");
                req.RequestUri = new Uri(Url);
                req.Method = method;
                req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                try
                {
                    HttpResponseMessage response = Task.Run(() => client.SendAsync(req)).Result;
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        result = response.Content.ReadAsStringAsync().Result;
                    }
                    return result;
                }
                catch { return SendRequestToUrlGetObject(Obj, Url, Token, method); }
            }
        }

        public static async System.Threading.Tasks.Task<string> GetObjectFromRequestAsync(string Url, string Token, object Output)
        {
            using (var client = new HttpClient())
            {
                string result = "";
                HttpRequestMessage req = new HttpRequestMessage();
                req.RequestUri = new Uri(Url);
                req.Method = HttpMethod.Get;
                req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                try
                {
                    HttpResponseMessage response = await client.SendAsync(req);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        result = response.Content.ReadAsStringAsync().Result;
                    }
                    return result;
                }
                catch { return await GetObjectFromRequestAsync(Url, Token, Output); }
            }
        }

        public static string GetObjectFromRequest(string Url, string Token, object Output)
        {
            using (var client = new HttpClient())
            {
                string result = "";
                HttpRequestMessage req = new HttpRequestMessage();
                req.RequestUri = new Uri(Url);
                req.Method = HttpMethod.Get;
                req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                try
                {
                    HttpResponseMessage response = Task.Run(() => client.SendAsync(req)).Result;
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        result = response.Content.ReadAsStringAsync().Result;
                    }
                    return result;
                }
                catch { return GetObjectFromRequest(Url, Token, Output); }
            }
        }
        public static string ImageSave(HttpPostedFileBase Content_Two, string Type, string F_UserName = "")
        {
            if (Content_Two != null)
            {
                string path = Tools.ReturnPathPhysicalMode(Type, F_UserName, "ImageSave");
                string extension = Path.GetExtension(Content_Two.FileName);
                string curFile = "";
                string RandomValueString;
                Random rnd = new Random();
                do
                {
                    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                    RandomValueString = new string(Enumerable.Repeat(chars, 12)
                     .Select(s => s[rnd.Next(s.Length)]).ToArray());
                    curFile = path + "/" + RandomValueString + extension;  //Your path
                } while (File.Exists(curFile));
                WebImage img = new WebImage(Content_Two.InputStream);
                string newextension = img.ImageFormat;
                if (newextension.ToLower() == "jpeg")
                {
                    newextension = "jpg";
                }
                if (img.Width < 790 || img.Height < 460)
                {
                    int wi;
                    int hi;
                    // maintain the aspect ratio despite the thumbnail size parameters
                    if (img.Width > img.Height)
                    {
                        wi = 790;
                        hi = (int)(img.Height * ((decimal)790 / img.Width));
                    }
                    else
                    {
                        hi = 460;
                        wi = (int)(img.Width * ((decimal)460 / img.Height));
                    }
                    img.Resize(wi, hi);
                }
                img.Save(path + "/" + RandomValueString + "." + newextension);
                return RandomValueString + "." + newextension;
            }
            else
                return "NotSaved";
        }

        //public static string ReturnPathPhysicalMode(string ConfigPath)
        //{
        //    NameValueCollection section = (NameValueCollection)ConfigurationManager.GetSection("UsersFoldersPath");
        //    string Path = HttpContext.Current.Server.MapPath("~" + section[ConfigPath]);
        //    MakeVaidPath(Path, true);
        //    return Path;
        //}

        public static void MakeVaidPath(string Path, bool isPhysicalPath)
        {
            if (!isPhysicalPath)
                Path = HttpContext.Current.Server.MapPath(Path);
            if (!System.IO.Directory.Exists(Path))
                System.IO.Directory.CreateDirectory(Path);
        }



        public static string ConvertNativeDigits(this string text)
        {

            if (text == null)
                return null;
            if (text.Length == 0)
                return string.Empty;
            StringBuilder sb = new StringBuilder();
            foreach (char character in text)
            {
                if (char.IsDigit(character))
                    sb.Append(char.GetNumericValue(character));
                else
                    sb.Append(character);
            }
            return sb.ToString();


        }
        private static readonly CultureInfo arabic = new CultureInfo("fa-IR");
        private static readonly CultureInfo latin = new CultureInfo("en-US");

        /// <summary>
        /// in tabe jahate tabdile zabane english be arabic ( ta hududi farsi ) estefade mishavad
        /// </summary>
        /// <param name="input">reshteye morede nazar baraye tabdil</param>
        /// <returns>
        /// string
        /// reshteye tabdil shode
        /// </returns>
        public static string ToArabic(string input)
        {
            var arabicDigits = arabic.NumberFormat.NativeDigits;
            for (int i = 0; i < arabicDigits.Length; i++)
            {
                input = input.Replace(i.ToString(), arabicDigits[i]);
            }
            return input;
        }

        public static string ToLatin(string input)
        {
            var latinDigits = latin.NumberFormat.NativeDigits;
            var arabicDigits = arabic.NumberFormat.NativeDigits;
            for (int i = 0; i < latinDigits.Length; i++)
            {
                input = input.Replace(arabicDigits[i], latinDigits[i]);
            }
            return input;
        }

        /// <summary>
        /// in tabe tarikhe miladi ra dar forme datetime gerefte va tarikhe jalali ra dar forme string baz migardanad
        /// </summary>
        /// <param name="date">tarikhe morede nazar jahate tabdil</param>
        /// <returns>
        /// string
        /// tarikhe tabdil shode be jalali
        /// </returns>
        public static string GetDateTimeReturnJalaliDate(DateTime date)
        {
            if (date != DateTime.MinValue)
            {
                PersianCalendar p = new PersianCalendar();
                int Month = p.GetMonth(date);
                int Year = p.GetYear(date);
                int Day = p.GetDayOfMonth(date);
                int Hour = p.GetHour(date);
                int Minute = p.GetMinute(date);
                int Second = p.GetSecond(date);
                string result1 = "";
                string result = Tools.ToArabic(Year.ToString()) + '/';
                if (Month.ToString().Count() == 2)
                    result += Tools.ToArabic(Month.ToString()) + '/';
                else
                    result += '۰' + Tools.ToArabic(Month.ToString()) + '/';
                if (Day.ToString().Count() == 2)
                    result += Tools.ToArabic(Day.ToString());
                else
                    result += '۰' + Tools.ToArabic(Day.ToString());
                if (Hour.ToString().Count() == 2)
                    result1 += Tools.ToArabic(Hour.ToString()) + ':';
                else
                    result1 += '۰' + Tools.ToArabic(Hour.ToString()) + ':';
                if (Minute.ToString().Count() == 2)
                    result1 += Tools.ToArabic(Minute.ToString()) + ':';
                else
                    result1 += '۰' + Tools.ToArabic(Minute.ToString()) + ':';
                if (Second.ToString().Count() == 2)
                    result1 += Tools.ToArabic(Second.ToString());
                else
                    result1 += '۰' + Tools.ToArabic(Second.ToString());
                string FinalResult = result + " " + result1;

                return FinalResult;
            }
            return "";
        }
        public static string JalaliDateWithoutHour(DateTime date)
        {
            PersianCalendar p = new PersianCalendar();
            int Month = p.GetMonth(date);
            int Year = p.GetYear(date);
            int Day = p.GetDayOfMonth(date);
            string result = Tools.ToArabic(Year.ToString()) + '/';
            if (Month.ToString().Count() == 2)
                result += Tools.ToArabic(Month.ToString()) + '/';
            else
                result += '۰' + Tools.ToArabic(Month.ToString()) + '/';
            if (Day.ToString().Count() == 2)
                result += Tools.ToArabic(Day.ToString());
            else
                result += '۰' + Tools.ToArabic(Day.ToString());

            return result;
        }

        /// <summary>
        /// in tabe tarikhe jalali ra dar forme string gerefte va tarikhe miladi ra dar forme datetime baz migardanad
        /// </summary>
        /// <param name="date">tarikhe jalaliye morede nazar dar forme string</param>
        /// <param name="_Date"></param>
        /// <returns>
        ///  
        /// </returns>
        public static bool GetJalaliDateReturnDateTime(string date, out DateTime _Date)
        {
            if (!string.IsNullOrEmpty(date))
            {
                Regex rex = new Regex(@"^[۰-۹0-9]{4}\/[۰-۹0-9]{2}\/[۰-۹0-9]{2} [۰-۹0-9]{2}:[۰-۹0-9]{2}:[۰-۹0-9]{2}$");
                if (rex.Match(date).Success)
                {
                    string firstpart = date.Substring(0, date.IndexOf(':') - 2);
                    string SecondPart = date.Substring(date.IndexOf(':') - 2);
                    string[] persianDatePartsStart = firstpart.Split('/');
                    string[] persianDatePartsStartHour = SecondPart.Split(':');


                    int persianYearStart = int.Parse(Tools.ConvertNativeDigits(persianDatePartsStart[0]));
                    int persianMonthStart = int.Parse(Tools.ConvertNativeDigits(persianDatePartsStart[1]));
                    int persianDayStart = int.Parse(Tools.ConvertNativeDigits(persianDatePartsStart[2]));
                    int persianHourStart = int.Parse(Tools.ConvertNativeDigits(persianDatePartsStartHour[0]));
                    int persianMinStart = int.Parse(Tools.ConvertNativeDigits(persianDatePartsStartHour[1]));
                    int persianSecondStart = int.Parse(Tools.ConvertNativeDigits(persianDatePartsStartHour[2]));

                    string datetimeString = string.Format("{0}-{1}-{2} {3}:{4}:{5}", persianYearStart, persianMonthStart, persianDayStart, persianHourStart, persianMinStart, persianSecondStart);

                    PersianCalendar pc = new PersianCalendar();
                    try
                    {
                        DateTime start = new DateTime(persianYearStart, persianMonthStart, persianDayStart, persianHourStart, persianMinStart, persianSecondStart, pc);
                        _Date = start;
                        return true; ;
                    }
                    catch
                    {
                        _Date = DateTime.Now;
                        return false;
                    }
                }
            }
            _Date = DateTime.Now;
            return false; ;
        }


        public static List<string> AllowedExtentions(string Type = "Default")
        {
            if (Type == "Img")
                return new List<string> { ".gif", ".jpg", ".jpeg", ".bmp", ".png" };
            return new List<string> { ".doc", ".xlsx", ".txt", ".pdf", ".ppt", ".gif", ".jpg", ".jpeg", ".bmp", ".png", ".m4a", ".mp3", ".wav" };
        }
        public static string FileSave(HttpPostedFileBase Content_Two, string PathForSave)
        {

            string path = PathForSave;
            string extension = Path.GetExtension(Content_Two.FileName);
            string curFile = "";
            string RandomValueString;
            Random rnd = new Random();
            do
            {

                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                RandomValueString = new string(Enumerable.Repeat(chars, 12)
                 .Select(s => s[rnd.Next(s.Length)]).ToArray());
                curFile = path + "/" + RandomValueString + extension;  //Your path
            } while (File.Exists(curFile));
            var allowedExtensions = Tools.AllowedExtentions("Img");
            var extensions = Path.GetExtension(Content_Two.FileName).ToLower();
            if (allowedExtensions.Contains(extensions))
            {
                string pic = System.IO.Path.GetFileName(RandomValueString + extension);
                WebImage img = new WebImage(Content_Two.InputStream);
                if (img.Width < 790 || img.Height < 460)
                {
                    int wi;
                    int hi;
                    // maintain the aspect ratio despite the thumbnail size parameters
                    if (img.Width > img.Height)
                    {
                        wi = 790;
                        hi = (int)(img.Height * ((decimal)790 / img.Width));
                    }
                    else
                    {
                        hi = 460;
                        wi = (int)(img.Width * ((decimal)460 / img.Height));
                    }
                    img.Resize(wi, hi);
                }
                img.Save(path + pic);
            }
            else
            {
                string pic = System.IO.Path.GetFileName(RandomValueString + extension);
                byte[] tempFile = new byte[Content_Two.ContentLength];
                Content_Two.InputStream.Read(tempFile, 0, Content_Two.ContentLength);

                System.IO.File.WriteAllBytes(path + pic, tempFile);
            }
            return RandomValueString + extension;
        }


        public static string GetFileType(string ext)
        {
            var Voice = new List<string> { "m4a", "mp3", "wav" };
            var Video = new List<string> { "mp4", "flv", "mkv", "3gp" };
            var File = new List<string> { "doc", "xlsx", "txt", "pdf", "ppt" };
            if (Voice.Contains(ext)) return "voice";
            else if (Video.Contains(ext)) return "video";
            else if (File.Contains(ext)) return "file";
            else return "image";
        }
    }
}