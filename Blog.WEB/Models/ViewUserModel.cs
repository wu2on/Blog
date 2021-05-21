using Blog.BLL.Dto;
using System.Collections.Generic;

namespace Blog.WEB.Models
{
    public class ViewUserModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public  List<UserDto> Users { get; set; } 
    }
}