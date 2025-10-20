
namespace SurveyBasket.Contract.Vote
{
    public class VoteRequestValidator: AbstractValidator<VoteRequest>
    {
        public VoteRequestValidator()
        {
            RuleFor(x => x.Answers)
                .NotEmpty().WithMessage("At least one answer must be provided.");


            RuleForEach(x => x.Answers)
                .SetInheritanceValidator(x =>
                x.Add(new VoteAnwserRequestValidator())
                );

        }
    }

}
