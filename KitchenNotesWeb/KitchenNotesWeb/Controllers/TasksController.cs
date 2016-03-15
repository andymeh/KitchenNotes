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
            KitchenNotesUser.updateLastLogin(User.Identity.Name);
            var model = new Models.TasksIndexModel();
            model.taskModel = new Models.NewTaskModel();
            model.taskModel.userList = GetUsersInHub();
            return View(model);
        }

        private SelectList GetUsersInHub()
        {
            User user = KitchenNotesUser.getUser(User.Identity.Name);
            List<UserHub> allUserHub = KitchenNotesUserHub.getAllUserHubsInHub(user.CurrentHub);
            List<User> allUsersInHub = new List<User>();
            foreach (UserHub uh in allUserHub)
            {
                allUsersInHub.Add(KitchenNotesUser.getUser(uh.UserId));
            }
            return new SelectList(allUsersInHub, "Username", "Forename");
        }

        [Authorize]
        public ActionResult NewTask()
        {
            var model = new NewTaskModel();

            model.userList = GetUsersInHub();
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult NewTask(NewTaskModel newTask)
        {
            if (ModelState.IsValid)
            {
                User user = KitchenNotesUser.getUser(User.Identity.Name);
                UserHub uHub = KitchenNotesUserHub.getCurrentUserHub(user.CurrentHub, user.UserId);
                KitchenNotesTasks.addTask(new Tasks
                {
                    TaskId = Guid.NewGuid(),
                    TaskDetail = newTask.taskDetail,
                    AssignedTo = KitchenNotesUser.getUser(newTask.assignedTo).Username,
                    UserHubId = uHub.UserHubId,
                    DatePosted = DateTime.UtcNow
                });
                return RedirectToAction("Index", "Tasks");
            }
            return View(newTask);
        }

        [Authorize]
        public ActionResult ViewTasks()
        {
            return View();
        }
    }
}