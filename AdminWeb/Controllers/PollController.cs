using AdminWeb.CustomFilters;
using AdminWeb.Models.BLL;
using AdminWeb.Models.DataModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AdminWeb.Controllers
{
    public class PollController : Controller
    {
        [AuthLog]
        [PageTittleAttributeActionFilter(Function = "ListPollQuestion_Poll")]
        public async Task<ActionResult> ListPollQuestion(string Token)
        {
            ViewBag.Notification = TempData["Notification"];
            PollQuestionManagement poll = new PollQuestionManagement();
            return View(await poll.ListPollQuestion(Token));
        }
        [AuthLog]
        [PageTittleAttributeActionFilter(Function = "AddPollQuestion_Poll")]
        public ActionResult AddPollQuestion(string Token)
        {
            PollQuestionModel model = new PollQuestionModel();
            model.PollAnswer.Add(new PollAnswerDataModel() { ID = 0 });
            model.PollAnswer.Add(new PollAnswerDataModel() { ID = 1 });
            ViewBag.ChartTypes = Tools.ChartTypes();
            return View(model);
        }

        [HttpPost]
        [AuthLog]
        [PageTittleAttributeActionFilter(Function = "AddPollQuestion_Poll")]
        public async Task<ActionResult> AddPollQuestion(PollQuestionModel model, string Token)
        {
            DateTime start = new DateTime();
            DateTime end = new DateTime();
            if (!Tools.GetJalaliDateReturnDateTime(model.StartDateOnUTCHelper, out start))
            {
                ModelState.AddModelError("StartDateOnUTC", @Resource.Resource.View_DateFormatError);
            }
            if (!Tools.GetJalaliDateReturnDateTime(model.EndDateOnUTCHelper, out end))
            {
                ModelState.AddModelError("EndDateOnUTC", @Resource.Resource.View_DateFormatError);
            }


            if (ModelState.IsValid)
            {
                PollQuestionManagement poll = new PollQuestionManagement();
                string scale1 =await poll.AddPollQuestion(model, start, end,Token);
                if (scale1 == "OK")
                {
                    TempData["Notification"] = "success";
                    return RedirectToAction("ListPollQuestion", "Poll");
                }
                else
                {
                    ViewBag.ChartTypes = Tools.ChartTypes();
                    ViewBag.Notification = Resource.Resource.View_DateConflictError;
                    return View(model);
                }
            }
            else
            {
                ViewBag.ChartTypes = Tools.ChartTypes();
                ViewBag.Notification = Resource.Resource.View_DateConflictError;

                return View(model);
            }
        }
        [AuthLog]
        [PageTittleAttributeActionFilter(Function = "EditPollQuestion_Poll")]
        public async Task<ActionResult> EditPollQuestion(int PollQuestionId, string Token)
        {
          
            PollQuestionManagement post = new PollQuestionManagement();
            var model = await post.PollQuestionDetail(PollQuestionId, Token);
            model.StartDateOnUTCHelper= Tools.GetDateTimeReturnJalaliDate(model.StartDateOnUTC ??default(DateTime));

            model.EndDateOnUTCHelper = Tools.GetDateTimeReturnJalaliDate(model.EndDateOnUTC ?? default(DateTime));
            ViewBag.ChartTypes = Tools.ChartTypes(model.ChartType);

            return View(model);
        }
        [HttpPost]
        [AuthLog]
        [PageTittleAttributeActionFilter(Function = "EditPollQuestion_Poll")]
        public async Task<ActionResult> EditPollQuestion(PollQuestionModel model, string Token)
        {
           
            DateTime start = new DateTime();
            DateTime end = new DateTime();
            if (!Tools.GetJalaliDateReturnDateTime(model.StartDateOnUTCHelper, out start))
                ModelState.AddModelError("StartDateOnUTC", Resource.Resource.View_DateFormatError);
            if (!Tools.GetJalaliDateReturnDateTime(model.EndDateOnUTCHelper, out end))
                ModelState.AddModelError("EndDateOnUTC", Resource.Resource.View_DateFormatError);
            if (ModelState.IsValid)
            {
                PollQuestionManagement poll = new PollQuestionManagement();
                string result =await poll.EditPollQuestion(model, start, end, Token);
                if (result == "NotFound")
                    return View("~/Views/Shared/NotFoundFailed.cshtml");
                else if (result == "Conflict")
                {
                    ViewBag.ChartTypes = Tools.ChartTypes(model.ChartType);
                    ViewBag.Notification = Resource.Resource.View_DateConflictError;
                    return View(model);
                }
                else if (result == "OK")
                {
                    TempData["Notification"] = "success";
                    return RedirectToAction("ListPollQuestion", "Poll");
                }
                else
                {
                    ViewBag.ChartTypes = Tools.ChartTypes(model.ChartType);
                    ViewBag.Notification = Resource.Resource.View_DateConflictError;
                    return View(model);
                }
            }
            ViewBag.ChartTypes = Tools.ChartTypes(model.ChartType);
            return View(model);
        }

        [HttpPost]
        [AuthLog]
        public async Task<ActionResult> DeletePoll(int PollQuestionId, string Token)
        {
            PollQuestionManagement post = new PollQuestionManagement();
            if (await post.DeletePollQuestion(PollQuestionId, Token) == "NotFound") { TempData["Notification"] = "danger"; return View("~/Views/Shared/NotFoundFailed.cshtml"); }
            TempData["Notification"] = "success";
            return RedirectToAction("ListPollQuestion", "Poll");
        }
        [PageTittleAttributeActionFilter(Function = "DetailPollQuestion_Poll")]
        [AuthLog]
        public async Task<ActionResult> DetailPollQuestion(int PollQuestionId, string Token)
        {
            string temp = (TempData["DeleteError"] == null) ? null : TempData["DeleteError"].ToString();
            @ViewBag.ErrorinDelete = temp;
            PollQuestionManagement post = new PollQuestionManagement();
            PollQuestionModel model = new PollQuestionModel();
            model =await post.PollQuestionDetail(PollQuestionId, Token);
            if (model == null) { return View("~/Views/Shared/NotFoundFailed.cshtml"); }
            return View(model);
        }

        [HttpPost]
        [AuthLog]
        public async Task<ActionResult> DeletePollAnswer(int PollAnswerId, int PollQuestionID, string Token)
        {
            PollAnswerManagement post = new PollAnswerManagement();
            string result =await post.DeletePollAnswer(PollAnswerId, PollQuestionID, Token);
            if (result == "NotFound") { return View("~/Views/Shared/NotFoundFailed.cshtml"); }
            else if (result == "MinCount")
            {
                TempData["Notification"] = "حداقل باید دو گزینه برای نظر سنجی وجود داشته باشد.";
            }
            TempData["Notification"] = "success";
            return RedirectToAction("DetailPollQuestion", "Poll", new { PollQuestionId = PollQuestionID });
        }
    }
}