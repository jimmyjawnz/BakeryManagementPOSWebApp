using BakeryManagementPOSWebApp.Data.Enities.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace BakeryManagementPOSWebApp.Data.Enities
{
    public class OrderItem : Entity
    {
        [Required(ErrorMessage = "Quantity of product is requried.")]
        public int Quantity { get; set; } = 1;
        [Required(ErrorMessage = "Product is required.")]
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public decimal? ProductPrice { get; set; }


        [Required]
        public decimal RowPrice { get; set; } = 0.00m;

        [Required]
        public string ProductName { get; set; } = string.Empty;

        [Required(ErrorMessage = "No Order Relation.")]
        public int OrderId { get; set; }
        public Order? Order { get; set; }

        // Fucntions
        public decimal CalcRowPrice()
        {
            RowPrice = ProductPrice!.Value * Quantity;
            return RowPrice;
        }
    }
}
