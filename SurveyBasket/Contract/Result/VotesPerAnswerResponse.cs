namespace SurveyBasket.Contract.Result
{
    public record VotesPerAnswerResponse(
        string Answer,
        int Count
    );
}
