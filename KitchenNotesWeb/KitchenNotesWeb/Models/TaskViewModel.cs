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
        [Display(Name = "Task")]
        public string taskDetail { get; set; }
        [Display(Name = "User")]
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
        public string taskDetail { get; set; }
        [Display(Name = "Posted")]
        public string timeAgo { get; set; }
        public string assignedTo { get; set; }
        public Tasks task { get; set; }
    }

    public class TasksIndexModel
    {
        public IEnumerable<DetailedTaskModel> taskList { get; set; }
        public NewTaskModel taskModel { get; set; }
    }

    public class EditTaskModel
    {
        public Guid taskId { get; set; }
        [Required(ErrorMessage = "Task detail is required!")]
        public string taskDetail { get; set; }
        public string assignedTo { get; set; }
        public List<User> hubUsers { get; set; }
        public SelectList userList { get; set; }
    }
    
}
