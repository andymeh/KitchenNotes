using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitchenNotesWeb.Models
{
    public class DetailedNoteModel
    {
        [Display(Name = "Username")]
        public string username { get; set; }
        [Display(Name = "Name")]
        public string forename { get; set; }
        public KitchenNotesDAL.Notes note { get; set; }
        [Display(Name = "Posted")]
        public string timeAgo { get; set; }
    }

    public class NewNotesModel
    {

        [Display(Name = "Note")]
        [Required(ErrorMessage = "Text is required..")]
        public string noteContent { get; set; }

    }

    public class NotesIndexModel
    {
        public IEnumerable<DetailedNoteModel> noteList { get; set; }

        public NewNotesModel noteModel { get; set; }

    }
}
