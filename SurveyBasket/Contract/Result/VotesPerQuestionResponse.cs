namespace SurveyBasket.Contract.Result
{
    public record VotesPerQuestionResponse(
        string Question,
        IEnumerable<VotesPerAnswerResponse> SelectedAnswers
        );

}
