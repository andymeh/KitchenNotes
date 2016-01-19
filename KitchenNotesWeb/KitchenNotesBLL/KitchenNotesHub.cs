using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KitchenNotesDAL;
using Helpers;

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
            using(var dc = new DALDataContext())
            {
                List<UserHub> lstUserHubs = dc.UserHubs.Where(x => x.UserId == userId).ToList();
                return lstUserHubs;
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
        public static bool UserNameExists(string username) {
            using (var db = new DALDataContext())
            {
                List<User> user = db.Users.Where(x => x.Username == username).ToList();
                
                if (user.Count == 0 )
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
                List<User> user = db.Users.Where(x => x.Username == username).ToList();

                if (user.Count == 0)
                {
                    return user.FirstOrDefault(); ;
                }
                else
                {
                    return user.FirstOrDefault();
                }
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
                foreach(Hub f in oHubList)
                {
                    string HubIdString = f.HubId.ToString();
                    if(HubIdString.StartsWith(userHubId))
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
                
                UserHub nUserHub = Entities.createUserHub(oHub.HubId, newUser.UserId);
                dc.Users.InsertOnSubmit(newUser);
                dc.UserHubs.InsertOnSubmit(nUserHub);
                dc.SubmitChanges();

            }
        }

        public static bool isUserValid(String username, String password)
        {
            using (var db = new DALDataContext())
            {
                List<User> user = db.Users.Where(x => x.Username == username).ToList();
                
                if (user != null )
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
