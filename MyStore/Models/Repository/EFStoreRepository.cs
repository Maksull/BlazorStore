using Microsoft.EntityFrameworkCore;

namespace MyStore.Models.Repository
{
    public class EFStoreRepository : IStoreRepository
    {
        private readonly MyStoreDataContext _context;

        public EFStoreRepository(MyStoreDataContext context)
        {
            _context = context;
        }

        public IQueryable<Product> Products => _context.Products;

        public IQueryable<Supplier> Suppliers => _context.Suppliers;

        public IQueryable<Category> Categories => _context.Categories;

        public async Task CreateProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task SaveProduct(Product product)
        {
            await _context.SaveChangesAsync(); ;
        }
    }
}
