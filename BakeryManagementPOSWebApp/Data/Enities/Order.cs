using BakeryManagementPOSWebApp.Data.Enities.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace BakeryManagementPOSWebApp.Data.Enities
{
    public enum Status
    {
        Unknown,
        Complete,
        WaitingPickup,
        PassedPickupDate,
        NeedsProcessing
    }

    [Table("Orders")]
    public class Order : Entity
    {
        // Customer who ordered
        [Required]
        [Column("order_ordered_by", Order = 2, TypeName = "int")]
        public int CustomerID { get; set; }
        public Customer OrderedBy { get; set; } = null!;

        // Employee who processed
        [Required]
        [Column("order_processed_by", Order = 3, TypeName = "nvarchar(max)")]
        public string EmployeeID { get; set; } = string.Empty;
        public Employee ProcessedBy { get; set; } = null!;

        // Order items linked to the order
        public ICollection<OrderItem> OrderItems { get; set; } = [];

        // Payment type (Cash, Credit, etc.)
        [Required]
        [Column("order_payment_type", Order = 5, TypeName = "nvarchar(25)")]
        public string? PaymentType { get; set; }

        // Price sum of order items
        [Column("order_item_total", Order = 6, TypeName = "decimal(18,2)")]
        public decimal SumOfItems { get; set; } = 0.00m;

        // Discount percent and calculated amount
        [Required]
        [Column("order_discount_percent", Order = 8, TypeName = "tinyint")]
        public int DiscountPercent { get; set; } = 0;
        [Required]
        [Column("order_discount_amount", Order = 7, TypeName = "decimal(18,2)")]
        public decimal DiscountAmount { get; set; } = 0.00m;

        // Total order amount
        [Required]
        [Column("order_total_amount", Order = 4, TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; } = 0.00m;

        // Order notes
        [Column("order_notes", Order = 11, TypeName = "nvarchar(max)")]
        public string? Notes { get; set; }

        // Planned pickup date
        [Column("order_pickup", Order = 10)]
        public DateTime? Pickup { get; set; }

        // Date Pickedup
        [Column("order_pickedup", Order = 9)]
        public DateTime? PickedUp { get; set; }

        // Date Processed
        [Column("order_processed", Order = 8)]
        public DateTime? Processed { get; set; }


        // Date strings
        public string PickupDate
        {
            get
            {
                if (Pickup is null)
                {
                    return "None";
                }
                else
                {
                    return Pickup.ToString()!;
                }
            }
        }

        public string PickedUpDate
        {
            get
            {
                if (PickedUp is null)
                {
                    return "None";
                }
                else
                {
                    return PickedUp.ToString()!;
                }
            }
        }

        public string DateProcessed
        {
            get
            {
                if (Processed is null)
                {
                    return "None";
                }
                else
                {
                    return Processed.ToString()!;
                }
            }
        }
    }
}
