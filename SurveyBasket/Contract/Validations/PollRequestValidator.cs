

namespace ProjectRESTFullApi.Contract.Validations
{
    public class PollRequestValidator : AbstractValidator<PollRequest>
    {
        public PollRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .Length(3, 100);
            RuleFor(x => x.Description)
                .NotEmpty()
                .Length(3, 1500);
            RuleFor(x => x.StartAt)
                .NotEmpty()
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now));

            RuleFor(x => x)
                .Must(HasVaildDate)
                .WithName(nameof(PollRequest.EndsAt))
                .WithMessage("{PropertyName} must be greater than or equal to StartAt");


        }
        private bool HasVaildDate(PollRequest pollRequest)
        {
          
            return pollRequest.EndsAt >= pollRequest.StartAt;
        }
    
    }
}
