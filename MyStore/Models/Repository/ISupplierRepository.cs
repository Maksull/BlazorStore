namespace MyStore.Models.Repository
{
    public interface ISupplierRepository
    {
        IQueryable<Supplier> Suppliers { get; }
    }
}
