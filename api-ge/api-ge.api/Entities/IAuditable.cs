namespace api_gestiona.Entities
{
    public interface IAuditable
    {
        long Id { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
        long Version { get; set; }
        bool Active { get; set; }
        string ModifiedBy { get; set; }
        string CreatedBy { get; set; }
    }
}
