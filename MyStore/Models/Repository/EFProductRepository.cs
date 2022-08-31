namespace MyStore.Models.Repository
{
    public class EFProductRepository : IProductRepository
    {
        private MyStoreDataContext _context;

        public EFProductRepository(MyStoreDataContext context)
        {
            _context = context;
        }

        public IQueryable<Product> Products => _context.Products;

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
            await _context.SaveChangesAsync();
        }
    }
}
