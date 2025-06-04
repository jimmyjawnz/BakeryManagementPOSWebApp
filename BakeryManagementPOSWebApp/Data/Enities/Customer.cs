using BakeryManagementPOSWebApp.Data.Enities.Abstractions;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BakeryManagementPOSWebApp.Data.Enities
{
    [Table("Customers")]
    public class Customer : Entity
    {

        [Column("customer_first_name", Order = 4, TypeName = "nvarchar(50)")]
        public string? FirstName { get; set; }

        [Column("customer_last_name", Order = 5, TypeName = "nvarchar(50)")]
        public string? LastName { get; set; }

        [Phone]
        [Required]
        [Column("customer_phone_number", Order = 2, TypeName = "nvarchar(15)")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Column("customer_fullname", Order = 3, TypeName = "nvarchar(101)")]
        public string FullName { get; set; } = string.Empty;

        // Relational value for Employee
        [Column("customer_employee", Order = 8)]
        public string EmployeeId { get; set; } = string.Empty;
        public Employee Employee { get; set; } = null!;

        [EmailAddress]
        [Column("customer_email", Order = 6, TypeName = "nvarchar(101)")]
        public string? EmailAddress { get; set; }

        [Column("customer_phone_fullname", Order = 7, TypeName = "nvarchar(125)")]
        public string PhoneAndFullName { get; set; } = string.Empty;

    }
}
