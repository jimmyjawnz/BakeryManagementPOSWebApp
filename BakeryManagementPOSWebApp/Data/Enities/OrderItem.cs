using BakeryManagementPOSWebApp.Data.Enities.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BakeryManagementPOSWebApp.Data.Enities
{
    [Table("OrderItems")]
    public class OrderItem : Entity
    {
        [Required]
        [Column("item_quantity", Order = 2, TypeName = "tinyint")]
        public int Quantity { get; set; } = 1;

        // Relational value for Product
        [Required]
        [Column("item_product_id", Order = 3)]
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        [Column("item_row_amount", Order = 4, TypeName = "decimal(8,2)")]
        public decimal RowPrice { get; set; } = 0.00m;

        [Required]
        [Column("item_order_id", Order = 5)]
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;
    }
}
