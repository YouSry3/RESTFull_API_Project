namespace SurveyBasket.Errors
{
    public class QuestionErrors
    {
        public static Error QuestionNotFound(int questionId) =>
            new(
                "QuestionNotFound",
                $"Question with ID '{questionId}' was not found."
            );
        public static Error DuplicateQuestionContent() =>
            new(
                "DuplicateQuestionContent",
                $"A question with the content already exists in this poll."
            );
   
    }
}
