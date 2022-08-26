namespace MyStore.Models.Repository
{
    public class EFSupplierRepository : ISupplierRepository
    {
        private MyStoreDataContext _context;

        public EFSupplierRepository(MyStoreDataContext context)
        {
            _context = context;
        }

        public IQueryable<Supplier> Suppliers => _context.Suppliers;
    }
}
