﻿using System.Threading.Tasks;
using Blog.BLL.Dto;
using Blog.BLL.Infrastructure;

namespace Blog.BLL.Interfaces
{
    public interface IBlogService
    {
        Task<OperationDetails> Create(BlogDto blogDto);
        void Dispose();
    }
}
