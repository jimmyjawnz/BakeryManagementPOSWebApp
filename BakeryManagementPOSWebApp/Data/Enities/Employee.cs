using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BakeryManagementPOSWebApp.Data.Enities;

[Table("Employees")]
public class Employee : IdentityUser
{
    [Required]
    [Column("employee_first_name", Order = 4, TypeName = "nvarchar(50)")]
    public string FirstName { get; set; } = string.Empty;

    [Column("employee_last_name", Order = 5, TypeName = "nvarchar(50)")]
    public string? LastName { get; set; }

    [Phone]
    [Required]
    [Column("employee_phone_number", Order = 2, TypeName = "nvarchar(15)")]
    public override string? PhoneNumber { get; set; }

    [Column("employee_fullname", Order = 3, TypeName = "nvarchar(101)")]
    public string FullName { get; set; } = string.Empty;

    // Relational value for Customer
    [Column("employee_customer", Order = 6)]
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;


    [Required]
    [Column("created")]
    public DateTime Created { get; set; } = DateTime.Now;

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    [Column("last_updated")]
    public DateTime? LastUpdated { get; set; }

    [Column("deleted")]
    public DateTime? Deleted { get; set; }

    public string DateCreated
    {
        get
        {
            return Created.ToString("g");
        }
    }
    public string DateUpdated
    {
        get
        {
            if (LastUpdated is null)
            {
                return "None";
            }
            else
            {
                return LastUpdated.Value.ToString("g");
            }
        }
    }
    public string DateDeleted
    {
        get
        {
            if (Deleted is null)
            {
                return "None";
            }
            else
            {
                return Deleted.Value.ToString("g");
            }
        }
    }
}

