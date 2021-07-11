using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using WebApi.Application.CreateToken;

namespace WebApi.Application.Validator
{
    public class ValidatorToken: AbstractValidator<CheckTokenRequest>
    {
        public ValidatorToken()
        {
            RuleFor(c => c.CardId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Card ID is required");

            RuleFor(c => c.CustomerId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Customer ID is required");

            RuleFor(c => c.Cvv)
                .LessThanOrEqualTo(99999)
                .WithMessage("CVV length is up to 5 characters");

            RuleFor(c => c.Token)
                .NotNull()
                .NotEmpty()
                .WithMessage("Token is required");
        }
    }
}
