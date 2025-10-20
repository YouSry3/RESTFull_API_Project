using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using SurveyBasket.Contract.Questions;

namespace SurveyBasket.Services.Questions
{
    public class QuestionService(AppDbContext context) : IQuestionService
    {
        private readonly AppDbContext _context = context;

        public async Task<Result<QuestionResponse>> GetAsync(int pollId, int questionId, CancellationToken cancellation = default)
        {
            var question = await _context.Questions
                .Where(q => q.PollId == pollId && q.Id == questionId)
                .Include(q => q.Answers)          
                .ProjectToType<QuestionResponse>()
                .AsNoTracking()
                .SingleOrDefaultAsync(cancellation);

            if (question is null)
                return Result.Failure<QuestionResponse>(QuestionErrors.QuestionNotFound(questionId));

            return Result.Success(question);
        }
        public async Task<Result<IEnumerable<QuestionResponse>>> GetAllAsync(int pollId, CancellationToken cancellation = default)
        {
            var pollIsExists = await _context.Polls.AnyAsync(p => p.Id == pollId, cancellationToken: cancellation);

            if (!pollIsExists)
                return Result.Failure<IEnumerable<QuestionResponse>>(PollErrors.PollNotFound(pollId));

            var questions = await _context.Questions
                .Where(q => q.PollId == pollId)
                .Include(q => q.Answers)
             //   .Select(q=> new QuestionResponse(
             //       q.Id,
             //       q.Content,
             //q.Answers.Select(a=> new AnswerResponse(a.Id,a.Content))
             //       ))
                .ProjectToType<QuestionResponse>()
                .AsNoTracking()
                .ToListAsync(cancellation);

            return Result.Success<IEnumerable<QuestionResponse>>(questions);

        }

        public async Task<Result<IEnumerable<QuestionResponse>>> GetAvaildableAsync(int pollId, string userId, CancellationToken cancellation = default!)
        {
            var hasVote = await _context.Votes
                .AnyAsync(v => v.pollId == pollId 
                && v.userId == userId
                , cancellationToken: cancellation);

            if (hasVote)
                return Result.Failure<IEnumerable<QuestionResponse>>(VoteErrors.DuplicateVotes());

            var pollIsExists = await _context.Polls
                .AnyAsync(x => x.Id == pollId 
                && x.IsPublished 
                && x.StartAt <= DateOnly.FromDateTime(DateTime.UtcNow)
                && x.EndsAt >= DateOnly.FromDateTime(DateTime.UtcNow)
                , cancellationToken: cancellation);

            if (!pollIsExists)
                return Result.Failure<IEnumerable<QuestionResponse>>(PollErrors.PollNotFound(pollId));
            
            var questions = await _context.Questions
                            .Where(x => x.PollId == pollId && x.IsActive)
                            .Include(x => x.Answers)
                            .Select(x => new QuestionResponse(
                                x.Id,
                                x.Content,
                                x.Answers
                                    .Where(a => a.IsActive)
                                    .Select(a => new AnswerResponse(a.Id, a.Content))
                            )) 
                            .AsNoTracking()
                            .ToListAsync(cancellation);

            return Result.Success<IEnumerable<QuestionResponse>>(questions);


        }
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

        public async Task<Result> UpdateAsync(int pollId, int id, QuestionRequest request, CancellationToken cancellation = default)
        {
            var questionIsExits = await _context.Questions
                .AnyAsync(q =>  q.PollId == pollId
                          && q.Id != id 
                          && q.Content == request.Content  , cancellationToken: cancellation);
            if (questionIsExits)
                return Result.Failure(QuestionErrors.DuplicateQuestionContent());
            var question = await _context.Questions
                           .Include(q => q.Answers)
                           .SingleOrDefaultAsync(q => q.PollId == pollId && q.Id == id, cancellationToken: cancellation);
            if (question is null)
                return Result.Failure(QuestionErrors.QuestionNotFound(id));
            question.Content = request.Content;
            // Current Answers
            var existingAnswerContents = question.Answers.Select(a => a.Content).ToList();

            //Add new answers
            var newAnswers = request.Answers
                .Except(existingAnswerContents)
                .ToList();

            newAnswers.ForEach(answerContent => new Answer { Content = answerContent });

            question.Answers.ToList().ForEach(answer =>
            {
                answer.IsActive = request.Answers.Contains(answer.Content);
            });

            await _context.SaveChangesAsync(cancellation);
            return Result.Success();
        }

        public async Task<Result> ToggleStatusAsync(int pollId, int id, CancellationToken cancellationToken = default)
        {
            var IsExitQuestion = await _context.Questions.SingleOrDefaultAsync(x=>(x.Id == id && x.PollId == pollId ), cancellationToken);

            if (IsExitQuestion is null) return Result.Failure(QuestionErrors.QuestionNotFound(id));

            IsExitQuestion.IsActive = !IsExitQuestion.IsActive;

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
