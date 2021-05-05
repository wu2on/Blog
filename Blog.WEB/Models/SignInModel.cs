using System.ComponentModel.DataAnnotations;

namespace Blog.WEB.Models
{
    public class SignInModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}