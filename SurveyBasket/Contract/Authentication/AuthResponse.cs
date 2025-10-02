namespace SurveyBasket.Contract.Authentication
{
    public record AuthResponse(
        string Id,
        string Email,
        string FristName,
        string LastName,
        string Token,
        int ExpiresIn

        );

}
