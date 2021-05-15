using Blog.BLL.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.WEB.Models
{
    public class BlogPreviewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreateAt { get; set; }
    }
    public class ViewBlogModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreateAt { get; set; }
        IList<CommentDto> Comments { get; set; }
    }
    public class CreateBlogModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }
    }
}