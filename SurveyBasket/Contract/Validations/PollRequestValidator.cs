

namespace ProjectRESTFullApi.Contract.Validations
{
    public class PollRequestValidator : AbstractValidator<PollRequest>
    {
        public PollRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .Length(5, 100);
            RuleFor(x => x.Description)
                .NotEmpty()
                .Length(10, 500);


        }
    }
}
