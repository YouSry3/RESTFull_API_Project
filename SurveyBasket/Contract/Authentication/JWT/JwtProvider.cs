



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
    }
}
