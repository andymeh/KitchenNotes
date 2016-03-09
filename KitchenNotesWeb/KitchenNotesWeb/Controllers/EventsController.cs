using KitchenNotesWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using KitchenNotesBLL;
using KitchenNotesDAL;
using Newtonsoft.Json;

namespace KitchenNotesWeb.Controllers
{
    public class EventsController : Controller
    {
        // GET: Events
        [Authorize]
        public ActionResult Index()
        {
            //var model = GetEvents();
            return View();
        }

        [Authorize]
        public ActionResult NewEvent()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [Route("NewEvent")]
        public ActionResult NewEvent(NewEventModel newEvent)
        {
            if (ModelState.IsValid)
            {
                User user = KitchenNotesUser.getUser(User.Identity.Name);
                UserHub uHub = KitchenNotesUserHub.getCurrentUserHub(user.CurrentHub, user.UserId);
                HubEvent Event = new HubEvent { HubEventId = Guid.NewGuid(), UserHubId = uHub.UserHubId, Name = newEvent.title, Description = newEvent.Description, StartDate = newEvent.StartDate, EndDate = newEvent.EndDate };
                KitchenNotesEvents.addEvent(Event);
                return RedirectToAction("Index", "Events");
            }
            return View(newEvent);
        }

        [Authorize]
        [HttpGet]
        [Route("GetEvents")]
        public IEnumerable<CalendarEventModel> GetEvents()
        {
            User user = KitchenNotesUser.getUser(User.Identity.Name);
            List<HubEvent> lstEvents = KitchenNotesEvents.getAllEvents(user.CurrentHub);
            List<CalendarEventModel> lstCalendarEvents = new List<CalendarEventModel>();
            foreach(HubEvent he in lstEvents)
            {
                lstCalendarEvents.Add(new CalendarEventModel
                {
                    id = he.HubEventId.ToString().Substring(0, 8),
                    title = he.StartDate.ToString("HH.mm") + " - " + he.EndDate.ToString("HH.mm") + " : " + he.Name + he.Description,
                    start = Convert.ToInt64(he.StartDate.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds),
                    end = Convert.ToInt64(he.EndDate.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds)
                });
            }
            return lstCalendarEvents;
        }

        [Authorize]
        [HttpGet]
        [Route("GetEventsWithinDate")]
        public ContentResult GetEventsWithinDate(long? from, long? to)
        {
            User user = KitchenNotesUser.getUser(User.Identity.Name);
            List<HubEvent> lstEvents = new List<HubEvent>();
            if (from != null && to != null)
            {
                DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                DateTime fromDate = start.AddMilliseconds((double)from).ToLocalTime();
                DateTime toDate = start.AddMilliseconds((double)to).ToLocalTime();


                lstEvents.AddRange(KitchenNotesEvents.getEventsWithinDate(user.CurrentHub, fromDate, toDate));
            }
            else
            {
                lstEvents.AddRange(KitchenNotesEvents.getAllEvents(user.CurrentHub));
            }
            
            List<CalendarEventModel> lstCalendarEvents = new List<CalendarEventModel>();
            foreach (HubEvent he in lstEvents)
            {
                lstCalendarEvents.Add(new CalendarEventModel
                {
                    id = he.HubEventId.ToString().Substring(0, 8),
                    title = he.StartDate.ToString("HH.mm") + " - " + he.EndDate.ToString("HH.mm") + " : " + he.Name + he.Description,
                    Class = "event-important",
                    start = ConvertToTimestamp(he.StartDate),
                    end = ConvertToTimestamp(he.EndDate)
                });
            }
            var json = JsonConvert.SerializeObject(lstCalendarEvents, Formatting.Indented);
            return Content(@"{""success"": 1, ""result"": "+json+"}", "");
        }

        private long ConvertToTimestamp(DateTime value)
        {
            //create Timespan by subtracting the value provided from
            //the Unix Epoch
            TimeSpan span = (value - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());

            //return the total seconds (which is a UNIX timestamp)
            var milliSeconds = (long)(span.TotalSeconds * 1000);
            return milliSeconds;
        }
    }
    
}