using FluentValidation;

namespace SurveyBasket.Contract.Questions
{
    public class QuestionRequestValidator : AbstractValidator<QuestionRequest>
    {
        public QuestionRequestValidator()
        {
            RuleFor(x=> x.Content)
                .NotEmpty().WithMessage("Content is required.")
                .Length(3,1000).WithMessage("Content must be between 3 and 1000 characters.");


            RuleFor(x=> x.Answers)
                .NotEmpty().WithMessage("Answers is required.");

            RuleFor(x => x.Answers)
                .Must(x => x.Count > 1).WithMessage("Question Should has at least 2 Answers")
                .When(x => x.Answers != null);



            RuleFor(x => x.Answers)
                .Must(x => x.Distinct().Count() == x.Count()).WithMessage("You Cann`t Add Duplicated Answer for Same Question")
                .When(x => x.Answers != null);
                
        }
    }
}
