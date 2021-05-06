using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.DAL.Entities
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Body { get; set; }

        public ICollection<Post> Post { get; set; }

        public Tag()
        {
           Post  = new List<Post>();
        }


    }
}
