
using SurveyBasket.Contract.Authentication.Auth;

namespace SurveyBasket.Services.Authentication
{
    public interface IAuthService
    {
        Task<Result<AuthResponse>> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default);
        Task<Result<AuthResponse>> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default);
        Task<Result> GetRevokedRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default);

    }
}
