

namespace SurveyBasket.Contract.Questions
{
    public record QuestionResponse(
        int Id,
        string Content,
        IEnumerable<AnswerResponse> Answers
        );
}
