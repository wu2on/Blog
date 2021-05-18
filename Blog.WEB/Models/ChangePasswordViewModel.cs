using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog.WEB.Models
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Password cannot be empty")]
        [StringLength(16, MinimumLength = 6, ErrorMessage = "Password length must be between 6 and 16 characters")]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Password cannot be empty")]
        [StringLength(16, MinimumLength = 6, ErrorMessage = "Password length must be between 6 and 16 characters")]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Re-type password")]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}