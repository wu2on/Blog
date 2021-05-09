using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.BLL.Dto;
using Blog.BLL.Infrastructure;

namespace Blog.BLL.Interfaces
{
    public interface IBlogService
    {
        Task<OperationDetails> Create(BlogDto blogDto);
        List<BlogDto> GetAllUserBlogs(string Id);
        void Dispose();
    }
}
