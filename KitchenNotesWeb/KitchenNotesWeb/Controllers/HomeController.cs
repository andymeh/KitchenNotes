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

                List<UserHub> lstUserHubs = KitchenNotesHub.lstUserHubs(user.UserId);
                Hub uHub = KitchenNotesHub.getHub(lstUserHubs.First().HubId);


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
            if(username != "")
            {
                List<string> HubIdReferences = new List<string>();
                User user = KitchenNotesUser.getUser(username);

                List<UserHub> lstUserHubs = KitchenNotesHub.lstUserHubs(user.UserId);

                foreach (UserHub uh in lstUserHubs)
                {
                    HubIdReferences.Add(uh.HubId.ToString().Substring(0, 8));
                }
                return HubIdReferences.First(); ;
            }
            else
            {
                return "";
            }
        }




    }
}
