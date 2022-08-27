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

        public CartModel(IProductRepository repository, Cart cartService)
        {
            _repository = repository;
            Cart = cartService;
        }

        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; } = "/";

        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
        }

        public IActionResult OnPost(long productId, string returnUrl)
        {
            Product? product = _repository.Products.FirstOrDefault(p => p.ProductId == productId); 
            if(product != null)
            {
                Cart.AddItem(product, 1);
            }
            return RedirectToPage(new { returnUrl = returnUrl });
        }

        public IActionResult OnPostRemove(long productId, string returnUrl)
        {
            Cart.RemoveLine(Cart.Lines.First(cl => cl.Product.ProductId == productId).Product);
            return RedirectToPage(new {ReturnUrl = returnUrl});
        }
    }
}
