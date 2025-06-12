using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BakeryManagementPOSWebApp.Data.Enities.Abstractions
{
    public abstract class Entity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("created")]
        public DateTime Created { get; set; } = DateTime.Now;

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
}
