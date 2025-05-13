using BakeryManagementPOSWebApp.Data.Enities.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace BakeryManagementPOSWebApp.Data.Enities
{
    public class Customer : Entity
    {
        [Length(1, 25, ErrorMessage = "First name is too large.")]
        public string FirstName { get; set; } = string.Empty;
        [Length(1, 25, ErrorMessage = "Last name is too large.")]
        public string LastName { get; set; } = string.Empty;
        [Phone(ErrorMessage = "Phone number is not a valid/supported number.")]
        [Required(ErrorMessage = "Phone number is required.")]
        public string PhoneNumber { get; set; } = string.Empty;
        [EmailAddress]
        public string EmailAddress { get; set; } = string.Empty;
    }
}
