using Microsoft.EntityFrameworkCore;
using SurveyBasket.Contract.Vote;

namespace SurveyBasket.Services.Votes
{
    public class VoteService(AppDbContext context) : IVoteService
    {
        private readonly AppDbContext _context = context;

        public async Task<Result> AddAsync(int pollId, string userId, VoteRequest request, CancellationToken cancellationToken)
        {
            var hasVote = await _context.Votes
             .AnyAsync(v => v.pollId == pollId
             && v.userId == userId
             , cancellationToken);

            if (hasVote)
                return Result.Failure(VoteErrors.DuplicateVotes());

            var pollIsExists = await _context.Polls
              .AnyAsync(x => x.Id == pollId
              && x.IsPublished
              && x.StartAt <= DateOnly.FromDateTime(DateTime.UtcNow)
              && x.EndsAt >= DateOnly.FromDateTime(DateTime.UtcNow)
              , cancellationToken);

            if (!pollIsExists)
                return Result.Failure(PollErrors.PollNotFound(pollId));

            var availableQuestions = await _context.Questions
                .Where(q => q.PollId == pollId && q.IsActive)
                .Select(q => q.Id)
                .ToListAsync(cancellationToken);

            if (!request.Answers.Select(x => x.QuestionId).SequenceEqual(availableQuestions))
                return Result.Failure(VoteErrors.InvalidQuestionsInVote());

            var vote = new Vote
            {
                pollId = pollId,
                userId = userId,
                VoteAnswers = request.Answers
                     .Select(a => new VoteAnswer
                     {
                         questionId = a.QuestionId,
                         answerId = a.AnswerId
                     })
                     .ToList()
                        };
            await _context.Votes.AddAsync(vote, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();

        }
    }
}
