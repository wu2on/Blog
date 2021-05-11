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
                Post post = new Post { Title = blogDto.Title, Text = blogDto.Text, CreateAt = blogDto.CreateAt, IsDeleted = blogDto.IsDeleted, UserProfileId = blogDto.UserProfileId };

                Post create = _uow.PostRepository.Create(post);
                await _uow.SaveAsync();
                return new OperationDetails(true, "Blog has been successfully created", "");
            } else
            {
                return new OperationDetails(false, "Things went wrong... maybe blog is empty", "");
            }
            
        }

        public async Task<OperationDetails> AddComment(CommentDto commentDto)
        {
            if (commentDto != null)
            {
                Comment comment = new Comment { Text = commentDto.Text, PostId = commentDto.PostId, IsDeleted = commentDto.IsDeleted, CreateAt = commentDto.CreateAt, UserProfileId = commentDto.UserProfileId};

                Comment create = _uow.CommentRepository.Create(comment);
                await _uow.SaveAsync();
                return new OperationDetails(true, "Blog has been successfully created", "");
            }
            else
            {
                return new OperationDetails(false, "Things went wrong... maybe blog is empty", "");
            }

        }
        public List<BlogDto> GetAllUserBlogs(string Id)
        { 
            var result = _uow.PostRepository.GetWithInclude(x => x.UserProfileId == Id, p => p.UserProfile);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Post, BlogDto>();
                cfg.CreateMap<UserProfile, UserDto>();
            });
            
           var mapper = new Mapper(config);
           var userBlogs = mapper.Map<List<BlogDto>>(_uow.PostRepository.GetWithInclude(x => x.UserProfileId == Id, p => p.UserProfile));
           return userBlogs;
        }

        public List<BlogDto> GetAllBlogs()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Post, BlogDto>();
                cfg.CreateMap<UserProfile, UserDto>();
                cfg.CreateMap<Comment, CommentDto>();
            });

            var mapper = new Mapper(config);
            var userBlogs = mapper.Map<List<BlogDto>>(_uow.PostRepository.GetWithInclude(p => p.UserProfile, c => c.Comment));
            return userBlogs;
        }
        public void Dispose()
        {
            _uow.Dispose();
        }
    }
}
