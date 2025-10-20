using SurveyBasket.Contract.Questions;

namespace SurveyBasket.Services.Questions
{
    public interface IQuestionService
    {
        Task<Result<IEnumerable<QuestionResponse>>> GetAllAsync(int pollId, CancellationToken cancellation = default!);
        Task<Result<IEnumerable<QuestionResponse>>> GetAvaildableAsync(int pollId,string userId ,CancellationToken cancellation = default!);
        Task<Result<QuestionResponse>> GetAsync(int pollId, int questionId, CancellationToken cancellation = default!);
        Task<Result<QuestionResponse>> AddAsync(int pollId, QuestionRequest request, CancellationToken cancellation = default!);
        Task<Result> UpdateAsync(int pollId, int id, QuestionRequest request, CancellationToken cancellation = default!);
        Task<Result> ToggleStatusAsync(int pollId ,int id, CancellationToken cancellationToken = default);

    }
}
