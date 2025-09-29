
namespace SurveyBasket.Services
{
    public  class PollService : IPollService
    {
        public static List<Poll> Polls = [
            new Poll { Id = 1, Title = "Poll 1", Description = "Description 1" },

			];

        public Poll Get(int id) => Polls.SingleOrDefault(x => x.Id == id);


        public List<Poll> GetAll() => Polls;

        public Poll Add(Poll newPoll)
        {
           newPoll.Id = Polls.Max(p => p.Id) + 1;
            Polls.Add(newPoll);
            return newPoll;

		}

        public bool Update(int id, Poll updatedPoll)
        {
            var IsExitPoll = Polls.SingleOrDefault(x => x.Id == id);
            if(IsExitPoll == null) return false;
            IsExitPoll.Title = updatedPoll.Title;
            return true;
		}

        public bool Delete(int id)
        {
            var isExitPolll = Polls.SingleOrDefault(x => x.Id == id);
            if(isExitPolll == null)
                return false;
            Polls.Remove(isExitPolll);
            return true;
       
        }
    }
}
