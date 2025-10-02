
using Microsoft.AspNetCore.Identity;
using SurveyBasket.Contract.Authentication.JWT;
using SurveyBasket.Entities;

namespace SurveyBasket.Services.Authentication
{
    public class AuthService(UserManager<ApplicationUser> userManager, IJwtProvider jwtProvider) : IAuthService
    {
        private readonly UserManager<ApplicationUser> UserManager = userManager;
        private readonly IJwtProvider _JwtProvider = jwtProvider;

        public async Task<AuthResponse?> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default)
        {
            //check Email is Vaild
           var IsVaildUser =  await UserManager.FindByEmailAsync(email);
            if (IsVaildUser == null)
                return null;

           var isVaildRequest= await UserManager.CheckPasswordAsync(IsVaildUser, password);

            if(!isVaildRequest)
                return null;

            //Generate Token
            var (Token, ExpiresIn) = _JwtProvider.GenerateToken(IsVaildUser);

            return new AuthResponse(
                Guid.NewGuid().ToString(),
                IsVaildUser.Email,
                IsVaildUser.FristName,
                IsVaildUser.LastName,
                Token,
                ExpiresIn
                  );


        }
    }
}
