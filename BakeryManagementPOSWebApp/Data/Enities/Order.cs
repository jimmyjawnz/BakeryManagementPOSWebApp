using BakeryManagementPOSWebApp.Data.Enities.Abstractions;
using System.ComponentModel.DataAnnotations;
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

    public class Order : Entity
    {
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
        public decimal SumOfItems { get; set; } = 0.00m;

        // Discount percent and calculated amount
        public int DiscountPercent { get; set; } = 0;
        public decimal DiscountAmount { get; set; } = 0.00m;

        // Total order amount
        public decimal TotalAmount { get; set; } = 0.00m;

        // Order notes
        public string? Notes { get; set; }

        // Planned pickup date
        public DateTime? PickupDate { get; set; }

        // Date Pickedup
        public DateTime? DatePickedUp { get; set; }

        // Date Processed
        public DateTime? DateProcessed { get; set; }

        // Creates a string with "ID-CustomerID"
        public string OrderIdentifier
        {
            get
            {
                if(CustomerID is null)
                    return string.Empty;

                return Id.ToString("00000000") + "-" + CustomerID.Value.ToString("0000");
            }
        }

        // Returns true if the order is processed and picked up,
        // false if not proccessed and picked up, and null otherwise
        public Status IsComplete
        {
            get
            {
                if (DateProcessed is not null && DatePickedUp is not null)
                {
                    return Status.Complete;
                }
                else if (DateProcessed is null && DatePickedUp is null)
                {
                    if (PickupDate < DateTime.Now)
                    {
                        return Status.PassedPickupDate;
                    }

                    return Status.WaitingPickup;
                }
                else if (DateProcessed is not null && DatePickedUp is null)
                {
                    return Status.WaitingPickup;
                }
                else if (DateProcessed is null && DatePickedUp is not null)
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
        public string PickupDateStr
        {
            get
            {
                if (PickupDate is null)
                {
                    return "None";
                }
                else
                {
                    return PickupDate.ToString()!;
                }
            }
        }

        public string DateProcessedStr
        {
            get
            {
                if (DateProcessed is null)
                {
                    return "None";
                }
                else
                {
                    return DateProcessed.ToString()!;
                }
            }
        }

        // Functions
        public decimal CalcTotalAmount()
        {
            TotalAmount = CalcItemsAmount() - CalcDiscountAmount();
            return TotalAmount;
        }

        public decimal CalcDiscountAmount()
        {
            DiscountAmount = CalcItemsAmount() * (DiscountPercent / 100.0m);
            return DiscountAmount;
        }

        public decimal CalcItemsAmount()
        {
            SumOfItems = 0.00m;

            if (OrderItems is null || OrderItems.Count <= 0)
            {
                return SumOfItems;
            }

            foreach (var item in OrderItems)
            {
                SumOfItems += item.RowPrice;
            }
            
            return SumOfItems;
        }
    }
}
