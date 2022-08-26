using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyStore.Infrastructure;
using MyStore.Models;
using MyStore.Models.Cart;
using MyStore.Models.Repository;

namespace MyStore.Pages
{
    public class CartModel : PageModel
    {
        private IProductRepository _repository;

        public CartModel(IProductRepository repository)
        {
            _repository = repository;
        }

        public Cart? Cart { get; set; }
        public string ReturnUrl { get; set; } = "/";

        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
            Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
        }

        public IActionResult OnPost(long productId, string returnUrl)
        {
            Product? product = _repository.Products.FirstOrDefault(p => p.ProductId == productId); 
            if(product != null)
            {
                Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new();
                Cart.AddItem(product, 1);
                HttpContext.Session.SetJson("cart", Cart);
            }
            return RedirectToPage(new { returnUrl = returnUrl });
        }
    }
}
