using KitchenNotesDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using KitchenNotesBLL;
using Helpers;
using KitchenNotesWeb.Models;

namespace KitchenNotesWeb.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult NewUser()
        {
            return View();
        }

        public ActionResult UserInfo()
        {
            return View();
        }

        public ActionResult JoinHub()
        {
            return View();
        }
        public ActionResult NewHub()
        {
            return View();
        }
        [Authorize]
        public ActionResult AccountInfo(string username)
        {
            User user = KitchenNotesUser.getUser(username);
            var model = new UserDetails()
            {
                username = username,
                Forename = user.Forename,
                Surname = user.Surname,
                DOB = user.DOB,
                UserEmail = user.Email,
                LastLogin = user.LastLogin
            };
            return View(model);
        }

        [Authorize]
        public ActionResult ManageAccount(string username)
        {
            
            User user = KitchenNotesUser.getUser(username);
            var model = new UserDetails()
            {
                username = username,
                Forename = user.Forename,
                Surname = user.Surname,
                DOB = user.DOB,
                UserEmail = user.Email
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(UserLogin user)
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

        
        public ActionResult Logout()
        {
            KitchenNotesUser.updateLastLogin(User.Identity.Name);
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Register(Models.UserHubViewModel userReg)
        {
            if (ModelState.IsValid)
            {
                if (userReg.Password == userReg.Password2)
                {
                    if (!KitchenNotesUser.UserNameExists(userReg.UserName))
                    {
                        Guid HubId = KitchenNotesHub.addHubReturnId(userReg.HubName);
                        User newUser = new User()
                        {
                            Username = userReg.UserName,
                            Forename = userReg.Forename,
                            Surname = userReg.Surname,
                            Email = userReg.UserEmail,
                            DOB = userReg.DOB,
                            Password = SHA1.Encode(userReg.Password),
                            CurrentHub = HubId,
                            LastLogin = DateTime.Now
                        };

                        KitchenNotesUser.addNewUserToExistingHub(newUser, HubId);
                        FormsAuthentication.SetAuthCookie(newUser.Username, true);
                        KitchenNotesUser.addUserAsAdmin(newUser.Username);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Username already exists");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Passwords did not match");
                }
            }
            return View(userReg);
        }

        [HttpPost]
        public ActionResult NewUser(Models.NewUserViewModel nUser)
        {
            if (ModelState.IsValid)
            {
                if (nUser.Password == nUser.Password2)
                {
                    Hub oHub = KitchenNotesHub.getHub(nUser.HubIdStart);
                    if (oHub != null)
                    {
                        Guid HubId = oHub.HubId;
                        User newUser = new User()
                        {
                            Username = nUser.UserName,
                            Forename = nUser.Forename,
                            Surname = nUser.Surname,
                            Email = nUser.UserEmail,
                            DOB = nUser.DOB,
                            Password = SHA1.Encode(nUser.Password),
                            CurrentHub = HubId,
                            LastLogin = DateTime.UtcNow
                        };

                        if (!KitchenNotesUser.UserNameExists(nUser.UserName))
                        {
                            KitchenNotesUser.addNewUserToExistingHub(newUser, HubId);
                            FormsAuthentication.SetAuthCookie(nUser.UserName, true);
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Username already exists");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Could not find Hub");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Passwords did not match");
                }
            }
            return View(nUser);
        }

        [Authorize]
        [HttpPost]
        public ActionResult JoinHub(string HubReference)
        {
            if (ModelState.IsValid)
            {
                Hub hub = KitchenNotesHub.getHub(HubReference);
                if (hub != null)
                {
                    User user = KitchenNotesUser.getUser(User.Identity.Name);
                    KitchenNotesUser.addUserToHub(user.UserId, hub.HubId);
                    KitchenNotesUser.ChangeCurrentHub(user.UserId, hub.HubId);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("0", "Hub not found!");
                }
            }
            return View(HubReference);
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

    }
}