using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MyStore.Models;
using MyStore.Models.Cart;
using MyStore.Models.Repository;
using MyStore.Models.Services.EmailService;

namespace MyStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _repository;
        private readonly Cart _cart;
        private IEmailService _emailService;

        public OrderController(IOrderRepository repository, Cart cart, IEmailService emailService)
        {
            _repository = repository;
            _cart = cart;
            _emailService = emailService;
        }

        public IActionResult Checkout() => View(new Order());

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (_cart.Lines.Count == 0)
            {
                ModelState.AddModelError("", "Your cart is empty");
            }

            if (ModelState.IsValid)
            {
                order.Lines = _cart.Lines.ToArray();
                _repository.SaveOrder(order);
                _cart.Clear();

                EmailDto emailDto = new()
                {
                    To = order.Email!,
                    Subject = "You have made an order.",
                    Order = order
                };

                _emailService.SendEmail(emailDto);

                return RedirectToPage("/Completed", new { orderId = order.OrderId });
            }
            else
            {
                return View();
            }
        }
    }
}
