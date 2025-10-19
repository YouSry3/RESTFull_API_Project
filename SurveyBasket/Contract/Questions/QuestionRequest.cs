namespace SurveyBasket.Contract.Questions
{
    public record QuestionRequest(
        string Content,
        List<string> Answers
        );
   
}
