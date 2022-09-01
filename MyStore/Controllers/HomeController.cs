using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyStore.Models.Repository;
using MyStore.Models.ViewModels;

namespace MyStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _repository;

        public HomeController(IProductRepository repository)
        {
            _repository = repository;
        }   
        public int PageSize { get; set; } = 4;

        public IActionResult Index(string? category, int productPage = 1)
        {
            return View(new ProductsListViewModel
            {
                Products = _repository.Products.Where(p => p.Category != null && p.Category.Name == category || category == null).Include(p => p.Category).Include(p => p.Supplier).OrderBy(p => p.ProductId).
                                Skip((productPage - 1) * PageSize).Take(PageSize),
                PagingInfo =
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ? _repository.Products.Count() :  _repository.Products.Where(p => p.Category != null  && p.Category.Name == category).Count()
                },
                CurrentCategory = category
            });
        }
    }
}
