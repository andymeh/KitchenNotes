using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitchenNotesWeb.Models
{
    public class NewUserViewModel
    {
        [Display(Name = "Enter your 8 digit Hub Reference*")]
        [Required(ErrorMessage = "Hub name is required")]
        [StringLength(8, ErrorMessage = "Hub Name can be no larger than 8 characters")]
        public string HubIdStart { get; set; }

        [Display(Name = "Username*")]
        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, ErrorMessage = "Username can be no larger than 50 characters")]
        public string UserName { get; set; }

        [Display(Name = "Forename*")]
        [Required(ErrorMessage = "Forename is required")]
        [StringLength(50, ErrorMessage = "Forename can be no larger than 50 characters")]
        public string Forename { get; set; }

        [Display(Name = "Surname*")]
        [Required(ErrorMessage = "Surname is required")]
        [StringLength(50, ErrorMessage = "Surname can be no larger than 50 characters")]
        public string Surname { get; set; }

        [Display(Name = "Password*")]
        [Required(ErrorMessage = "Password is required")]
        [StringLength(50, ErrorMessage = "Password can be no larger than 40 characters")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password*")]
        [Required(ErrorMessage = "Password Confirmation is required")]
        [StringLength(50, ErrorMessage = "Password can be no larger than 40 characters")]
        public string Password2 { get; set; }

        [Display(Name = "Date Of Birth*")]
        [Required(ErrorMessage = "Date of Birth is required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DOB { get; set; }

        [Display(Name = "Email*")]
        [Required(ErrorMessage = "Email Address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string UserEmail { get; set; }
    }
}
