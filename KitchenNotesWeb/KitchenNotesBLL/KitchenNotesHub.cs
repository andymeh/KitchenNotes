using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KitchenNotesDAL;
using Helpers;
using System.Data.SqlClient;

namespace KitchenNotesBLL
{
    public class KitchenNotesHub
    {
        public static void addHub(string newHubName)
        {
            Hub newHub = Entities.createHub(newHubName);
            using (var dc = new DALDataContext())
            {
                dc.Hubs.InsertOnSubmit(newHub);
                dc.SubmitChanges();
                Guid id = newHub.HubId;
            }
        }

        public static List<UserHub> lstUserHubs(Guid userId)
        {
            using (var dc = new DALDataContext())
            {
                List<UserHub> lstUserHubs = dc.UserHubs.Where(x => x.UserId == userId).ToList();
                return lstUserHubs;
            }
        }

        public static List<Hub> lstHubDetails(List<UserHub> userHubs)
        {
            using (var db = new DALDataContext())
            {
                List<Hub> lstHubDetails = new List<Hub>();
                foreach (var uh in userHubs)
                {
                    lstHubDetails.Add(db.Hubs.First(x => x.HubId == uh.HubId));
                }
                return lstHubDetails;
            }
        }

        public static Guid addHubReturnId(string newHubName)
        {
            Hub newHub = Entities.createHub(newHubName);
            using (var dc = new DALDataContext())
            {
                dc.Hubs.InsertOnSubmit(newHub);
                dc.SubmitChanges();
                Guid id = newHub.HubId;
                return id;
            }
        }

        public static List<User> getUsersInHub(Guid HubId)
        {
            using (var db = new DALDataContext())
            {
                List<UserHub> allUserHubs = db.UserHubs.Where(x => x.HubId == HubId).ToList();
                List<User> allUsersInHub = new List<User>();
                foreach (var u in allUserHubs)
                {
                    allUsersInHub.Add(db.Users.First(x => x.UserId == u.UserId));
                }
                return allUsersInHub;
            }
        }

        public static Hub getHub(string startHubid)
        {
            Hub oHub = new Hub();
            using (var dc = new DALDataContext())
            {
                oHub = dc.Hubs.FirstOrDefault(x => x.HubId.ToString().StartsWith(startHubid));
            }
            return oHub;
        }

        public static Hub getHub(Guid Hubid)
        {
            Hub oHub = new Hub();
            using (var dc = new DALDataContext())
            {
                oHub = dc.Hubs.FirstOrDefault(x => x.HubId == Hubid);
            }
            return oHub;
        }

    }

