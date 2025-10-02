
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SurveyBasket.Contract.Authentication.JWT
{
    public class JwtProvider : IJwtProvider
    {
        public (string Token, int ExpiresIn) GenerateToken(ApplicationUser user)
        {
            Claim[] claims = [

                new (JwtRegisteredClaimNames.Sub, user.Id),
                new (JwtRegisteredClaimNames.Email, user.Email!),
                new(JwtRegisteredClaimNames.GivenName, user.FristName),
                new(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())  ];

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("Dse0xBiw003xptwazaaElvLNHQgzqU9a"));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            int expiresIn = 30;


            var token = new JwtSecurityToken(
                issuer: "SurveyBasket App",
                audience: "SurveyBasket Users",
                claims: claims,
                expires: DateTime.Now.AddMinutes(expiresIn),
                signingCredentials: creds
                );
            
            return (new JwtSecurityTokenHandler().WriteToken(token), expiresIn * 60);    

        }
    }
}
