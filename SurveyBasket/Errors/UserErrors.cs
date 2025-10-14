namespace SurveyBasket.Errors
{
    public static class UserErrors
    {
        public static Error InValidCredentials => new("User.InValidCredentials", "The user credentials(Email | Password) are invalid.");
        public static Error InValidToken => new("User.InValidToken", "The user token is invalid.");
        public static Error InValidIdentifier => new("User.InValidIdentifier", "The user identifier is invalid.");

        public static Error UnMatchedRefreshToken => new("User.UnMatchedRefreshToken", "The user refresh token is unmatched.");

    }
}
