using System;

namespace Blog.BLL.Dto
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }   
        public DateTime CreateAt { get; set; }
        public string Role { get; set; }
        public bool IsDeleted { get; set; }
    }
}