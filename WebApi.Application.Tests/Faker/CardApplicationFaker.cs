using System;
using System.Collections.Generic;
using System.Text;
using Bogus;
using Bogus.DataSets;
using WebApi.Application.Command;

namespace WebApi.Tests.Faker
{
    public static class CardApplicationFaker
    {
        public static Faker<CardRequest>
            CardRequest() =>
            new Faker<CardRequest>().Rules((x, y) =>
            {
                var cardNumber = x.Finance.CreditCardNumber(CardType.Mastercard)
                .Replace(" ", "").Replace("-", "");

                y.CardNumber = long.Parse(cardNumber);
                y.CustomerId = x.Random.Int();
                y.Cvv = int.Parse(x.Finance.CreditCardCvv());
            });
    }
}
