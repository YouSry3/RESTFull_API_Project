﻿namespace SurveyBasket.Contract.Authentication.RefreshToken
{
    public class RefreshTokenValidator : AbstractValidator<RefreshTokenRequest>
    {
        public RefreshTokenValidator()
        {
            RuleFor(x => x.Token)
                .NotEmpty();
            RuleFor(x => x.RefreshToken)
                .NotEmpty();



        }


    }
}
