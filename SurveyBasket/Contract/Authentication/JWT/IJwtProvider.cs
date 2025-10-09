using Microsoft.Identity.Client;

namespace SurveyBasket.Contract.Authentication.JWT
{
    public interface IJwtProvider
    {
        (String Token, int ExpiresIn) GenerateToken(ApplicationUser user);
        String? ValidateToken(string token);

    }
}
