using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KitchenNotesWeb.Models
{
    public class NotesIndexModel
    {
        public IEnumerable<KitchenNotesDAL.Notes> noteList { get; set; }

        public NotesModel noteModel { get; set; }
        
    }
}