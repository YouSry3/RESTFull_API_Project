

namespace SurveyBasket.Abstractions
{
    public static class ResultExtensions
    {
        public static ObjectResult ToProblem(this Result result, int StatusCode) {

            // Ensure the result is a failure
            if (result.IsSuccess)
                throw new Exception("Cannot convert a success result to a ProblemDetails response.");


            var problem = Results.Problem(statusCode: StatusCode);

            var problemDetails = problem.GetType().GetProperty(nameof(ProblemDetails))!.GetValue(problem) as ProblemDetails;
            problemDetails!.Extensions = new Dictionary<string, object?>
            {
                {
                       "errors", new []{result.Error }
                }
            };
            return new ObjectResult(problemDetails);

        }
    }
}
