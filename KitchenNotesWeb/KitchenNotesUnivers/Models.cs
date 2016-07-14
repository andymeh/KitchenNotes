using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitchenNotesUnivers
{
    public class Tasks
    {
        public Guid taskId { get; set; }
        public string username { get; set; }
        public string forename { get; set; }
        public string taskDetail { get; set; }
        public string timeAgo { get; set; }
        public string assignedTo { get; set; }
    }

    public class Notes
    {
        public Guid noteId { get; set; }
        public string username { get; set; }
        public string forename { get; set; }
        public string timeAgo { get; set; }
        public string noteContent { get; set; }
    }

    public class Events
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string shortStart { get; set; }
        public string shortEnd { get; set; }
    }

    public class LoginInfo
    {
        public string HubId { get; set; }
        public string username { get; set; }
        public string HubName { get; set; }
    }
}
