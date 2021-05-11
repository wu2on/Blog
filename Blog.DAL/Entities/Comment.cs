using Blog.DAL.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.DAL.Entities
{
    public class Comment : Entity<int>, ISoftDeletable
    {
        public override int Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public DateTime CreateAt { get; set; }
        [Required]
        public string UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
        public string UserEmail { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}
