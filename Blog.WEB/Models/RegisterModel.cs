using System.ComponentModel.DataAnnotations;

namespace Blog.WEB.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Email cannot be empty")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password cannot be empty")]
        [StringLength(16, MinimumLength = 6, ErrorMessage = "Password length must be between 6 and 16 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm password cannot be empty")]
        [DataType(DataType.Password, ErrorMessage = "ConfirmPassword and Password do not match")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "First name cannot be empty")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name must contain only characters")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name cannot be empty")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name must contain only characters")]
        public string LastName { get; set; }
    }
}