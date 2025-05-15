using BakeryManagementPOSWebApp.Data.Enities.Abstractions;

namespace BakeryManagementPOSWebApp.Data.Enities
{
    public class Order : Entity
    {
        public int OrderedByID { get; set; }
        public Customer? OrderedBy { get; set; }

        public int? ProcessedByID { get; set; }
        public Employee? ProcessedBy { get; set; }

        public ICollection<OrderItem>? OrderItems { get; set; } = [];

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
                return Id.ToString() + "-" + OrderedByID.ToString();
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
