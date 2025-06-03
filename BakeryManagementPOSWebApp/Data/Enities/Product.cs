using BakeryManagementPOSWebApp.Data.Enities.Abstractions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BakeryManagementPOSWebApp.Data.Enities
{
    [Table("Products")]
    public class Product : Entity
    {
        [Required]
        [Column("product_name", Order = 2, TypeName = "nvarchar(100)")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Column("product_price", Order = 3, TypeName = "decimal(8,2)")]
        public decimal Price { get; set; } = 0.00m;

        [Column("product_description", Order = 4, TypeName = "nvarchar(max)")]
        public string? Description { get; set; }

        [Required]
        [Column("product_availability", Order = 1, TypeName = "bit")]
        public bool IsAvailable { get; set; } = true;

        // Relational value for OrderItems
        public ICollection<OrderItem> OrderItems { get; } = [];
    }
}
