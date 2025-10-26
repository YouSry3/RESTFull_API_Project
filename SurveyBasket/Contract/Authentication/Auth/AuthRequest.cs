namespace SurveyBasket.Contract.Authentication.Auth
{
    public record AuthRequest(
        string Email,
        string Password
        );
}
