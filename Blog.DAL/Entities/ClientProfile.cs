﻿using Blog.DAL.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.DAL.Entities
{
    public class ClientProfile : ISoftDeletable
    {
        [Key]
        [ForeignKey("User")]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual User User { get; set; }

        public ICollection<Post> Posts { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ClientProfile()
        {
            Posts = new List<Post>();
            Comments = new List<Comment>();
        }



        public bool IsDeleted { get; set; }
        public void OnDelete()
        {
            IsDeleted = true;
        }
    }
}