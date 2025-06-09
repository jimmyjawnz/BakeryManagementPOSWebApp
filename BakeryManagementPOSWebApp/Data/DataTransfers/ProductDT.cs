using BakeryManagementPOSWebApp.Data.Enities;
using System.ComponentModel.DataAnnotations;

namespace BakeryManagementPOSWebApp.Data.DataTransfers
{
    public class ProductDT
    {
        public int? ID { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, ErrorMessage = "Product name is too long.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Product price is required.")]
        [Range(0.00d, 999.99d, ErrorMessage = "Price needs to be between 0.00 and 1000.00.")]
        public decimal? Price { get; set; }

        public string? Description { get; set; }

        public bool? Availability { get; set; }

        public ICollection<OrderItem>? OrderItems { get; set; } = [];
    }
}
