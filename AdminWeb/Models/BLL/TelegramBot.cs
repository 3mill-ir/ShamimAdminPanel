using AdminWeb.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace AdminWeb.Models.BLL
{
    public class TelegramBot
    {
        public string SendMessage(string message)
        {
            string Chat_Id = GetChatId();
            string token = ConfigurationManager.AppSettings["TelegramToken"];
            string url = string.Format("https://api.telegram.org/bot{0}/sendMessage?chat_id={1}&text={2}", token, Chat_Id, message);
            var req = (HttpWebRequest)WebRequest.Create(url);
            var respons = req.GetResponse();
            var result = new StreamReader(respons.GetResponseStream()).ReadToEnd();
            int index = result.IndexOf(":");
            string IsOk = result.Substring(index + 1, result.IndexOf(",") - (index + 1));
            return IsOk;
        }
        private string GetChatId(string Token = "")
        {
            string Chat_Id = System.Configuration.ConfigurationManager.AppSettings["DefaultChat_Id"];
            if (!string.IsNullOrEmpty(Token))
            {
                ContactManagement cm = new ContactManagement(Tools.F_UserName(Token));
                var temp = cm.LoadContact();
                if (temp != null && !string.IsNullOrEmpty(temp.TelegramChannelID))
                    Chat_Id = temp.TelegramChannelID;
            }
            return "@" + Chat_Id;
        }
        public string SendPhoto(TelegramModel model, HttpPostedFileBase Path)
        {
            string Chat_Id = GetChatId();
            string result;
            using (var client = new System.Net.Http.HttpClient())
            {
                string token = System.Configuration.ConfigurationManager.AppSettings["TelegramToken"];
                var uri = new Uri("https://api.telegram.org/bot" + token + "/sendPhoto?chat_id=" + Chat_Id + "&caption=" + model.Caption);
                string corrected = Path.FileName.Replace(" ", "");
                byte[] fileBytes = null;
                using (var multipartFormDataContent = new System.Net.Http.MultipartFormDataContent())
                {
                    using (var binaryReader = new BinaryReader(Path.InputStream))
                    {
                        fileBytes = binaryReader.ReadBytes(Path.ContentLength);
                    }

                    var streamContent = new System.Net.Http.StreamContent(new System.IO.MemoryStream(fileBytes));
                    streamContent.Headers.Add("Content-Type", "application/octet-stream");
                    streamContent.Headers.Add("Content-Disposition", "form-data; name=\"photo\"; filename=" + corrected);

                    multipartFormDataContent.Add(streamContent, "file", Path.FileName);
                    using (var message = client.PostAsync(uri, multipartFormDataContent))
                    {
                        var contentString = message.Result.Content.ReadAsStringAsync().Result;
                        result = contentString.ToString();
                    }
                    int index = result.IndexOf(":");
                    string IsOk = result.Substring(index + 1, result.IndexOf(",") - (index + 1));
                    return IsOk;
                }

            }
        }
        public string SendVideo(TelegramModel model, HttpPostedFileBase Path)
        {
            string Chat_Id = GetChatId();
            string result;
            using (var client = new System.Net.Http.HttpClient())
            {
                string token = System.Configuration.ConfigurationManager.AppSettings["TelegramToken"];
                var uri = new Uri("https://api.telegram.org/bot" + token + "/sendVideo?chat_id=" + Chat_Id + "&caption=" + model.Caption);
                string corrected = Path.FileName.Replace(" ", "");
                byte[] fileBytes = null;
                using (var multipartFormDataContent = new System.Net.Http.MultipartFormDataContent())
                {
                    using (var binaryReader = new BinaryReader(Path.InputStream))
                    {
                        fileBytes = binaryReader.ReadBytes(Path.ContentLength);
                    }

                    var streamContent = new System.Net.Http.StreamContent(new System.IO.MemoryStream(fileBytes));
                    streamContent.Headers.Add("Content-Type", "application/octet-stream");
                    streamContent.Headers.Add("Content-Disposition", "form-data; name=\"video\"; filename=" + corrected);

                    multipartFormDataContent.Add(streamContent, "Video", Path.FileName);

                    using (var message = client.PostAsync(uri, multipartFormDataContent))
                    {
                        var contentString = message.Result.Content.ReadAsStringAsync().Result;
                        result = contentString.ToString();
                    }
                    int index = result.IndexOf(":");
                    string IsOk = result.Substring(index + 1, result.IndexOf(",") - (index + 1));
                    return IsOk;
                }

            }
        }
        public string SendVoice(TelegramModel model, HttpPostedFileBase Path)
        {
            string Chat_Id = GetChatId();
            string result;
            using (var client = new System.Net.Http.HttpClient())
            {
                string token = System.Configuration.ConfigurationManager.AppSettings["TelegramToken"];
                var uri = new Uri("https://api.telegram.org/bot" + token + "/sendVoice?chat_id=" + Chat_Id);
                string corrected = Path.FileName.Replace(" ", "");
                byte[] fileBytes = null;
                using (var multipartFormDataContent = new System.Net.Http.MultipartFormDataContent())
                {
                    using (var binaryReader = new BinaryReader(Path.InputStream))
                    {
                        fileBytes = binaryReader.ReadBytes(Path.ContentLength);
                    }

                    var streamContent = new System.Net.Http.StreamContent(new System.IO.MemoryStream(fileBytes));
                    streamContent.Headers.Add("Content-Type", "application/octet-stream");
                    streamContent.Headers.Add("Content-Disposition", "form-data; name=\"voice\"; filename=" + corrected);
                    multipartFormDataContent.Add(streamContent, "Voice", Path.FileName);
                    using (var message = client.PostAsync(uri, multipartFormDataContent))
                    {
                        var contentString = message.Result.Content.ReadAsStringAsync().Result;
                        result = contentString.ToString();
                    }
                    int index = result.IndexOf(":");
                    string IsOk = result.Substring(index + 1, result.IndexOf(",") - (index + 1));

                    return IsOk;
                }

            }
        }
        public string SendDocument(TelegramModel model, HttpPostedFileBase Path)
        {
            string Chat_Id = GetChatId();
            string result;
            using (var client = new System.Net.Http.HttpClient())
            {
                string token = System.Configuration.ConfigurationManager.AppSettings["TelegramToken"];
                var uri = new Uri("https://api.telegram.org/bot" + token + "/sendDocument?chat_id=" + Chat_Id);
                string corrected = Path.FileName.Replace(" ", "");
                byte[] fileBytes = null;
                using (var multipartFormDataContent = new System.Net.Http.MultipartFormDataContent())
                {
                    using (var binaryReader = new BinaryReader(Path.InputStream))
                    {
                        fileBytes = binaryReader.ReadBytes(Path.ContentLength);
                    }

                    var streamContent = new System.Net.Http.StreamContent(new System.IO.MemoryStream(fileBytes));
                    streamContent.Headers.Add("Content-Type", "application/octet-stream");
                    streamContent.Headers.Add("Content-Disposition", "form-data; name=\"document\"; filename=" + corrected);

                    multipartFormDataContent.Add(streamContent, "Document", Path.FileName);

                    using (var message = client.PostAsync(uri, multipartFormDataContent))
                    {
                        var contentString = message.Result.Content.ReadAsStringAsync().Result;
                        result = contentString.ToString();
                    }
                    int index = result.IndexOf(":");
                    string IsOk = result.Substring(index + 1, result.IndexOf(",") - (index + 1));
                    return IsOk;
                }

            }
        }
        public string SendContent(TelegramModel model, string Token)
        {
            string Chat_Id = GetChatId(Token);
            string result;
            using (var client = new System.Net.Http.HttpClient())
            {
                string token = System.Configuration.ConfigurationManager.AppSettings["TelegramToken"];
                var uri = new Uri("https://api.telegram.org/bot" + token + "/sendPhoto?chat_id=" + Chat_Id + "&caption=" + model.Caption);

                using (var multipartFormDataContent = new System.Net.Http.MultipartFormDataContent())
                {
                    //string F_UserName = Tools.F_UserName(Token);
                    string y = Tools.ReturnPathPhysicalMode("DynamicPageImages", Tools.F_UserName(Token), "SendContent()") + model.Path;
                    if (System.IO.File.Exists(y))
                    {
                        // System.Web.HttpContext.Current.Server.MapPath("~/Content/UplodedImages/PostImages/Posts/" + F_UserName + "/" + model.Path);
                        byte[] fileBytes = System.IO.File.ReadAllBytes(y);

                        var streamContent = new System.Net.Http.StreamContent(new System.IO.MemoryStream(fileBytes));
                        streamContent.Headers.Add("Content-Type", "application/octet-stream");
                        streamContent.Headers.Add("Content-Disposition", "form-data; name=\"photo\"; filename=" + model.Path);

                        multipartFormDataContent.Add(streamContent, "file", model.Path);
                        using (var message = client.PostAsync(uri, multipartFormDataContent))
                        {
                            var contentString = message.Result.Content.ReadAsStringAsync().Result;
                            result = contentString.ToString();
                        }
                        int index = result.IndexOf(":");
                        string IsOk = result.Substring(index + 1, result.IndexOf(",") - (index + 1));
                        return IsOk;
                    }
                    return "NOK";
                }

            }
        }
    }
}