using SurveyBasket.Contract.Questions;

namespace SurveyBasket.Services.Questions
{
    public class QuestionService(AppDbContext context) : IQuestionService
    {
        private readonly AppDbContext _context = context;

        public async Task<Result<QuestionResponse>> AddAsync(int pollId, QuestionRequest request, CancellationToken cancellation = default)
        {
            var pollIsExists = await _context.Polls.AnyAsync(p => p.Id == pollId, cancellationToken: cancellation);

            if(!pollIsExists)
                return Result.Failure<QuestionResponse>(PollErrors.PollNotFound(pollId));

            var QuestionIsExists = await _context.Questions
                .AnyAsync(q => q.PollId == pollId && q.Content == request.Content, cancellationToken: cancellation);
            if(QuestionIsExists)
                return Result.Failure<QuestionResponse>(QuestionErrors.DuplicateQuestionContent());
            var question = request.Adapt<Question>();
            question.PollId = pollId;

            request.Answers.ForEach(answer => question.Answers.Add(new Answer { Content = answer}));

            await _context.Questions.AddAsync(question, cancellation);
            await _context.SaveChangesAsync(cancellation);

            return Result.Success(question.Adapt<QuestionResponse>());

        }
    }
}
