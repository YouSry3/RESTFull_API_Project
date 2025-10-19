namespace SurveyBasket.Entities
{
    public class Poll : AuditEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsPublished { get; set; }
        public DateOnly StartAt { get; set; }
        public DateOnly EndsAt { get; set; }


        public ICollection<Question> Questions { get; set; } = [];


    }
}
