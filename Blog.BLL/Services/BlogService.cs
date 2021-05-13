using AutoMapper;
using Blog.BLL.Dto;
using Blog.BLL.Infrastructure;
using Blog.BLL.Interfaces;
using Blog.DAL.Entities;
using Blog.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
            if (blogDto != null)
            {
                List<Tag> tags = new List<Tag>();
                Regex regex = new Regex(@"(?<=#)\w+");
                IEnumerable<string> uniqueTags = Unique(regex.Matches(blogDto.Text));

                if(uniqueTags != null)
                {
                    foreach (string tag in uniqueTags)
                    {
                        Tag result = _uow.TagRepository.GetFirstOrDefault(x => x.Body == tag);

                        if (result == null)
                        {
                            tags.Add(new Tag { Body = tag });
                        }
                        else
                        {
                            tags.Add(result);
                        }
                    }
                }

                Post post = new Post { Title = blogDto.Title, Text = blogDto.Text, CreateAt = blogDto.CreateAt, IsDeleted = blogDto.IsDeleted, UserProfileId = blogDto.UserProfileId, Tag = tags };

                Post createdPost = _uow.PostRepository.Create(post);

                await _uow.SaveAsync();
                return new OperationDetails(true, "Blog has been successfully created", "");
            }
            else
            {
                return new OperationDetails(false, "Things went wrong... maybe blog is empty", "");
            }

        }

        public async Task<OperationDetails> AddComment(CommentDto commentDto)
        {
            if (commentDto != null)
            {
                Comment comment = new Comment { Text = commentDto.Text, PostId = commentDto.PostId, IsDeleted = commentDto.IsDeleted, CreateAt = commentDto.CreateAt, UserProfileId = commentDto.UserProfileId, UserEmail = commentDto.UserEmail };

                Comment create = _uow.CommentRepository.Create(comment);
                await _uow.SaveAsync();
                return new OperationDetails(true, "Comment has been successfully created", "");
            }
            else
            {
                return new OperationDetails(false, "Things went wrong... maybe blog is empty", "");
            }

        }
        public List<BlogDto> GetAllUserBlogs(string Id)
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Post, BlogDto>();
                cfg.CreateMap<UserProfile, UserDto>();
                cfg.CreateMap<Comment, CommentDto>();
            });

            Mapper mapper = new Mapper(config);
            List<BlogDto> userBlogs = mapper.Map<List<BlogDto>>(_uow.PostRepository.GetWithInclude(x => x.UserProfileId == Id, p => p.UserProfile, c => c.Comment));
            return userBlogs;
        }

        public List<BlogDto> GetAllBlogs()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Post, BlogDto>();
                cfg.CreateMap<UserProfile, UserDto>();
                cfg.CreateMap<Comment, CommentDto>();
            });

            Mapper mapper = new Mapper(config);
            List<BlogDto> usersBlogs = mapper.Map<List<BlogDto>>(_uow.PostRepository.GetWithInclude(p => p.UserProfile, c => c.Comment));
            return usersBlogs;
        }
        public void Dispose()
        {
            _uow.Dispose();
        }

        private IEnumerable<string> Unique(MatchCollection matches)
        {
            List<string> tags = null;
            IEnumerable<string> uniqueTags = null;

            if (matches != null)
            {
                tags = new List<string>();

                foreach (Match tag in matches)
                {
                    tags.Add(tag.Value.ToLower());
                }

                uniqueTags = tags.Distinct<string>();

                return uniqueTags;

            }

            return uniqueTags;
        }
    }
}

