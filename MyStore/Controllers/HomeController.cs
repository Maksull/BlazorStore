using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyStore.Models;
using MyStore.Models.Repository;
using MyStore.Models.ViewModels;
using System.Reflection;

namespace MyStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _repository;
        private List<PropertyInfo> _productProperties => new Product().GetType().GetProperties().ToList();
        private IEnumerable<Product> _productsData { get; set; } = Enumerable.Empty<Product>();


        public HomeController(IProductRepository repository)
        {
            _repository = repository;
        }   
        public int PageSize { get; set; } = 4;


        public IActionResult Index(string? category, int productPage = 1, string sortBy = "ProductId", string? searchBy = "")
        {
            int totalItems = default;

            _productsData = _repository.Products.Where(p => p.Category != null && p.Category.Name == category || category == null).Include(p => p.Category).Include(p => p.Supplier);

            if(category == null && searchBy == null)
            {
                totalItems = _productsData.Count();
            }
            else if (category != null && searchBy != null)
            {
                totalItems = _productsData.Where(p => p.Category != null && p.Category.Name == category && p.Name.Contains(searchBy)).Count();
            }
            else if(category != null)
            {
                totalItems = _productsData.Where(p => p.Category != null && p.Category.Name == category).Count();
            }
            else if (searchBy != null)
            {
                totalItems = _productsData.Where(p => p.Name.Contains(searchBy ?? "")).Count();
            }

            return View(new ProductsListViewModel
            {
                /*Products = _repository.Products.Where(p => p.Category != null && p.Category.Name == category || category == null).Include(p => p.Category).Include(p => p.Supplier).OrderBy(p => p.ProductId).
                                Skip((productPage - 1) * PageSize).Take(PageSize),*/
                Products = _productsData.Where(p => p.Name.Contains(searchBy ?? "")).OrderBy(p => p.GetType().GetProperty(sortBy)!.GetValue(p)).
                                Skip((productPage - 1) * PageSize).Take(PageSize),
                ProductProperties = _productProperties,
                PagingInfo =
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    //TotalItems = category == null ? _repository.Products.Count() :  _repository.Products.Where(p => p.Category != null  && p.Category.Name == category).Count()
                    TotalItems = totalItems
                },
                CurrentCategory = category
            });
        }
    }
}
