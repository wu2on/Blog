using AutoMapper;
using Blog.BLL.Dto;
using Blog.BLL.Infrastructure;
using Blog.BLL.Interfaces;
using Blog.DAL.Entities;
using Blog.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
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
                Post post = new Post { Title = blogDto.Title, Text = blogDto.Text, CreateAt = blogDto.CreateAt, IsDeleted = blogDto.IsDeleted, UserProfileId = blogDto.UserProfile_Id };

                Post create = _uow.PostRepository.Create(post);
                await _uow.SaveAsync();
                return new OperationDetails(true, "Blog has been successfully created", "");
            } else
            {
                return new OperationDetails(false, "Things went wrong... maybe blog is empty", "");
            }
            
        }
        public List<BlogDto> GetAllUserBlogs(string Id)
        {
           //List<Post> userBlogs = _uow.PostRepository.Find((x => x.UserProfileId == Id)).ToList();
           var config = new MapperConfiguration(cfg => cfg.CreateMap<Post, BlogDto>());
           var mapper = new Mapper(config);
           var userBlogs = mapper.Map<List<BlogDto>>(_uow.PostRepository.Find((x => x.UserProfileId == Id)));
           return userBlogs;
        }
        public void Dispose()
        {
            _uow.Dispose();
        }
    }
}
