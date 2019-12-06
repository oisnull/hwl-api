using System;
using System.Collections.Generic;
using HWL.Entity.Models;
using HWL.Manage.Service;
using HWL.PushStandard;
using HWL.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HWL.Manage.Controllers
{
    public class PushController : BaseController
    {
        private HWLEntities dbContext;
        private PosService posService;

        public PushController(HWLEntities dbContext)
        {
            this.dbContext = dbContext;
            this.posService = new PosService(dbContext);
        }
        public IActionResult Entrance()
        {
            List<SelectListItem> items = new List<SelectListItem>()
            {
                new SelectListItem(){ Text = "Select Active position", Value = "", Selected = true }
            };

            List<SelectListItem> geoItems = posService.GetActiveGeoInfos(0)?.ConvertAll(c => new SelectListItem()
            {
                Value = c.ToString(),
                Text = c.Details,
                Selected = false
            });

            if (geoItems != null && geoItems.Count > 0)
                items.AddRange(geoItems);

            ViewBag.GeoInfos = items;

            string randomText = RandomText.GetString(6);
            PushModel defaultPushModel = new PushModel()
            {
                PushMessageType = PushMessageType.NearCirlce,
                SourceType = SourceType.TestCreate,
                PositionType = PushPositionType.Near,
                MessageModel = new PushMessageModel()
                {
                    Content = $"content-{randomText}",
                    //ImageUrls = null,
                }
            };
            return View(defaultPushModel);
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

            if (model.PositionModel == null || string.IsNullOrEmpty(model.PositionModel.PosDetails))
                return Json(new { state = -1, error = "Publish position can't be empty." });

            if (model.PositionModel.UserId <= 0)
                return Json(new { state = -1, error = "UserId can't be empty." });

            string[] posDetails = model.PositionModel.PosDetails?.Split(',');
            if (posDetails != null && posDetails.Length > 0)
                model.PositionModel.PosDetails = posDetails[posDetails.Length - 1];

            if (model.MessageModel?.ImageUrls != null)
                model.MessageModel.ImageUrls.RemoveAll(r => string.IsNullOrEmpty(r));

            try
            {
                PushService.PushEntry.Process(model);
            }
            catch (Exception ex)
            {
                return Json(new { state = -1, error = ex.Message });
            }
            return Json(new { state = 1 });
        }
    }
}