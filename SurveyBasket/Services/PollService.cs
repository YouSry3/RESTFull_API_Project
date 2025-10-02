
using SurveyBasket.Persistence;

namespace SurveyBasket.Services
{
    public  class PollService(AppDbContext context) : IPollService
    {
        private readonly AppDbContext _context = context;

        public static List<Poll> Polls = [
            new Poll { Id = 1, Title = "Poll 1", Description = "Description 1" },

			];

        public async Task<Poll?> GetAsync(int id, CancellationToken cancellationToken = default)=>
            await _context.Polls.FindAsync(id, cancellationToken);
        


        public async Task<List<Poll>?> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var Polls = await _context.Polls.AsNoTracking().ToListAsync(cancellationToken); 
            return Polls;
        }

        public async Task<Poll> AddAsync(Poll newPoll, CancellationToken cancellationToken = default)
        {
             await _context.Polls.AddAsync(newPoll, cancellationToken);
             await _context.SaveChangesAsync(cancellationToken);
            return newPoll;

		}

        public async Task<bool> UpdateAsync(int id, Poll updatedPoll, CancellationToken cancellationToken = default)
        {
            var IsExitPoll =await GetAsync(id, cancellationToken);
            
            if(IsExitPoll == null) return false;

            IsExitPoll.Title = updatedPoll.Title;
            IsExitPoll.Description = updatedPoll.Description;
            IsExitPoll.StartAt = updatedPoll.StartAt;
            IsExitPoll.EndsAt = updatedPoll.EndsAt;
            IsExitPoll.IsPublished = updatedPoll.IsPublished;


            await _context.SaveChangesAsync(cancellationToken);

            return true;
		}

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var isExitPolll = await GetAsync(id, cancellationToken);
            if(isExitPolll == null)
                return false;
             _context.Polls.Remove(isExitPolll);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
       
        }
    }
}
