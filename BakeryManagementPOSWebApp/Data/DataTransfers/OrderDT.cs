using BakeryManagementPOSWebApp.Data.Enities.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace BakeryManagementPOSWebApp.Data.Enities
{

    public class OrderDT
    {
        public int? ID { get; set; }

        // Customer who ordered
        public int? CustomerID { get; set; }
        public Customer? OrderedBy { get; set; }

        // Employee who processed
        public string? EmployeeID { get; set; }
        public Employee? ProcessedBy { get; set; }

        // Order items linked to the order
        public ICollection<OrderItem> OrderItems { get; set; } = [];

        // Payment type (Cash, Credit, etc.)
        public string? PaymentType { get; set; }

        // Price sum of order items
        public decimal? SumOfItems { get; set; } = 0.00m;

        // Discount percent and calculated amount
        public int? DiscountPercent { get; set; } = 0;
        public decimal? DiscountAmount { get; set; } = 0.00m;

        // Total order amount
        public decimal? TotalAmount { get; set; } = 0.00m;

        // Order notes
        public string? Notes { get; set; }

        // Planned pickup date
        public DateTime? Pickup { get; set; }

        // Date Pickedup
        public DateTime? PickedUp { get; set; }

        // Date Processed
        public DateTime? Processed { get; set; }

        // Creates a string with "ID-CustomerID"
        public string OrderIdentifier
        {
            get
            {
                if(CustomerID is null)
                    return string.Empty;

                if (ID is null)
                    return string.Empty;

                return ID.Value.ToString("00000000") + "-" + CustomerID.Value.ToString("0000");
            }
        }

        // Returns true if the order is processed and picked up,
        // false if not proccessed and picked up, and null otherwise
        public Status IsComplete
        {
            get
            {
                if (Processed is not null && PickedUp is not null)
                {
                    return Status.Complete;
                }
                else if (Processed is null && PickedUp is null)
                {
                    if (Pickup < DateTime.Now)
                    {
                        return Status.PassedPickupDate;
                    }

                    return Status.WaitingPickup;
                }
                else if (Processed is not null && PickedUp is null)
                {
                    return Status.WaitingPickup;
                }
                else if (Processed is null && PickedUp is not null)
                {
                    return Status.NeedsProcessing;
                }
                else
                {
                    return Status.Unknown;
                }
            }
        }

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

        // Functions
        public decimal CalcTotalAmount()
        {
            TotalAmount = CalcItemsAmount() - CalcDiscountAmount();
            return TotalAmount.Value;
        }

        public decimal CalcDiscountAmount()
        {
            DiscountAmount = CalcItemsAmount() * (DiscountPercent / 100.0m);
            return DiscountAmount.Value;
        }

        public decimal CalcItemsAmount()
        {
            SumOfItems = 0.00m;

            if (OrderItems is null || OrderItems.Count <= 0)
            {
                return SumOfItems.Value;
            }

            foreach (var item in OrderItems)
            {
                SumOfItems += item.RowPrice;
            }
            
            return SumOfItems.Value;
        }
    }
}
