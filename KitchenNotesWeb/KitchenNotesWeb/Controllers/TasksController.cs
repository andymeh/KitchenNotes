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
            model.taskList = getHubTasks();
            return View(model);
        }

        [Authorize]
        public ActionResult ViewHubTasks()
        {
            var model = getHubTasks();
            return View(model);
        }

        [Authorize]
        public ActionResult EditTask(Guid taskId)
        {
            var model = new EditTaskModel();
            model.userList = GetUsersInHub();
            return View(model);
        }

        [Authorize]
        public ActionResult DeleteTask(Guid? TaskId)
        {
            User user = KitchenNotesUser.getUser(User.Identity.Name);
            UserHub uHub = KitchenNotesUserHub.getCurrentUserHub(user.CurrentHub, user.UserId);
            Guid _taskId = TaskId.GetValueOrDefault();
            if (_taskId != null || _taskId != Guid.Empty)
            {
                Tasks task = KitchenNotesTasks.getTask(_taskId);
                if (task.UserHubId == uHub.UserHubId || KitchenNotesUser.isUserAdmin(uHub.UserHubId))
                {
                    KitchenNotesTasks.deleteTask(_taskId);
                }
            }
            return Redirect(Request.UrlReferrer.ToString());
        }

        private List<DetailedTaskModel> getHubTasks()
        {
            User user = KitchenNotesUser.getUser(User.Identity.Name);
            List<DetailedTaskModel> detailedTaskList = new List<DetailedTaskModel>();
            List<Tasks> taskList = KitchenNotesTasks.getAllHubTasks(user.CurrentHub);
            foreach(var t in taskList)
            {
                User postUser = KitchenNotesUser.getUserFromUserHubId(t.UserHubId);
                detailedTaskList.Add(new DetailedTaskModel
                {
                    
                    assignedTo = t.AssignedTo,
                    taskDetail = t.TaskDetail,
                    username = postUser.Username,
                    forename = postUser.Forename,
                    task = t,
                    timeAgo = TimeAgo(t.DatePosted)

                });
            }
            return detailedTaskList;
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

        [HttpPost]
        public ActionResult EditTask(EditTaskModel editedTask)
        {
            if (ModelState.IsValid)
            {
                Tasks task = KitchenNotesTasks.getTask(editedTask.taskId);
                task.TaskDetail = editedTask.taskDetail;
                task.AssignedTo = editedTask.assignedTo;
                KitchenNotesTasks.editTask(task);
            }
            editedTask.userList = GetUsersInHub();
            return View(editedTask);
        }
        public static string TimeAgo(DateTime dt)
        {
            TimeSpan span = DateTime.Now - dt;
            if (span.Days > 365)
            {
                int years = (span.Days / 365);
                if (span.Days % 365 != 0)
                    years += 1;
                return String.Format("about {0} {1} ago",
                years, years == 1 ? "year" : "years");
            }
            if (span.Days > 30)
            {
                int months = (span.Days / 30);
                if (span.Days % 31 != 0)
                    months += 1;
                return String.Format("about {0} {1} ago",
                months, months == 1 ? "month" : "months");
            }
            if (span.Days > 0)
                return String.Format("about {0} {1} ago",
                span.Days, span.Days == 1 ? "day" : "days");
            if (span.Hours > 0)
                return String.Format("about {0} {1} ago",
                span.Hours, span.Hours == 1 ? "hour" : "hours");
            if (span.Minutes > 0)
                return String.Format("about {0} {1} ago",
                span.Minutes, span.Minutes == 1 ? "minute" : "minutes");
            if (span.Seconds > 5)
                return String.Format("about {0} seconds ago", span.Seconds);
            if (span.Seconds <= 5)
                return "just now";
            return string.Empty;
        }
    }


}