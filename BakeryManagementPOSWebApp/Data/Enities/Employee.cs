using BakeryManagementPOSWebApp.Data.Enities.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace BakeryManagementPOSWebApp.Data.Enities;

public class Employee : IdentityUser
{
    [Required(ErrorMessage = "First name is required.")]
    [Length(1, 25, ErrorMessage = "First name is too large.")]
    public string FirstName { get; set; } = string.Empty;
    [Length(1, 25, ErrorMessage = "Last name is too large.")]
    public string LastName { get; set; } = string.Empty;
    [Phone]
    [Required(ErrorMessage = "Phone number is required.")]
    public override string? PhoneNumber { get; set; } = string.Empty;

    public string FullName
    {
        get
        {
            if (!FirstName.IsNullOrEmpty())
            {
                return FirstName + " " + LastName;
            }
            else
            {
                return "";
            }
        }
    }

    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
}

