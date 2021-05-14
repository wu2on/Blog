using System.ComponentModel.DataAnnotations;

namespace Blog.WEB.Models
{
    public class SearchModel
    {
        [Required]
        public string Text { get; set; }
    }
}