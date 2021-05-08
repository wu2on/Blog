using Blog.BLL.Dto;
using Blog.BLL.Infrastructure;
using Blog.BLL.Interfaces;
using Blog.DAL.Entities;
using Blog.DAL.Interfaces;
using System.Threading.Tasks;

namespace Blog.BLL.Services
{
    public class BlogService : IBlogService
    {
        IUnitOfWork _uow { get; set; }

        public BlogService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<OperationDetails> Create(BlogDto blogDto)
        {
            if(blogDto != null)
            {
                Post post = new Post { Title = blogDto.Title, Text = blogDto.Text, Date = blogDto.Date, IsDeleted = blogDto.IsDeleted };

                _uow.PostRepository.Create(post);
                await _uow.SaveAsync();
                return new OperationDetails(true, "Blog has been successfully created", "");
            } else
            {
                return new OperationDetails(false, "Things went wrong... maybe blog is empty", "");
            }
            
        }
        public void Dispose()
        {
            _uow.Dispose();
        }
    }
}
