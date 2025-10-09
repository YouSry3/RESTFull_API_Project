namespace SurveyBasket.Entities
{
    public class AuditEntity
    {
        public string CreatedById { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? UpdatedById { get; set; }
        public DateTime? UpdatedAt { get; set; }


        public ApplicationUser CreatedBy { get; set; } = default!;
        public ApplicationUser? UpdateBy { get; set; }
    }
}
