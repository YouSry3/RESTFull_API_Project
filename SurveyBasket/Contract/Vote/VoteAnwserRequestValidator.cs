namespace SurveyBasket.Contract.Vote
{
    public class VoteAnwserRequestValidator : AbstractValidator<VoteAnswerRequest>
    {
        public VoteAnwserRequestValidator()
        {
            RuleFor(x => x.QuestionId)
                .GreaterThanOrEqualTo(1).WithMessage("AnswerId must be greater than Or Equal To '1'.");

            RuleFor(x => x.AnswerId)
              .GreaterThanOrEqualTo(1).WithMessage("AnswerId must be greater than Or Equal To '1'.");
        }
    }

}
