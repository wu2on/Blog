using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.BLL.Dto;
using Blog.BLL.Infrastructure;

namespace Blog.BLL.Interfaces
{
    public interface IBlogService
    {
        Task<OperationDetails> Create(BlogDto blogDto);
        Task<OperationDetails> AddComment(CommentDto commentDto);
        List<BlogDto> GetAllUserBlogs(string Id);
        List<BlogDto> GetAllBlogs();
        List<SearchDto> SearchBlogs(SearchDto searchDto);
        void Dispose();
    }
}
