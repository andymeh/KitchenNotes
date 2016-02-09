using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KitchenNotesWeb.Controllers
{
    public class EventsController : Controller
    {
        // GET: Events
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}