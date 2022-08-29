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

        public async Task CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategory(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task SaveCategory(Category category)
        {
            await _context.SaveChangesAsync();
        }
    }
}
