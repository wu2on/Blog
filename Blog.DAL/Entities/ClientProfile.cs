using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.DAL.Entities
{
    public class ClientProfile
    {
        [Key]
        [ForeignKey("User")]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual User User { get; set; }
    }
}