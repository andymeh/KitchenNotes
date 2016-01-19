using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KitchenNotesWeb.Models
{
    public class NotesModel
    {

        public Guid userHubId { get; set; }
        public string username { get; set; }
        public string Note { get; set; }
        public DateTime dateAdded { get; set; }

    }
}