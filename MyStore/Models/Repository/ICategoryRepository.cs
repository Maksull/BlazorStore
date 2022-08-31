namespace MyStore.Models.Repository
{
    public interface ICategoryRepository
    {
        IQueryable<Category> Categories { get; }

        Task SaveCategory(Category category);
        Task CreateCategory(Category category);
        Task DeleteCategory(Category category);
    }
}
