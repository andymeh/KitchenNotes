﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}