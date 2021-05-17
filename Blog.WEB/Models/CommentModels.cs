using System.ComponentModel.DataAnnotations;

namespace Blog.WEB.Models
{
    public class CreateCommentModel
    {
        [Required(ErrorMessage = "Comment cannot be empty")]
        public string Text { get; set; }
    }
}