using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KitchenNotesWeb.Models
{
    public class NotesIndexModel
    {
        public IEnumerable<DetailedNoteModel> noteList { get; set; }

        public NewNotesModel noteModel { get; set; }
        
    }
}