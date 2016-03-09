using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KitchenNotesWeb.Models
{
    public class TasksIndexModel
    {
        public class NotesIndexModel
        {
            public IEnumerable<DetailedTaskModel> taskList { get; set; }

            public NewTaskModel taskModel { get; set; }

        }
    }
}