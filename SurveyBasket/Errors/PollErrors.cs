namespace SurveyBasket.Errors
{
    public static class PollErrors
    {
        public static Error PollNotFound(int id) =>
            new Error("Poll.NotFound", $"The Poll with id: {id} was not found.");
    }
}
