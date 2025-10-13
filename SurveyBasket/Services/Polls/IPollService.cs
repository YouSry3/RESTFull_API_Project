namespace SurveyBasket.Services.Polls
{
    public interface IPollService
    {
        Task<List<Poll>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Result<PollResponse>> GetAsync(int id, CancellationToken cancellationToken = default);
        Task<Result<PollResponse>> AddAsync(PollRequest newPoll, CancellationToken cancellationToken=default);
        Task<Result> UpdateAsync(int id, PollRequest updatedPoll , CancellationToken cancellationToken = default);
        Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<Result> TogglePublishAsync(int id, CancellationToken cancellationToken = default);
    }
}
