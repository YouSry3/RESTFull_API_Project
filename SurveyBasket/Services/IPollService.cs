namespace SurveyBasket.Services
{
    public interface IPollService
    {
        Task<List<Poll>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Poll> GetAsync(int id, CancellationToken cancellationToken = default);
        Task<Poll> AddAsync(Poll newPoll, CancellationToken cancellationToken=default);
        Task<bool> UpdateAsync(int id, Poll updatedPoll , CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<bool> TogglePublishAsync(int id, CancellationToken cancellationToken = default);
    }
}
