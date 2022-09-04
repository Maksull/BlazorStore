using Microsoft.AspNetCore.Mvc.ModelBinding;
using MyStore.Models.Cart;
using System.ComponentModel.DataAnnotations;

namespace MyStore.Models
{
    public class Order
    {
        [BindNever]
        public long OrderId { get; set; }
        [BindNever]
        public IEnumerable<CartLine> Lines { get; set; } = new List<CartLine>();

        [Required(ErrorMessage = "Please enter a name")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Please enter an email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Please enter the address line")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Please enter a city name")]
        public string? City { get; set; }

        [Required(ErrorMessage = "Please enter a country")]
        public string? Country { get; set; }

        public string? Zip { get; set; }

        public bool GiftWrap { get; set; }

        [BindNever]
        public bool Shipped { get; set; }
    }
}
