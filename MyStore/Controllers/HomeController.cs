using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyStore.Models.Repository;
using MyStore.Models.ViewModels;

namespace MyStore.Controllers
{
    public class HomeController : Controller
    {
        private IProductRepository _repository;
        private int _pageSize = 4;

        public HomeController(IProductRepository repository)
        {
            _repository = repository;
        }   

        public IActionResult Index(string? category, int productPage = 1)
        {
            return View(new ProductsListViewModel
            {
                Products = _repository.Products.Where(p => p.Category.Name == category || category == null).Include(p => p.Category).Include(p => p.Supplier).OrderBy(p => p.ProductId).Skip((productPage - 1) * _pageSize).Take(_pageSize),
                PagingInfo =
                {
                    CurrentPage = productPage,
                    ItemsPerPage = _pageSize,
                    TotalItems = category == null ? _repository.Products.Count() :  _repository.Products.Where(p => p.Category.Name == category).Count()
                },
                CurrentCategory = category
            });
        }
    }
}
