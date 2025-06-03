using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BakeryManagementPOSWebApp.Data.Enities.Abstractions
{
    public abstract class Entity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Created { get; set; } = DateTime.Now;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? LastUpdated { get; set; }

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
}
