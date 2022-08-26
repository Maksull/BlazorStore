namespace MyStore.Models.Repository
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
    }
}
