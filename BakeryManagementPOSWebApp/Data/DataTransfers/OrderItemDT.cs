using BakeryManagementPOSWebApp.Data.Enities.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace BakeryManagementPOSWebApp.Data.Enities
{
    public class OrderItemDT
    {
        public int? ID { get; set; }

        [Required(ErrorMessage = "Quantity of product is requried.")]
        public int? Quantity { get; set; }

        [Required(ErrorMessage = "Product is required.")]
        public int? ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal? ProductPrice { get; set; }
        public Product? Product { get; set; }

        [Required(ErrorMessage = "A row price is required.")]
        public decimal? RowPrice { get; set; } = 0.00m;

        [Required(ErrorMessage = "A order must be linked.")]
        public int? OrderId { get; set; }
        public Order? Order { get; set; }
    }
}
