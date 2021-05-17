using System.ComponentModel.DataAnnotations;

namespace Blog.WEB.Models
{
    public class SearchModel
    {
        [Required(ErrorMessage = "Field cannot be empty")]
        public string Text { get; set; }
    }
}