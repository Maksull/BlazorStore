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

        public async Task CreateSupplier(Supplier supplier)
        {
            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSupplier(Supplier supplier)
        {
            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();
        }

        public async Task SaveSupplier(Supplier supplier)
        {
            await _context.SaveChangesAsync();
        }
    }
}
