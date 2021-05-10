using System.ComponentModel.DataAnnotations;

namespace Blog.WEB.Models
{
    public class CreateCommentModel
    {
        [Required]
        public string Text { get; set; }
    }
}