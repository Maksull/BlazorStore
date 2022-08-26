namespace MyStore.Models.Repository
{
    public class EFCategoryRepository : ICategoryRepository
    {
        private MyStoreDataContext _context;

        public EFCategoryRepository(MyStoreDataContext context)
        {
            _context = context;
        }

        public IQueryable<Category> Categories => _context.Categories;
    }
}
