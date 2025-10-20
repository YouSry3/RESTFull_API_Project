namespace SurveyBasket.Contract.Vote
{
    public record VoteAnswerRequest(
        int QuestionId,
        int AnswerId
        );

}
