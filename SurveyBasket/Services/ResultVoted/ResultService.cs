using SurveyBasket.Contract.Result;

namespace SurveyBasket.Services.ResultVoted
{
    public class ResultService(AppDbContext context) : IResultService
    {
        private readonly AppDbContext _context = context;

        public async Task<Result<PollVoteResponse>> GetPollVotesAsync(int pollId, CancellationToken cancellationToken = default)
        {
            var pollVotes = await _context.Polls
                .Where(p => p.Id == pollId)
                .Select(
                x=> new PollVoteResponse(x.Title,

                                    x.Votes.Select(
                                        v=> new VoteResponse(
                                            $"{v.User.FristName} {v.User.LastName}",
                                            v.SubmittedOn,

                                    v.VoteAnswers.Select(
                                        va=> new QuestionAnswerResponse(
                                            va.Question.Content,
                                            va.Answer.Content
                                                 ))
                                     ))
                     )).SingleOrDefaultAsync(cancellationToken);

              return pollVotes is null?
                  Result.Failure<PollVoteResponse>(PollErrors.PollNotFound(pollId))
                : Result.Success<PollVoteResponse>(pollVotes);
        }


        public async Task<Result<IEnumerable<VotesPerDayResponse>>> GetVotesPerDayAsync(int pollId, CancellationToken cancellationToken = default)
        {
             var pollIsExit = await _context.Polls
                .AnyAsync(p => p.Id == pollId, cancellationToken);
             
                if (!pollIsExit)
                    return Result.Failure<IEnumerable<VotesPerDayResponse>>(PollErrors.PollNotFound(pollId));

                var votesPerDay = await _context.Votes
                    .Where(x => x.pollId == pollId)
                         .GroupBy(x =>new { Data = DateOnly.FromDateTime(x.SubmittedOn)})
                            .Select(g => new VotesPerDayResponse(
                                g.Key.Data,
                                g.Count()
                            ))
                    .ToListAsync(cancellationToken);

            return Result.Success<IEnumerable<VotesPerDayResponse>>(votesPerDay);

        }

        public async Task<Result<IEnumerable<VotesPerQuestionResponse>>> GetVotesPerQuestionAsync(int pollId, CancellationToken cancellationToken = default)
        {
            var pollIsExit = await _context.Polls
               .AnyAsync(p => p.Id == pollId, cancellationToken);

            if (!pollIsExit)
                return Result.Failure<IEnumerable<VotesPerQuestionResponse>>(PollErrors.PollNotFound(pollId));

           var votesPerQuestion = await _context.VoteAnswers
                .Where(x=> x.Vote.pollId == pollId)
                    .Select(x=> new VotesPerQuestionResponse(
                        x.Question.Content,
                   x.Question.Votes
                                    .GroupBy(x => new {AnswerId = x.Answer.Id, AnswerContent = x.Answer.Content})
                                    .Select(g=> new VotesPerAnswerResponse(
                                           g.Key.AnswerContent,
                                           g.Count()
                                        ))
                        ))
                    .ToListAsync(cancellationToken);

            return Result.Success<IEnumerable<VotesPerQuestionResponse>>(votesPerQuestion);

        }


    }
}
