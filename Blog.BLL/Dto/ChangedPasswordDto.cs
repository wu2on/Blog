namespace Blog.BLL.Dto
{
    public class ChangedPasswordDto
    {
        public string UserId { get; set; }
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }
    }
}
