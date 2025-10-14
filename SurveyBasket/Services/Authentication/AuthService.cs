
using Microsoft.AspNetCore.Identity;
using SurveyBasket.Contract.Authentication.JWT;
using SurveyBasket.Entities;
using SurveyBasket.Errors;
using System.Security.Cryptography;

namespace SurveyBasket.Services.Authentication
{
    public class AuthService(UserManager<ApplicationUser> userManager, IJwtProvider jwtProvider) : IAuthService
    {
        private readonly UserManager<ApplicationUser> UserManager = userManager;
        private readonly IJwtProvider _JwtProvider = jwtProvider;
        private readonly int _RefrshTokenExpiryDays = 14;


        public async Task<Result<AuthResponse>> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default)
        {
            //check Email is Vaild
           var IsVaildUser =  await UserManager.FindByEmailAsync(email);
            if (IsVaildUser == null)
                return Result.Failure<AuthResponse>(UserErrors.InValidCredentials);

           var isVaildRequest= await UserManager.CheckPasswordAsync(IsVaildUser, password);

            if(!isVaildRequest)
                return Result.Failure<AuthResponse>(UserErrors.InValidCredentials);

            //Generate Token
            var (Token, ExpiresIn) = _JwtProvider.GenerateToken(IsVaildUser);
            //Generate Refresh Token
            var RefreshToken = GenerateRefreshToken();
            var RefreshTokenExpiration = DateTime.UtcNow.AddDays(_RefrshTokenExpiryDays);

            IsVaildUser.RefreshTokens.Add(
                new RefreshToken
                {
                    Token = RefreshToken,
                    ExpiresOn = RefreshTokenExpiration
                }
                );

            await UserManager.UpdateAsync(IsVaildUser);

            var response = new AuthResponse(
                Guid.NewGuid().ToString(),
                IsVaildUser.Email!,
                IsVaildUser.FristName,
                IsVaildUser.LastName,
                Token,
                ExpiresIn,
     RefreshToken,
                RefreshTokenExpiration
                  );



            return Result.Success(response);

        }

        public async Task<Result<AuthResponse>> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
        {
            var userId = _JwtProvider.ValidateToken(token);

            if (userId is null) return Result.Failure<AuthResponse>(UserErrors.InValidToken);

            var user = await UserManager.FindByIdAsync(userId);

            if (user == null) return Result.Failure<AuthResponse>(UserErrors.InValidIdentifier);


            var userRefreshToken = user.RefreshTokens.SingleOrDefault(
                x =>
                x.Token == refreshToken && x.IsActive);

            if (userRefreshToken == null) return Result.Failure<AuthResponse>(UserErrors.UnMatchedRefreshToken);

            userRefreshToken.RevokedOn = DateTime.UtcNow;

            //Generate Token
            var (newToken, ExpiresIn) = _JwtProvider.GenerateToken(user);
            //Generate Refresh Token
            var newRefreshToken = GenerateRefreshToken();
            var RefreshTokenExpiration = DateTime.UtcNow.AddDays(_RefrshTokenExpiryDays);

            user.RefreshTokens.Add(
                new RefreshToken
                {
                    Token = newRefreshToken,
                    ExpiresOn = RefreshTokenExpiration
                }
                );

            await UserManager.UpdateAsync(user);
            var response = new AuthResponse(
                   Guid.NewGuid().ToString(),
                   user.Email!,
                   user.FristName,
                   user.LastName,
                   newToken,
                   ExpiresIn,
        newRefreshToken,
                   RefreshTokenExpiration

             );

            return Result.Success(response);


        }

        public async Task<Result> GetRevokedRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
        {
            var userId = _JwtProvider.ValidateToken(token);

            if (userId is null) return Result.Failure(UserErrors.InValidToken);

            var user = await UserManager.FindByIdAsync(userId);

            if (user == null) return Result.Failure(UserErrors.InValidIdentifier);


            var userRefreshToken = user.RefreshTokens.SingleOrDefault(
                x =>
                x.Token == refreshToken && x.IsActive);

            if (userRefreshToken == null) return Result.Failure(UserErrors.UnMatchedRefreshToken);
            
            userRefreshToken.RevokedOn = DateTime.UtcNow;

            await UserManager.UpdateAsync(user);

            return Result.Success();

        }
        private static string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }
    }
}
