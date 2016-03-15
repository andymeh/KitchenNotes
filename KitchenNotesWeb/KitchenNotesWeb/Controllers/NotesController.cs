using KitchenNotesBLL;
using KitchenNotesDAL;
using KitchenNotesWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KitchenNotesWeb.Controllers
{
    public class NotesController : Controller
    {
        // GET: Notes
        [Authorize]
        public ActionResult Index()
        {
            //KitchenNotesUser.updateLastLogin(User.Identity.Name);
            var model = new Models.NotesIndexModel();
            model.noteModel = new Models.NewNotesModel();
            model.noteList = getDetailedHubNotes(User.Identity.Name);
            model.noteList.OrderByDescending(x => x.note.DateAdded);
            return View(model);
        }

        [Authorize]
        public ActionResult AddNote()
        {
            return View();
        }
        [Authorize]
        public ActionResult NewNote()
        {
            return View();
        }
        [Authorize]
        public ActionResult ViewHubNotes()
        {
            var model = getDetailedHubNotes(User.Identity.Name);
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddNote(string username, string note)
        {
            if (username != "")
            {
                User user = KitchenNotesUser.getUser(username);

                List<UserHub> lstUserHubs = KitchenNotesHub.lstUserHubs(user.UserId);

                KitchenNotesNotes.addNewNote(lstUserHubs.First().UserHubId, note);
                return View(username, note);
            }
            else
            {
                return View(username, note);
            }
        }

        [Authorize]
        [Route("GetHubNotes")]
        public static List<Notes> getHubNotes(string username)
        {
            List<Notes> notes = new List<Notes>();

            if (username != "")
            {
                User user = KitchenNotesUser.getUser(username);
                notes = KitchenNotesNotes.getAllHubNotes(user.CurrentHub);
            }
            return notes;
        }

        [Authorize]
        [Route("GetDetailedHubNotes")]
        public List<DetailedNoteModel> getDetailedHubNotes(string username)
        {
            List<Notes> notes = new List<Notes>();
            List<DetailedNoteModel> detailNoteList = new List<DetailedNoteModel>();
            if (username != "")
            {
                User user = KitchenNotesUser.getUser(username);
                notes = KitchenNotesNotes.getAllHubNotes(user.CurrentHub);
                foreach(Notes n in notes)
                {
                    User noteUser = KitchenNotesUser.getUserFromUserHubId(n.UserHubId);
                    detailNoteList.Add(new DetailedNoteModel
                    {
                        forename = noteUser.Forename,
                        username = noteUser.Username,
                        timeAgo = TimeAgo(n.DateAdded),
                        note = n
                    });
                }
                detailNoteList.Sort((a, b) => b.note.DateAdded.CompareTo(a.note.DateAdded));
            }
            return detailNoteList;
        }
        [Authorize]
        [HttpPost]
        public ActionResult NewNote(Models.NewNotesModel note)
        {
            if (ModelState.IsValid)
            {
                if (User.Identity.Name != "")
                {
                    User user = KitchenNotesUser.getUser(User.Identity.Name);

                    UserHub uHub = KitchenNotesUserHub.getCurrentUserHub(user.CurrentHub, user.UserId);

                    KitchenNotesNotes.addNewNote(uHub.UserHubId, note.noteContent);
                    return RedirectToAction("Index", "Notes");
                }
                else
                {
                    return RedirectToAction("Index", "Notes");
                }
            }
            return RedirectToAction("Index", "Notes");
        }

        [Authorize]
        [Route("DeleteNote")]
        [HttpGet]
        public ActionResult DeleteNote(Guid? NoteId)
        {
            if(NoteId != null)
            {
                KitchenNotesNotes.deleteNote((Guid) NoteId);
            }
            return Redirect(Request.UrlReferrer.ToString());
        }

        [Authorize]
        [Route("IsUserHubAdmin")]
        [HttpGet]
        public bool IsUserAdmin()
        {
            User user = KitchenNotesUser.getUser(User.Identity.Name);
            UserHub uHub = KitchenNotesUserHub.getCurrentUserHub(user.CurrentHub, user.UserId);
            bool isAdmin = KitchenNotesUser.isUserAdmin(uHub.UserHubId);

            return isAdmin;
        }

        [Authorize]
        [Route("EditNote")]
        [HttpGet]
        public ActionResult EditNote(NewNotesModel editedNote)
        {
            if (ModelState.IsValid)
            {

            }

            return Redirect(Request.UrlReferrer.ToString());
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