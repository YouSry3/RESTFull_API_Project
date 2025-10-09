namespace SurveyBasket.Contract.Polls
{
    public record PollRequest(
        string Title,
        string Description,
        DateOnly StartAt,
        DateOnly EndsAt

        );

}
