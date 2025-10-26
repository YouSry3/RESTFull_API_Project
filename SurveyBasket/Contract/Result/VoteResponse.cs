namespace SurveyBasket.Contract.Result
{
    public record VoteResponse(
        string VoterName,
        DateTime VoteAt,
        IEnumerable<QuestionAnswerResponse> SelectedAnswers
        );
}
