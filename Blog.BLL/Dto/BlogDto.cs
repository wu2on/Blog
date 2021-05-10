﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.Dto
{
    public class BlogDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreateAt { get; set; }
        public string UserProfileId { get; set; }
        public UserDto Email {get; set; }
        IList<CommentDto> Comments { get; set; }
        public bool IsDeleted { get; set; }
    }
}
