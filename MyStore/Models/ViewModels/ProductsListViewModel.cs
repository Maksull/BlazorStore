using System.Reflection;

namespace MyStore.Models.ViewModels
{
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; } = Enumerable.Empty<Product>();
        public PagingInfo PagingInfo { get; set; } = new();

        public string? CurrentCategory { get; set; }
        public string? SortBy { get; set; }
        public string? SearchBy { get; set; }
        public List<PropertyInfo> ProductProperties { get; set; } = new();
    }
}
