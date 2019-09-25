using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HWL.PushStandard;
using Microsoft.AspNetCore.Mvc;

namespace HWL.Manage.Controllers
{
    public class PushController : BaseController
    {
        public IActionResult Entrance()
        {
            return View();
        }

        public IActionResult List()
        {
            return View();
        }

        public IActionResult Publish(PushModel model)
        {
            if (model == null)
                return Json(new { state = -1, error = "Publish data can't be empty." });

            if (model.MessageModel == null)
                return Json(new { state = -1, error = "Publish message content can't be empty." });

            if (model.PositionModel == null)
                return Json(new { state = -1, error = "Publish position can't be empty." });

            try
            {
                PushFunction.PushEntry.Process(model);
            }
            catch (Exception ex)
            {
                return Json(new { state = -1, error = ex.Message });
            }
            return Json(new { state = 1 });
        }
    }
}