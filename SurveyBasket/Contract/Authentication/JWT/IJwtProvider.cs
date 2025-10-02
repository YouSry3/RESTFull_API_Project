namespace SurveyBasket.Contract.Authentication.JWT
{
    public interface IJwtProvider
    {
       public (String Token, int ExpiresIn) GenerateToken(ApplicationUser user);
    }
}
