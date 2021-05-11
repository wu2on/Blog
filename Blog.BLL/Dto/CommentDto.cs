using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.Dto
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsDeleted { get; set; }
        public string UserProfileId { get; set; }
        public string UserEmail { get; set; }
        public DateTime CreateAt { get; set; }
        public int PostId { get; set; }

    }
}
