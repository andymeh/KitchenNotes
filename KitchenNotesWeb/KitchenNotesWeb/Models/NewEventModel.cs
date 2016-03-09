using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KitchenNotesWeb.Models
{
    public class NewEventModel
    {
        [Display(Name = "Event Title*")]
        [Required(ErrorMessage = "Event Title is required")]
        public string title { get; set; }

        [Display(Name = "Event Description")]
        public string Description { get; set; }

        [Display(Name = "Event Start*")]
        [Required(ErrorMessage = "Start Date is required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Event End*")]
        [Required(ErrorMessage = "Start  is required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Event Importance")]
        public string importance { get; set; }
    }
}