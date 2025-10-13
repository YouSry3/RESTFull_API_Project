
namespace SurveyBasket.Services.Authentication
{
    public interface IAuthService
    {
        Task<Result<AuthResponse>> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default);
        Task<AuthResponse?> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default);
        Task<bool> GetRevokedRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default);

    }
}