    public class KitchenNotesUser
    {
        public static bool UserNameExists(string username)
        {
            using (var db = new DALDataContext())
            {
                List<User> user = db.Users.Where(x => x.Username == username).ToList();

                if (user.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public static User getUser(string username)
        {
            using (var db = new DALDataContext())
            {
                User user = new User();
                if (username != null && username != "")
                {
                    user = db.Users.First(x => x.Username == username);
                }
                return user;
            }
        }

        public static void ChangeCurrentHub(Guid UserId, Guid HubId)
        {
            using (var db = new DALDataContext())
            {
                User user = db.Users.First(x => x.UserId == UserId);
                user.CurrentHub = HubId;
                db.SubmitChanges();
            }
        }
        public static User getUser(Guid userId)
        {
            using (var db = new DALDataContext())
            {
                User user = db.Users.First(x => x.UserId == userId);

                return user;
            }
        }

        public static void updateLastLogin(string username)
        {
            using (var db = new DALDataContext())
            {
                User user = db.Users.First(x => x.Username == username);
                user.LastLogin = DateTime.UtcNow;
                db.SubmitChanges();
            }

        }

        public static User getUserFromUserHubId(Guid userHubId)
        {
            using (var db = new DALDataContext())
            {
                User _user = new User();
                UserHub _userHub = db.UserHubs.First(x => x.UserHubId == userHubId);
                if (_userHub != null)
                {
                    _user = db.Users.First(x => x.UserId == _userHub.UserId);
                }
                return _user;
            }

        }

        public static void addUserAsAdmin(string username)
        {
            using (var db = new DALDataContext())
            {
                User user = getUser(username);
                UserHub uHub = db.UserHubs.First(x => x.HubId == user.CurrentHub);
                uHub.HubAdmin = true;
                db.SubmitChanges();
            }

        }
        public static void addUserAsAdmin(string username, Guid _hubId)
        {
            using (var db = new DALDataContext())
            {
                User user = getUser(username);
                UserHub uHub = db.UserHubs.First(x => x.HubId == _hubId);
                uHub.HubAdmin = true;
                db.SubmitChanges();
            }
        }

        public static bool isUserAdmin(Guid userHubId)
        {
            using (var db = new DALDataContext())
            {
                UserHub uHub = db.UserHubs.First(x => x.UserHubId == userHubId);
                bool isAdmin = uHub.HubAdmin;
                return isAdmin;
            }
        }


        public static void addNewUserToExistingHub(string newUsername, string oldHubName, string password, string newUserForename, string userHubId)
        {
            Hub oHub = new Hub();
            User nUser = Entities.createUser(newUsername, newUserForename, password);
            List<Hub> oHubList = new List<Hub>();
            using (var dc = new DALDataContext())
            {
                oHubList = dc.Hubs.Where(x => x.HubName == oldHubName).ToList();
                foreach (Hub f in oHubList)
                {
                    string HubIdString = f.HubId.ToString();
                    if (HubIdString.StartsWith(userHubId))
                    {
                        oHub = f;
                    }
                }
                UserHub nUserHub = Entities.createUserHub(oHub.HubId, nUser.UserId);
                dc.Users.InsertOnSubmit(nUser);
                dc.UserHubs.InsertOnSubmit(nUserHub);
                dc.SubmitChanges();
            }
        }

        public static void addNewUserToExistingHub(User newUser, Guid hubId)
        {
            Hub oHub = new Hub();
            newUser.UserId = Guid.NewGuid();
            using (var dc = new DALDataContext())
            {
                oHub = dc.Hubs.FirstOrDefault(x => x.HubId == hubId);
                if(oHub != null)
                {
                    UserHub nUserHub = Entities.createUserHub(oHub.HubId, newUser.UserId);
                    dc.Users.InsertOnSubmit(newUser);
                    dc.UserHubs.InsertOnSubmit(nUserHub);
                    dc.SubmitChanges();
                }
            }
        }

        public static void addUserToHub(Guid userId, Guid hubId)
        {
            using (var dc = new DALDataContext())
            {
                UserHub uHub = new UserHub { UserHubId = Guid.NewGuid(), UserId = userId, HubId = hubId };
                dc.UserHubs.InsertOnSubmit(uHub);
                dc.SubmitChanges();
            }
        }

        public static void deleteUserFromHub(Guid userId, Guid HubId)
        {
            using (var dc = new DALDataContext())
            {
                UserHub uHub = dc.UserHubs.First(x => x.UserId == userId && x.HubId == HubId);
                
                dc.UserHubs.DeleteOnSubmit(uHub);
                dc.SubmitChanges();
            }
        }

        public static void removeUserFromHub(Guid userId, Guid hubId)
        {
            using (var dc = new DALDataContext())
            {
                UserHub uHub = dc.UserHubs.First(x => x.HubId == hubId && x.UserId == userId);
                dc.UserHubs.DeleteOnSubmit(uHub);
                dc.SubmitChanges();
            }
        }
        public static bool isUserValid(String username, String password)
        {
            using (var db = new DALDataContext())
            {
                try
                {
                    List<User> user = db.Users.Where(x => x.Username == username).ToList();

                    if (user != null && user.Count != 0)
                    {
                        String encodedPw = SHA1.Encode(password);
                        if (user.First().Password == encodedPw)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                catch(SqlException e)
                {

                }
                return false;
            }
        }
    }

    public class KitchenNotesNotes
    {
        public static void addNewNote(Guid _userHubId, string _note)
        {
            using(var db = new DALDataContext())
            {
                Notes newNote = new Notes { NoteId = Guid.NewGuid(), UserHubId = _userHubId, DateAdded = DateTime.Now, Note = _note };
                db.Notes.InsertOnSubmit(newNote);
                db.SubmitChanges();
            }
        }

        public static void deleteNote(Guid noteId)
        {
            using(var db = new DALDataContext())
            {
                db.Notes.DeleteOnSubmit(db.Notes.First(x => x.NoteId == noteId));
                db.SubmitChanges();
            }
        }

        public static void editNote(Notes editedNote)
        {
            using (var db = new DALDataContext())
            {
                Notes oldNote = db.Notes.FirstOrDefault(x => x.NoteId == editedNote.NoteId);
                oldNote.Note = editedNote.Note;
                db.SubmitChanges();
            }
        }

        public static List<Notes> getAllHubNotes(Guid _hubId)
        {
            using (var db = new DALDataContext())
            {
                List<Notes> hubNotes = new List<Notes>();
                List<UserHub> lstUserHub = db.UserHubs.Where(i => i.HubId == _hubId).ToList();
                if(lstUserHub != null && lstUserHub.Count != 0 )
                {
                    List<Guid> lstUserHubIds = new List<Guid>();
                    foreach (UserHub uh in lstUserHub)
                    {
                        lstUserHubIds.Add(uh.UserHubId);
                    }
                    foreach(Guid x in lstUserHubIds)
                    {
                        hubNotes.AddRange(db.Notes.Where(i => i.UserHubId == x));
                    }

                }
                return hubNotes;
            }
            
        }

        public static List<Notes> getAllUserHubNotes(Guid _UserHubId)
        {
            using (var db = new DALDataContext())
            {
                List<Notes> hubNotes = new List<Notes>();
                List<UserHub> lstUserHub = db.UserHubs.Where(i => i.UserHubId == _UserHubId).ToList();
                if (lstUserHub != null && lstUserHub.Count != 0)
                {
                    List<Guid> lstUserHubIds = new List<Guid>();
                    foreach (UserHub uh in lstUserHub)
                    {
                        lstUserHubIds.Add(uh.UserHubId);
                    }
                    foreach (Guid x in lstUserHubIds)
                    {
                        hubNotes.AddRange(db.Notes.Where(i => i.UserHubId == x));
                    }

                }
                return hubNotes;
            }

        }

        public static List<Notes> getAllUserNotes(Guid _UserId)
        {
            using (var db = new DALDataContext())
            {
                List<Notes> hubNotes = new List<Notes>();
                List<UserHub> lstUserHub = db.UserHubs.Where(i => i.UserId == _UserId).ToList();
                if (lstUserHub != null && lstUserHub.Count != 0)
                {
                    List<Guid> lstUserHubIds = new List<Guid>();
                    foreach (UserHub uh in lstUserHub)
                    {
                        lstUserHubIds.Add(uh.UserHubId);
                    }
                    foreach (Guid x in lstUserHubIds)
                    {
                        hubNotes.AddRange(db.Notes.Where(i => i.UserHubId == x));
                    }

                }
                return hubNotes;
            }
        }

        public static Notes getNote(Guid _noteId)
        {
            using (var db = new DALDataContext())
            {
                Notes note = db.Notes.FirstOrDefault(x => x.NoteId == _noteId);
                return note;
            }

        }
    }

    public class KitchenNotesUserHub
    {
        public static UserHub getCurrentUserHub(Guid currentHubId, Guid userId)
        {
            using (var db = new DALDataContext())
            {
                UserHub uHub = db.UserHubs.First(x => x.HubId == currentHubId & x.UserId == userId);
                return uHub;
            }
        }

        public static List<UserHub> getAllUserHubsInHub(Guid HubId)
        {
            using(var db = new DALDataContext())
            {
                List<UserHub> allUserHubs = db.UserHubs.Where(x => x.HubId == HubId).ToList();
                return allUserHubs;
            }
        }
    }

    public class KitchenNotesEvents
    {
        public static List<HubEvent> getAllEvents(Guid hubId)
        {
            using (var db = new DALDataContext())
            {
                List<UserHub> uHub = db.UserHubs.Where(x => x.HubId == hubId).ToList();
                List<HubEvent> lstHubEvents = new List<HubEvent>();
                foreach (UserHub uh in uHub)
                {
                    lstHubEvents.AddRange(db.HubEvents.Where(x => x.UserHubId == uh.UserHubId));
                }
                return lstHubEvents;
            }
        }

        public static List<HubEvent> getEventsWithinDate(Guid hubId, DateTime from, DateTime to)
        {
            using (var db = new DALDataContext())
            {
                List<UserHub> uHub = db.UserHubs.Where(x => x.HubId == hubId).ToList();
                List<HubEvent> lstHubEvents = new List<HubEvent>();
                foreach (UserHub uh in uHub)
                {
                    lstHubEvents.AddRange(db.HubEvents.Where(x => x.UserHubId == uh.UserHubId && x.StartDate >= from && x.EndDate <= to));
                }
                return lstHubEvents;
            }
        }
        
        public static void addEvent(HubEvent newEvent)
        {
            using(var db = new DALDataContext())
            {
                db.HubEvents.InsertOnSubmit(newEvent);
                db.SubmitChanges();
            }
        }

        public static HubEvent getEvent(Guid EventId)
        {
            using (var db = new DALDataContext())
            {
                HubEvent hEvent = new HubEvent();
                hEvent = db.HubEvents.First(x => x.HubEventId == EventId);
                return hEvent;
            }
        }

        public static void deleteEvent(Guid _eventId)
        {
            using(var dc = new DALDataContext())
            {
                HubEvent Event = dc.HubEvents.First(x => x.HubEventId == _eventId);
                dc.HubEvents.DeleteOnSubmit(Event);
                dc.SubmitChanges();
            }
        }
    }

    public class KitchenNotesTasks
    {
        public static void addTask(Tasks newTask)
        {
            using (var dc = new DALDataContext())
            {
                dc.Tasks.InsertOnSubmit(newTask);
                dc.SubmitChanges();
            }
        }

        public static void deleteTask(Guid _taskId)
        {
            using(var dc = new DALDataContext())
            {
                dc.Tasks.DeleteOnSubmit(dc.Tasks.First(x => x.TaskId == _taskId));
                dc.SubmitChanges();
            }
        }

        public static Tasks getTask(Guid _TaskId)
        {
            using(var db = new DALDataContext())
            {
                Tasks task = db.Tasks.FirstOrDefault(x => x.TaskId == _TaskId);
                return task;
            }
        }

        public static List<Tasks> getAllHubTasks(Guid _HubId)
        {
            using(var db = new DALDataContext())
            {
                List<Tasks> hubTasks = new List<Tasks>();
                List<UserHub> lstUserHubs = db.UserHubs.Where(x => x.HubId == _HubId).ToList();
                foreach (var uHub in lstUserHubs)
                {
                    hubTasks.AddRange(db.Tasks.Where(x => x.UserHubId == uHub.UserHubId));
                }
                return hubTasks; 
            }
        }

        public static List<Tasks> getAllUserHubTasks(Guid _UserHubId)
        {
            using (var db = new DALDataContext())
            {
                List<Tasks> hubTasks = new List<Tasks>();
                hubTasks.AddRange(db.Tasks.Where(i => i.UserHubId == _UserHubId));
                return hubTasks;
            }

        }

        public static List<Tasks> getAllUserTasks(string username)
        {
            using (var db = new DALDataContext())
            {
                List<Tasks> alluserTasks = new List<Tasks>();
                alluserTasks.AddRange(db.Tasks.Where(x => x.AssignedTo.Equals(username)));
                return alluserTasks;
            }
        }

        public static void editTask(Tasks newTask)
        {
            using (var db = new DALDataContext())
            {
                Tasks oldTask = db.Tasks.FirstOrDefault(x => x.TaskId == newTask.TaskId);
                oldTask.TaskDetail = newTask.TaskDetail;
                oldTask.AssignedTo = newTask.AssignedTo;
                db.SubmitChanges();
            }
        }
    }
    
    public class Entities
    {
        public static User createUser(string newUsername, string newForename, string newPassword)
        {
            User newUser = new User { UserId = Guid.NewGuid(), Username = newUsername, Forename = newForename, Password = newPassword };
            return newUser;
        }

        public static Hub createHub(string newHubName)
        {
            Hub newHub = new Hub { HubId = Guid.NewGuid(), HubName = newHubName };
            return newHub;
        }

        public static UserHub createUserHub(Guid nHubId, Guid nUserId)
        {
            UserHub newUserHub = new UserHub { UserHubId = Guid.NewGuid(), HubId = nHubId, UserId = nUserId };
            return newUserHub;
        }
    }
}
