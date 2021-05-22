using System.ComponentModel.DataAnnotations;

using Blog.DAL.Interfaces.Entities;

namespace Blog.DAL.Entities
{
    public abstract class Entity<T> : IEntity<T>
    {
        [Key]
        public abstract T Id { get; set; }
    }
}
