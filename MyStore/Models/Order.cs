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
        public ICollection<CartLine> Lines { get; set; } = new List<CartLine>();

        [Required(ErrorMessage = "Please enter a name")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Please enter the first address line")]
        public string? Line1 { get; set; }
        public string? Line2 { get; set; }

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
