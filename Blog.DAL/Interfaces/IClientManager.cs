using Blog.DAL.Entities;
using System;

namespace Blog.DAL.Interfaces
{
    public interface IClientManager : IDisposable
    {
        void Create(ClientProfile item);
    }
}