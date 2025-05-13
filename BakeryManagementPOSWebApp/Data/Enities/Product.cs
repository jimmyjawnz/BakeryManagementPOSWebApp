using BakeryManagementPOSWebApp.Data.Enities.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace BakeryManagementPOSWebApp.Data.Enities
{
    public class Product : Entity
    {
        [Required(ErrorMessage = "Product name is required.")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Product price is required.")]
        [Range(0.00d, 999.99d, ErrorMessage = "Price needs to be between 0.00 and 1000.00.")]
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public bool IsAvailable { get; set; } = true;

        public ICollection<OrderItem> OrderItems { get; set; } = [];
    }
}
