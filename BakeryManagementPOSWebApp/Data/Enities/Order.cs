using BakeryManagementPOSWebApp.Data.Enities.Abstractions;

namespace BakeryManagementPOSWebApp.Data.Enities
{
    public class Order : Entity
    {
        public int OrderedBy { get; set; }
        public Customer? Customer { get; set; }

        public int ProcessedBy { get; set; }
        public Employee? Employee { get; set; }

        public ICollection<OrderItem>? OrderItems { get; set; } = [];

        public string? Notes { get; set; }

        public DateTime? PickupDate { get; set; }

    }
}
