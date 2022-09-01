using System.Threading.Tasks;

namespace MyStore.Models.Repository
{
    public interface IStoreRepository
    {
        IQueryable<Product> Products { get; }
        IQueryable<Supplier> Suppliers { get; }
        IQueryable<Category> Categories { get; }

        Task SaveProduct(Product product);
        Task CreateProduct(Product product);
        Task DeleteProduct(Product product);
    }
}
