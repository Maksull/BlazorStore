using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyStore.Models.Repository;

namespace MyStore.Controllers
{
    public class HomeController : Controller
    {
        private IProductRepository _repository;

        public HomeController(IProductRepository repository)
        {
            _repository = repository;
        }   

        public IActionResult Index()
        {
            return View(_repository.Products.Include(p => p.Category).Include(p => p.Supplier));
        }
    }
}
