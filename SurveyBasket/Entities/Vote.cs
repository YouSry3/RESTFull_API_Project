using System.Data;

namespace SurveyBasket.Entities
{
    public sealed class Vote
    {
        public int Id { get; set; }
        public int pollId { get; set; }
        public string userId { get; set; } = string.Empty;

        public DateTime SubmittedOn { get; set; } = DateTime.UtcNow;


        public Poll Poll { get; set; } = default!;
        public ApplicationUser User { get; set; } = default!;
        public ICollection<VoteAnswer> VoteAnswers { get; set; } = [];
    }
}
