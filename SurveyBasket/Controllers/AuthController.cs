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
            var response = await AuthService.GetTokenAsync(request.Email, request.Password, cancellationToken);
            
            return response == null?
                BadRequest("Invalid 'Email' or 'Password'") 
                :Ok(response);

        }
         
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshAsync([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var AuthResult = await AuthService.GetRefreshTokenAsync(request.Token, request.RefreshToken, cancellationToken);

            return AuthResult == null ?
                BadRequest("Invalid 'Token'")
                : Ok(AuthResult);

        }

        [HttpPut("revoke-refresh-token")]
        public async Task<IActionResult> RevokedRefreshAsync([FromBody]RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var IsRevoked = await AuthService.GetRevokedRefreshTokenAsync(request.Token, request.RefreshToken, cancellationToken);

            return IsRevoked ?
                Ok("Revoked Success ^ _ ^") :
                BadRequest("Operation 'Failed'")
                ;

        }
        //[HttpGet("Test")]
        //public IActionResult Test()
        //{
        //    return Ok(JwtOptions.Key);
        //}

    }
}
