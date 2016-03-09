using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KitchenNotesWeb.Models
{
    public class CalendarEventModel
    {
        public string id { get; set; }
        public string title { get; set; }

        [JsonProperty(PropertyName = "class")]
        public string Class { get; set; }
        
        public string url = "URL";
        public long start { get; set; }
        public long end { get; set; }
    }
}