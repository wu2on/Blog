using System.Collections.Generic;
using System.Threading.Tasks;

using Blog.BLL.Dto;
using Blog.BLL.Infrastructure;

namespace Blog.BLL.Interfaces
{
    public interface IBlogService
    {
        OperationDetails Create(BlogDto blogDto);
        Task<OperationDetails> AddComment(CommentDto commentDto);
        List<BlogDto> GetAllUserBlogs(string Id);
        List<BlogDto> GetAllBlogs();
        BlogDto GetDetails(int? Id);
        Task<OperationDetails> UpdateBlog(BlogDto blogDto);
        Task<OperationDetails> DeleteComment(int? Id, string userId);
        Task<OperationDetails> DeletePost(int Id);
        List<BlogDto> SearchBlogs(SearchDto searchDto);
        void Dispose();
    }
}
