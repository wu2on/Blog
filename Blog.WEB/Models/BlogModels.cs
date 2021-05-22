using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Blog.BLL.Dto;

namespace Blog.WEB.Models
{
    public class BlogPreviewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string UserEmail { get; set; }
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
        [Required(ErrorMessage = "Title cannot be empty")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Body cannot be empty")]
        public string Text { get; set; }
    }
}