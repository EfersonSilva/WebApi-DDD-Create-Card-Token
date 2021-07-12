using System;
using System.Collections.Generic;
using System.Text;
using Bogus;
using WebApi.Application.CreateToken;

namespace WebApi.Tests.Faker
{
    public static class CheckTokenFaker
    {
        public static Faker<CheckTokenRequest> ChekedTokenRequest() =>
            new Faker<CheckTokenRequest>()
            .Rules((x, y) =>
            {
                y.CardId = x.Random.Int();
                y.CustomerId = x.Random.Int();
                y.Token = x.Random.Long();
                y.Cvv = int.Parse(x.Finance.CreditCardCvv());
            });
    }
}
