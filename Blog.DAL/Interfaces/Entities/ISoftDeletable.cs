namespace Blog.DAL.Interfaces.Entities
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get; set; }

        void OnDelete();
    }
}
