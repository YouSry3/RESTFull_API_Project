using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SurveyBasket.Contract.Authentication.JWT;
using SurveyBasket.Contract.Authentication.RefreshToken;

namespace SurveyBasket.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService,
        IOptions<JwtOptions> jwtOptions
        ) : ControllerBase
        {
        private readonly IAuthService AuthService = authService;
        private readonly JwtOptions JwtOptions = jwtOptions.Value;

        [HttpPost("")]
        public async Task<IActionResult> LoginAsync([FromBody] AuthRequest request, CancellationToken cancellationToken)
        {
            var Result = await AuthService.GetTokenAsync(request.Email, request.Password, cancellationToken);

            return Result.IsSuccess ?
                Ok(Result.Value) :
                BadRequest(Result.Error);

        }
         
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshAsync([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var Result = await AuthService.GetRefreshTokenAsync(request.Token, request.RefreshToken, cancellationToken);

            return Result.IsSuccess ?
                Ok(Result.Value) :
                BadRequest(Result.Error);

        }

        [HttpPut("revoke-refresh-token")]
        public async Task<IActionResult> RevokedRefreshAsync([FromBody]RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var Result = await AuthService.GetRevokedRefreshTokenAsync(request.Token, request.RefreshToken, cancellationToken);

            return Result.IsSuccess ?
                Ok("Revoked Success ^ _ ^") :
                BadRequest(Result.Error)
                ;

        }
    

    }
}
