namespace SurveyBasket.Contract.Responses
{
	public record PollResponse(
		int Id,
		string Title,
		 string Description,
		 bool IsPublished,
		    DateOnly StartAt,
			DateOnly EndsAt

        );
};