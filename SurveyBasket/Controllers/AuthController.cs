using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SurveyBasket.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService AuthService = authService;

        [HttpPost("")]
        public async Task<IActionResult> LoginAsync(AuthRequest request, CancellationToken cancellationToken)
        {
            var response = await AuthService.GetTokenAsync(request.Email, request.Password, cancellationToken);
            
            return response == null?
                BadRequest("Invalid 'Email' or 'Password'") 
                :Ok(response);

        }
    }
}
