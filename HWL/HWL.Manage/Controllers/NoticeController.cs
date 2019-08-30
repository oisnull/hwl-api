using Microsoft.AspNetCore.Mvc;
using System;

namespace HWL.Manage.Controllers
{
    public class NoticeController : BaseController
    {
        public ActionResult List()
        {
           // ViewBag.NoticeList = noticeService.GeNoticeList();
            return View();
        }
        public ActionResult Edit(int? id)
        {
            //NoticeManageModel model = noticeService.GetNoticeInfo(id ?? 0);
            //if (model == null)
            //{
            //    model = new NoticeManageModel();
            //    model.CreateTime = DateTime.Now;
            //}

            //ViewBag.NoticeTypeList = CommonHandler.GetNoticeTypeList();

            //return View(model);
            return Content("暂未实现");
        }

        //[ValidateInput(false)]
        //[HttpPost]
        //public ActionResult Action(NoticeManageModel model)
        //{
        //    string error;

        //    int result = noticeService.NoticeAction(model, out error);
        //    if (result > 0)
        //    {
        //        return Json(new { state = 1 });
        //    }
        //    else
        //    {
        //        return Json(new { state = -1, error = error });
        //    }
        //}

    }
}
