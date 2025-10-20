namespace SurveyBasket.Entities
{
    public sealed class VoteAnswer
    {
        public int Id { get; set; }
        public int voteId { get; set; }
        public int questionId { get; set; }
        public int answerId { get; set; }

        public Vote Vote { get; set; } = default!;
        public Question Question { get; set; } = default!;
        public Answer Answer { get; set; } = default!;

    }
}
