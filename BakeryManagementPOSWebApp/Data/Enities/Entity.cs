namespace BakeryManagementPOSWebApp.Data.Enities
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateDeleted { get; set; }

        public string DateCreatedStr
        {
            get
            {
                return DateCreated.ToString();
            }
        }
        public string DateUpdatedStr
        {
            get
            {
                if (DateUpdated is null)
                {
                    return "None";
                }
                else
                {
                    return DateUpdated.ToString()!;
                }
            }
        }
        public string DateDeletedStr
        {
            get
            {
                return DateDeleted.ToString()!;
            }
        }
    }
}
