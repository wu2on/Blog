using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.DAL.Entities
{
    public class Tag : Entity<int>
    {
        public override int Id { get; set; }
        [Required]
        public string Body { get; set; }

        public ICollection<Post> Post { get; set; }

        public Tag()
        {
           Post  = new List<Post>();
        }
    }
}
