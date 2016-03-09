using KitchenNotesWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KitchenNotesBLL;
using KitchenNotesDAL;

namespace KitchenNotesWeb.Controllers
{
    public class TasksController : Controller
    {
        // GET: Tasks
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewTask()
        {
            var model = new NewTaskModel();
            User user = KitchenNotesUser.getUser(User.Identity.Name);
            List<UserHub> allUserHub = KitchenNotesUserHub.getAllUserHubsInHub(user.CurrentHub);
            List<User> allUsersInHub = new List<User>();
            foreach(UserHub uh in allUserHub)
            {
                allUsersInHub.Add(KitchenNotesUser.getUser(uh.UserId));
            }
            model.hubUsers = allUsersInHub;
            return View(model);
        }
    }
}