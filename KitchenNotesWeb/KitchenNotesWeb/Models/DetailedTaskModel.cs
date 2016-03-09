using System.ComponentModel.DataAnnotations;

namespace KitchenNotesWeb.Models
{
    public class DetailedTaskModel
    {
        [Display(Name = "Posted By")]
        public string username { get; set; }
        [Display(Name = "Posted By")]
        public string forename { get; set; }
        public KitchenNotesDAL.Task task { get; set; }
        [Display(Name = "Posted")]
        public string timeAgo { get; set; }
    }
}