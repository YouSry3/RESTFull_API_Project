namespace SurveyBasket.Contract.Result
{
    public record PollVoteResponse(
        string Title,
        IEnumerable<VoteResponse> Votes
        );
}
