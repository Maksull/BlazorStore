namespace MyStore.Models.Repository
{
    public interface ICategoryRepository
    {
        IQueryable<Category> Categories { get; }
    }
}
