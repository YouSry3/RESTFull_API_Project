namespace SurveyBasket.Entities
{
    public class Poll : AuditEntity
    {
       public int Id { get; set; }
       public string Title { get; set; }
       public string Description { get; set; }
        public bool IsPublished { get; set; }
        public DateOnly StartAt { get; set; }
        public DateOnly EndsAt { get; set; }
   
    }
}
