using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SurveyBasket.Contract.Authentication.JWT;

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
        public async Task<IActionResult> LoginAsync(AuthRequest request, CancellationToken cancellationToken)
        {
            var response = await AuthService.GetTokenAsync(request.Email, request.Password, cancellationToken);
            
            return response == null?
                BadRequest("Invalid 'Email' or 'Password'") 
                :Ok(response);

        }

        [HttpGet("Test")]
        public IActionResult Test()
        {
            return Ok(JwtOptions.Key);
        }

    }
}
