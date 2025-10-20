namespace SurveyBasket.Contract.Vote
{
    public record VoteRequest(
        IEnumerable<VoteAnswerRequest> Answers
        );
}
