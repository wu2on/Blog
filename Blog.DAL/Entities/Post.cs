using Blog.DAL.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.DAL.Entities
{
    public class Post : Entity<int>, ISoftDeletable 
    {
        public override int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }

        [Required] 
        public DateTime CreateAt { get; set; }
        public string UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
        public ICollection<Comment> Comment { get; set; }
        public ICollection<Tag> Tag { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public Post()
        {
            Comment = new List<Comment>();
            Tag = new List<Tag>();
        }
    }
}