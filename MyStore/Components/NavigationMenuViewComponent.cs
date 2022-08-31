using Microsoft.AspNetCore.Mvc;
using MyStore.Models.Repository;

namespace MyStore.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private ICategoryRepository _repository;

        public NavigationMenuViewComponent(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData.Values["category"];
            return View(_repository.Categories.Select(c => c.Name));
        }
    }
}
