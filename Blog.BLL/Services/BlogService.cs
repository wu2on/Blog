using AutoMapper;
using Blog.BLL.Dto;
using Blog.BLL.Infrastructure;
using Blog.BLL.Interfaces;
using Blog.DAL.Entities;
using Blog.DAL.Interfaces;
using System;
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
                
                IEnumerable<string> uniqueTags = CheckUniqueTags(blogDto.Text);

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

        public BlogDto GetDetails(int? Id)
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Post, BlogDto>().ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment.Where(o => !o.IsDeleted)));
                cfg.CreateMap<UserProfile, UserDto>();
                cfg.CreateMap<Comment, CommentDto>();
            });

            Mapper mapper = new Mapper(config);

            BlogDto blog = mapper.Map<BlogDto>(_uow.PostRepository.GetRange(x => x.Id == Id, p => p.UserProfile, c => c.Comment).FirstOrDefault());

            return blog;
        }
        public List<BlogDto> GetAllUserBlogs(string Id)
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Post, BlogDto>().ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment.Where(o => !o.IsDeleted)));
                cfg.CreateMap<UserProfile, UserDto>();
                cfg.CreateMap<Comment, CommentDto>();
            });

            Mapper mapper = new Mapper(config);
            List<BlogDto> userBlogs = mapper.Map<List<BlogDto>>(_uow.PostRepository.GetRange(x => x.UserProfileId == Id && !x.IsDeleted, p => p.UserProfile, c => c.Comment).OrderByDescending(x => x.CreateAt));
            return userBlogs;
        }

        public List<BlogDto> GetAllBlogs()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Post, BlogDto>().ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment.Where(o => !o.IsDeleted)));
                cfg.CreateMap<UserProfile, UserDto>();
                cfg.CreateMap<Comment, CommentDto>();
            });

            Mapper mapper = new Mapper(config);

            List<BlogDto> usersBlogs = mapper.Map<List<BlogDto>>(_uow.PostRepository.GetRange(x => !x.IsDeleted,p => p.UserProfile, c => c.Comment).OrderByDescending(x => x.CreateAt));
            return usersBlogs;
        }

        public List<BlogDto> SearchBlogs(SearchDto searchDto)
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Post, BlogDto>();
                cfg.CreateMap<UserProfile, UserDto>();
            });

            Mapper mapper = new Mapper(config);
            if (searchDto != null)
            {
                string uniqueTags = CheckUniqueTags(searchDto.Text).FirstOrDefault();

                IEnumerable<Post> searchResults = null;

                if(uniqueTags != null)
                {
                    searchResults = _uow.TagRepository.GetRange(p => p.Body == uniqueTags, x => x.Post.Select(z => z.UserProfile)).Select(x => x.Post).FirstOrDefault();
                } 
                else if(uniqueTags == null)
                {
                    searchResults = _uow.PostRepository.GetRange(p => p.UserProfile).Where(x => x.Text.Contains(searchDto.Text));
                }

                List<BlogDto> foundBlogs = mapper.Map<List<BlogDto>>(searchResults);

                return foundBlogs;


            }

            throw new NotImplementedException();
        }
        public async Task<OperationDetails> UpdateBlog (BlogDto blogDto)
        {
            if (blogDto != null)
            {
                var result = _uow.PostRepository.GetFirstOrDefault(x => x.Id == blogDto.Id);
                result.Title = blogDto.Title;
                result.Text = blogDto.Text;
                _uow.PostRepository.Update(result);
                await _uow.SaveAsync();
                return new OperationDetails(true, "Blog has been successfully updated", "");
            }

            return new OperationDetails(false, "Things went wrong...", "");
        }
        
        public async Task<OperationDetails> DeleteComment(int? Id, string userId)
        {
            if (Id != null)
            {
                var result = _uow.CommentRepository.GetFirstOrDefault(x => x.Id == Id);

                result.IsDeleted = true;
                _uow.CommentRepository.Update(result);
                await _uow.SaveAsync();
                return new OperationDetails(true, "Blog has been successfully updated", "");
            }
            else
            {
                return new OperationDetails(false, "Things went wrong...", "");
            }
        }
        public async Task<OperationDetails> DeletePost(int Id)
        {
            if (Id != null)
            {
                var result = _uow.PostRepository.GetFirstOrDefault(x => x.Id == Id);
                result.IsDeleted = true;
                _uow.PostRepository.Update(result);
                await _uow.SaveAsync();
                return new OperationDetails(true, "Blog has been successfully updated", "");
            }
            else
            {
                return new OperationDetails(false, "Things went wrong...", "");
            }
        }

        public void Dispose()
        {
            _uow.Dispose();
        }

        private IEnumerable<string> CheckUniqueTags(string text)
        {
            
            Regex regex = new Regex(@"(?<=#)\w+");
            MatchCollection matches = regex.Matches(text);
            List<string> tags = null;
            IEnumerable<string> uniqueTags = null;

            if (matches != null)
            {
                tags = new List<string>();

                foreach (Match tag in matches)
                {
                    tags.Add(tag.Value.ToLower());
                }

                uniqueTags = tags.Distinct();

                return uniqueTags;

            }

            return uniqueTags;
        }
    }
}

