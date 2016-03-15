using KitchenNotesDAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace KitchenNotesWeb.Models
{
    public class NewTaskModel
    {
        public string taskDetail { get; set; }
        public string assignedTo { get; set; }
        public List<User> hubUsers { get; set; }
        public SelectList userList { get; set; }
    }

    public class DetailedTaskModel
    {
        [Display(Name = "Posted By")]
        public string username { get; set; }
        [Display(Name = "Posted By")]
        public string forename { get; set; }
        public KitchenNotesDAL.Tasks task { get; set; }
        [Display(Name = "Posted")]
        public string timeAgo { get; set; }
    }

    public class TasksIndexModel
    {
        public IEnumerable<DetailedTaskModel> taskList { get; set; }

        public NewTaskModel taskModel { get; set; }
    }
    
}
