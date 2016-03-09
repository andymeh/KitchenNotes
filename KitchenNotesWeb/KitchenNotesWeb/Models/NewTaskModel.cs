using KitchenNotesDAL;
using System.Collections.Generic;

namespace KitchenNotesWeb.Models
{
    public class NewTaskModel
    {
        public string taskDetail { get; set; }
        public string assignedTo { get; set; }
        public List<User> hubUsers { get; set; }
    }
}