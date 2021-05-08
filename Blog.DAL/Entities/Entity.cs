using Blog.DAL.Interfaces.Entities;
using System.ComponentModel.DataAnnotations;

namespace Blog.DAL.Entities
{
    public abstract class Entity<T> : IEntity<T>
    {
        [Key]
        public abstract T Id { get; set; }
    }
}
