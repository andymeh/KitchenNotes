using KitchenNotesBLL;
using KitchenNotesDAL;
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
            var model = new Models.NotesIndexModel();
            model.noteModel = new Models.NotesModel();  
            model.noteList = getHubNotes(User.Identity.Name);
            return View(model);
        }

        [Authorize]
        public ActionResult AddNote()
        {
            return View();
        }

        public ActionResult NewNote()
        {
            return View();
        }

        public ActionResult ViewHubNotes()
        {
            var model = getHubNotes(User.Identity.Name);
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
        public List<Notes> getHubNotes(string username)
        {
            List<Notes> notes = new List<Notes>();

            if (username != "")
            {
                User user = KitchenNotesUser.getUser(username);

                List<UserHub> lstUserHubs = KitchenNotesHub.lstUserHubs(user.UserId);

                notes = KitchenNotesNotes.getAllHubNotes(lstUserHubs.First().HubId);

            }
            return notes;
        }
        [Authorize]
        [HttpPost]
        public ActionResult NewNote(Models.NotesModel note)
        {
            if (ModelState.IsValid)
            {
                if (User.Identity.Name != "")
                {
                    User user = KitchenNotesUser.getUser(User.Identity.Name);

                    List<UserHub> lstUserHubs = KitchenNotesHub.lstUserHubs(user.UserId);

                    KitchenNotesNotes.addNewNote(lstUserHubs.First().UserHubId, note.noteContent);
                    return RedirectToAction("Index", "Notes");
                }
                else
                {
                    return RedirectToAction("Index", "Notes");
                }
            }
            return RedirectToAction("Index", "Notes");
        }


    }
}