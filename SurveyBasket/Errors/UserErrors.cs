namespace SurveyBasket.Errors
{
    public static class UserErrors
    {
        public static Error InValidCredentials => new("User.InValidCredentials", "The user credentials(Email | Password) are invalid.");
    }
}
