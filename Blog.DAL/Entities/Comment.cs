using Blog.DAL.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.DAL.Entities
{
    public class Comment : ISoftDeletable
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public void OnDelete()
        {
            IsDeleted = true;
        }
    }
}
