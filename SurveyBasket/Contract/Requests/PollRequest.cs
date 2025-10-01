namespace SurveyBasket.Contract.Requests
{
    public record PollRequest(
        string Title,
        string Description,
        bool IsPublished,
        DateOnly StartAt,
        DateOnly EndsAt

        );

}
