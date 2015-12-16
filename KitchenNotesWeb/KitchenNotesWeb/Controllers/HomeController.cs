using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KitchenNotesBLL;
using KitchenNotesDAL;


namespace KitchenNotesWeb.Controllers
{
    [RoutePrefix("Home")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        [Route("AddHub")]
        public string HubNameDos()
        {
            Hub hubName = KitchenNotesHub.getHub("Andy");

            return hubName.HubName;
        }


    }
}
