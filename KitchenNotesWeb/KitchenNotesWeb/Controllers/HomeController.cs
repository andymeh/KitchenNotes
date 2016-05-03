using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KitchenNotesBLL;
using KitchenNotesDAL;
using KitchenNotesWeb.Models;
using System.Web.Security;

namespace KitchenNotesWeb.Controllers
{
    [RoutePrefix("Home")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            User user = KitchenNotesUser.getUser(User.Identity.Name);
            var model = new HomeUserModel();
            model.userlogin = new UserLogin();
            model.currentHub = user.CurrentHub;

            List<UserHub> AllUsersHubs = KitchenNotesHub.lstUserHubs(user.UserId);
            List<Hub> AllHubDetails = KitchenNotesHub.lstHubDetails(AllUsersHubs);

            model.userHubs = new List<UserHubDetailModel>();
            foreach(var hd in AllHubDetails)
            {
                model.userHubs.Add(new UserHubDetailModel
                {
                    HubId = hd.HubId,
                    hubName = hd.HubName,
                    usersInHub = ConvertUserToUserDetails(KitchenNotesHub.getUsersInHub(hd.HubId))
                });
            }
            return View(model);
        }

        public ActionResult ModalLogin()
        {
            return View();
        }

        [Route("ChangeCurrentHub")]
        public ActionResult ChangeCurrentHub(string hubGuid)
        {
            User user = KitchenNotesUser.getUser(User.Identity.Name);

            KitchenNotesUser.ChangeCurrentHub(user.UserId, new Guid(hubGuid));

            return RedirectToAction("Index", "Home");
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

        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Retrieves the number of new notes since the user had last logged in
        /// </summary>
        /// <returns>Int of new notes</returns>
        [Authorize]
        [Route("numNewNotes")]
        public int NumNewNotes()
        {
            int numNotes = 0;

            User user = KitchenNotesUser.getUser(User.Identity.Name);
            List<Notes> allNotes = NotesController.getHubNotes(user.Username);
            List<Notes> newNotes = allNotes.Where(x => x.DateAdded > user.LastLogin).ToList();
            if(newNotes != null)
            {
                numNotes = newNotes.Count();
            }
            
            return numNotes;
        }
        [Authorize]
        [Route("numNewTasks")]
        public int NumNewTasks()
        {
            int numTasks = 0;

            User user = KitchenNotesUser.getUser(User.Identity.Name);
            UserHub uHub = KitchenNotesUserHub.getCurrentUserHub(user.CurrentHub, user.UserId);
            List<Tasks> allTasks = KitchenNotesTasks.getAllUserHubTasks(uHub.UserHubId);
            List< Tasks > newTasks = allTasks.Where(x => x.DatePosted > user.LastLogin).ToList();
            if (newTasks != null)
            {
                numTasks = newTasks.Count();
            }

            return numTasks;
        }
        [Authorize]
        [Route("numNewEvents")]
        public int NumNewEvents()
        {
            int numEvents = 0;

            User user = KitchenNotesUser.getUser(User.Identity.Name);
            List<HubEvent> allEvents = KitchenNotesEvents.getAllEvents(user.CurrentHub);
            List<HubEvent> newEvents = allEvents.Where(x => x.DateAdded > user.LastLogin).ToList();
            if (newEvents != null)
            {
                numEvents = newEvents.Count();
            }

            return numEvents;
        }

        public List<UserDetails> ConvertUserToUserDetails(List<User> userList)
        {
            List<UserDetails> uDetails = new List<UserDetails>();
            foreach (var u in userList)
            {
                uDetails.Add(new UserDetails
                {
                    username = u.Username,
                    Forename = u.Forename,
                    Surname = u.Surname,
                    DOB = u.DOB,
                    UserEmail = u.Email,
                    Password = u.Password,
                    LastLogin = u.LastLogin
                });
            }
            return uDetails;
        }

        [HttpPost]
        public ActionResult ModalLogin(UserLogin user)
        {
            if (ModelState.IsValid)
            {
                if (KitchenNotesUser.isUserValid(user.username, user.password))
                {
                    FormsAuthentication.SetAuthCookie(user.username, user.rememberMe);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect!");
                }
            }
            return View(user);
        }

    }
}
