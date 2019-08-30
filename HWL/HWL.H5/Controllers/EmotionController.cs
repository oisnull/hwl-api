using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HWL.H5.Controllers
{
    public class EmotionController : Controller
    {
        // GET: Emotion
        public ActionResult Index()
        {
            return View();
        }
    }
}