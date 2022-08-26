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
    }
}
