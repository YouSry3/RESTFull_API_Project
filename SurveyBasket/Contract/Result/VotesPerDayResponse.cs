namespace SurveyBasket.Contract.Result
{
    public record VotesPerDayResponse(
        DateOnly Date,
        int NumberofVotes
        );
}
