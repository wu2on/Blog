using System.ComponentModel.DataAnnotations;

namespace Blog.WEB.Models
{
    public class SignInModel
    {
        [Required(ErrorMessage = "Email cannot be empty")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password cannot be empty")]
        [StringLength(16, MinimumLength = 6, ErrorMessage = "Password length must be between 6 and 16 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}