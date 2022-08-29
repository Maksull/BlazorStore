namespace MyStore.Models.Repository
{
    public interface ISupplierRepository
    {
        IQueryable<Supplier> Suppliers { get; }

        Task SaveSupplier(Supplier supplier);
        Task CreateSupplier(Supplier supplier);
        Task DeleteSupplier(Supplier supplier);
    }
}
