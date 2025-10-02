namespace SurveyBasket.Contract.Polls
{
    public record PollRequest(
        string Title,
        string Description,
        bool IsPublished,
        DateOnly StartAt,
        DateOnly EndsAt

        );

}
