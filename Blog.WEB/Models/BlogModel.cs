using System.ComponentModel.DataAnnotations;

namespace Blog.WEB.Models
{
    public class BlogModel
    {
       [Required]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }
    }
}