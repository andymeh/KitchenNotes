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

        [Route("HubName")]
        public string HubName(string username)
        {
            if (username != "")
            {
                List<string> HubIdReferences = new List<string>();
                User user = KitchenNotesUser.getUser(username);
                
                Hub uHub = KitchenNotesHub.getHub(user.CurrentHub);


                return uHub.HubName;
            }
            else
            {
                return "";
            }
        }
        [Route("HubIdRefs")]
        public string HubIdRefs(string username)
        {
            string HubIdReference = "";
            if (username != "")
            {
                
                User user = KitchenNotesUser.getUser(username);
                HubIdReference = user.CurrentHub.ToString().Substring(0, 8);

            }
            return HubIdReference;
        }




    }
}
