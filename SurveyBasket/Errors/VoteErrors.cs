namespace SurveyBasket.Errors
{
    public class VoteErrors
    {
    //    public static Error QuestionNotFound(int questionId) =>
    //new(
    //    "QuestionNotFound",
    //    $"Question with ID '{questionId}' was not found."
    //);
        public static Error DuplicateVotes() =>
            new(
                "Vote.DuplicateVotes",
                $"This user already voted before for this poll."
            );
        public static Error InvalidQuestionsInVote() =>
            new(
                "Vote.InvalidQuestionsInVote",
                $"Invalid Questions."
            );
    }
}
