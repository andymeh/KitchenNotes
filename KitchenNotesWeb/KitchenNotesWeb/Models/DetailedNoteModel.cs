﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KitchenNotesWeb.Models
{
    public class DetailedNoteModel
    {
        [Display(Name = "Username")]
        public string username { get; set; }
        [Display(Name = "Name")]
        public string forename { get; set; }
        public KitchenNotesDAL.Notes note {get; set;}
        [Display(Name = "Posted")]
        public string timeAgo { get; set; }
    }
}