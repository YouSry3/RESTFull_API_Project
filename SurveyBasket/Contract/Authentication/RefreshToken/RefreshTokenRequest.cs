namespace SurveyBasket.Contract.Authentication.RefreshToken
{
    public record RefreshTokenRequest(
        string Token,
        string RefreshToken
        );
   
}
