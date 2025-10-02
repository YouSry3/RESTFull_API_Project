namespace SurveyBasket.Contract.Authentication
{
    public record AuthRequest(
        string Email,
        string Password
        );
}
