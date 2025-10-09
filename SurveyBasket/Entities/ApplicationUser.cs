using Microsoft.AspNetCore.Identity;

namespace SurveyBasket.Entities
{
    public class ApplicationUser : IdentityUser
    {
        // Additional properties can be added here
        public string FristName { get; set; }  = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public List<RefreshToken> RefreshTokens { get; set; } = new();
    }
}
