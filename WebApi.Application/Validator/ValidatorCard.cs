using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using WebApi.Application.Command;

namespace WebApi.Application.Validator
{
    public class ValidatorCard : AbstractValidator<CardRequest>
    {
        public ValidatorCard()
        {
            RuleFor(c => c.CardNumber)
                .NotNull()
                .NotEmpty()
                .WithMessage("Card Number is required");

            RuleFor(c => c.CardNumber)
                .LessThanOrEqualTo(9999999999999999)
                .WithMessage("Card Number Invalid, length is up to 16 characters");

            RuleFor(c => c.Cvv)
                .LessThanOrEqualTo(99999)
                .WithMessage("CVV Invalid...");

            RuleFor(c => c.CustomerId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Customer ID is required");
        }
    }
}
