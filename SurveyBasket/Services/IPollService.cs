namespace SurveyBasket.Services
{
    public interface IPollService
    {
        List<Poll> GetAll();
        Poll Get(int id);
        Poll Add(Poll newPoll);
        bool Update(int id, Poll updatedPoll);
        bool Delete(int id);
	}
}
