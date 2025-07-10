namespace api_gestiona.Entities
{
    public class BaseEntity : IAuditable
    {
        /// <summary>
        /// The identificator of the record in the data base
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Shows the date and time when the record has been created
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Shows the date and time when the record has been changed
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// It changes with every redord update
        /// </summary>
        public long Version { get; set; }

        /// <summary>
        /// It allows the soft delete feature
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// It stores the id of the user that made the last change on the record
        /// </summary>
        public string? ModifiedBy { get; set; }

        /// <summary>
        /// It stores the id of the user that created the record.
        /// </summary>
        public string? CreatedBy { get; set; }
    }
}
