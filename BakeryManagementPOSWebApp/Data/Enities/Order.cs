using BakeryManagementPOSWebApp.Data.Enities.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BakeryManagementPOSWebApp.Data.Enities
{
    public class Order : Entity
    {
        [Required(ErrorMessage = "A valid Customer must be selected.")]
        public int? CustomerID { get; set; }
        public Customer? OrderedBy { get; set; }

        public string? EmployeeID { get; set; }
        public Employee? ProcessedBy { get; set; }

        public ICollection<OrderItem>? OrderItems { get; set; } = [];

        [Required(ErrorMessage = "Payment type is required.")]
        public string? PaymentType { get; set; }

        public string? Notes { get; set; }

        public DateTime? PickupDate { get; set; }

        public DateTime? DateProcessed { get; set; }

        public decimal TotalSum { 
            get
            {
                decimal total = 0.0m;

                if (OrderItems is null)
                {
                    return total;
                }

                foreach (OrderItem item in OrderItems)
                {
                    total += item.RowPrice;
                }

                return total;
            } 
        }

        public string OrderIdentifier
        {
            get
            {
                return Id.ToString() + "-" + CustomerID.ToString();
            }
        }

        public bool? IsComplete
        {
            get
            {
                if (DateProcessed is not null && PickupDate is not null)
                {
                    return true;
                }
                else if (DateProcessed is null && PickupDate is null)
                {
                    return false;
                }
                else
                {
                    return null;
                }
            }
        }

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
    }
}
