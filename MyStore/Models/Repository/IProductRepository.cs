namespace MyStore.Models.Repository
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }

        Task SaveProduct(Product product);
        Task CreateProduct(Product product);
        Task DeleteProduct(Product product);
    }
}
