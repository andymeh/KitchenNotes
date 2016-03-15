using Newtonsoft.Json;
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
        [StringLength(20, ErrorMessage = "Title can be no longer than 20 characters")]
        public string title { get; set; }

        [Display(Name = "Event Description")]
        public string Description { get; set; }

        [Display(Name = "Event Start*")]
        [Required(ErrorMessage = "Start Date is required")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy hh:mm:ss}")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Event End*")]
        [Required(ErrorMessage = "End Date  is required")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy hh:mm:ss}")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Event Importance")]
        public string importance { get; set; }
    }

    public class CalendarEventModel
    {
        public string id { get; set; }
        public string title { get; set; }

        [JsonProperty(PropertyName = "class")]
        public string Class { get; set; }

        public string url { get; set; }
        public long start { get; set; }
        public long end { get; set; }
    }

    public class EventDetailModel
    {
        public Guid id { get; set; }
        public string title { get; set; }
        public string description { get; set; }

        [JsonProperty(PropertyName = "class")]
        public string Class { get; set; }
        public string url { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
    }
}