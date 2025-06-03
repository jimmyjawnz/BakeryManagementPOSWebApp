using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace BakeryManagementPOSWebApp.Data.Enities;

public class EmployeeDT
{
    [Required(ErrorMessage = "First name is required.")]
    [StringLength(50, ErrorMessage = "First name is too long.")]
    public string? FirstName { get; set; }

    [StringLength(50, ErrorMessage = "Last name is too long.")]
    public string? LastName { get; set; }

    [Phone(ErrorMessage = "Invalid phone number.")]
    [Required(ErrorMessage = "Phone number is required.")]
    public string? PhoneNumber { get; set; }
}

