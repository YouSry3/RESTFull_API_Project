



namespace SurveyBasket.Contract.Authentication.JWT
{
    public class JwtProvider(IOptions<JwtOptions> jwtOptions) : IJwtProvider
    {
        private readonly JwtOptions JwtOptions = jwtOptions.Value;

        public (string Token, int ExpiresIn) GenerateToken(ApplicationUser user)
        {
            Claim[] claims = [

                new (JwtRegisteredClaimNames.Sub, user.Id),
                new (JwtRegisteredClaimNames.Email, user.Email!),
                new(JwtRegisteredClaimNames.GivenName, user.FristName),
                new(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())  ];

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(JwtOptions.Key));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            

            var token = new JwtSecurityToken(
                issuer: JwtOptions.Issuer,
                audience: JwtOptions.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(JwtOptions.ExpiryMinutes),
                signingCredentials: creds
                );
            
            return (new JwtSecurityTokenHandler().WriteToken(token), JwtOptions.ExpiryMinutes * 60);    

        }

        public string? ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(JwtOptions.Key));


            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    IssuerSigningKey = key,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero

                }, out SecurityToken validedToken);

                var jwtToken = (JwtSecurityToken)validedToken;

                return jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value;
            }
            catch
            {
                return null;
            }

        }
    }
}
