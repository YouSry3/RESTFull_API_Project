using SurveyBasket.Contract.Vote;

namespace SurveyBasket.Services.Votes
{
    public interface IVoteService
    {
        Task<Result> AddAsync(int pollId, string userId, VoteRequest request, CancellationToken cancellationToken);
    }
}
