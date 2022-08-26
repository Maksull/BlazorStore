using Microsoft.EntityFrameworkCore;

namespace MyStore.Models
{
    public class MyStoreDataContext : DbContext
    {
        public MyStoreDataContext(DbContextOptions<MyStoreDataContext> opts) : base(opts) { }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Supplier> Suppliers => Set<Supplier>();
    }
}
