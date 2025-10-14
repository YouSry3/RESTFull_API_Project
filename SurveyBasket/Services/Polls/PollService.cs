


using Microsoft.AspNetCore.Http.HttpResults;

namespace SurveyBasket.Services.Polls
{
    public  class PollService(AppDbContext context) : IPollService
    {
        private readonly AppDbContext _context = context;

      

        public async Task<Result<PollResponse>> GetAsync(int id, CancellationToken cancellationToken = default)
        {
           var PollResult =  await _context.Polls.FindAsync(id, cancellationToken);

            return PollResult is not null ?
                Result.Success(PollResult.Adapt<PollResponse>()) :
                Result.Failure<PollResponse>(PollErrors.PollNotFound(id));
        }
        


        public async Task<List<Poll>> GetAllAsync(CancellationToken cancellationToken = default)=>
            await _context.Polls.AsNoTracking().ToListAsync(cancellationToken); 
        

        public async Task<Result<PollResponse>> AddAsync(PollRequest request, CancellationToken cancellationToken = default)
        {
          var addPoll = await _context.Polls.AddAsync(request.Adapt<Poll>(), cancellationToken);
             await _context.SaveChangesAsync(cancellationToken);
          return Result.Success(addPoll.Adapt<PollResponse>());
           
		}

        public async Task<Result> UpdateAsync(int id, PollRequest updatedPoll, CancellationToken cancellationToken = default)
        {
            var IsExitPoll = await _context.Polls.FindAsync(id, cancellationToken);

            if (IsExitPoll == null) return Result.Failure(PollErrors.PollNotFound(id));

            IsExitPoll.Title = updatedPoll.Title;
            IsExitPoll.Description = updatedPoll.Description;
            IsExitPoll.StartAt = updatedPoll.StartAt;
            IsExitPoll.EndsAt = updatedPoll.EndsAt;


            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }

        public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var isExitPolll = await _context.Polls.FindAsync(id, cancellationToken);
            if (isExitPolll == null)
                return Result.Failure(PollErrors.PollNotFound(id));
            _context.Polls.Remove(isExitPolll);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();

        }

        public async Task<Result> TogglePublishAsync(int id, CancellationToken cancellationToken = default)
        {

            var IsExitPoll = await _context.Polls.FindAsync(id, cancellationToken);

            if (IsExitPoll is null) return Result.Failure(PollErrors.PollNotFound(id));

            IsExitPoll.IsPublished = !IsExitPoll.IsPublished;

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
