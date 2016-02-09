using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitchenNotesWeb.Models
{
    public class NotesModel
    {


        [Display(Name = "Note")]
        [Required(ErrorMessage = "Text is required..")]
        public string noteContent { get; set; }

    }
}