using BakeryManagementPOSWebApp.Data.Enities;
using System.ComponentModel.DataAnnotations;

namespace BakeryManagementPOSWebApp.Data.DataTransfers
{
    public class ProductDT
    {
        [StringLength(100, ErrorMessage = "Product name is too long.")]
        public string Name { get; set; } = string.Empty;

        [DataType(DataType.Currency)]
        [Range(0.00d, 999.99d, ErrorMessage = "Price needs to be between 0.00 and 1000.00.")]
        public decimal Price { get; set; } = 0.00m;

        public string Description { get; set; } = string.Empty;

        public bool Availability { get; set; } = true;
    }
}
