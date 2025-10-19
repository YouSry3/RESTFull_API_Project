using SurveyBasket.Contract.Questions;

namespace SurveyBasket.Services.Questions
{
    public interface IQuestionService
    {
        Task<Result<QuestionResponse>> AddAsync(int pollId, QuestionRequest request, CancellationToken cancellation = default!);

    }
}
