namespace SurveyBasket.Contract.Authentication.JWT
{
    public class JwtOptions
    {
        public static string SectionName = "JwtOptions";
        public string Key { get; init; } = string.Empty;
        public string Issuer { get; init; } = string.Empty;
        public string Audience { get; init; } = string.Empty;

        public int ExpiryMinutes { get; init; }


    }
}
