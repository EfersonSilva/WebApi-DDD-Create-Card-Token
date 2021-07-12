using System;
using System.Collections.Generic;
using System.Text;
using Bogus;
using WebApi.Application.CreateToken;

namespace WebApi.Tests.Faker
{
    public static class CreateTokenFaker
    {
        public static Faker<CreateTokenRequest> 
            CreateTokenRequest() => 
            new Faker<CreateTokenRequest>()
            .Rules((x, y) =>
        {
            y.CardNumber = x.Random.Long(16);
            y.Cvv = int.Parse(x.Finance.CreditCardCvv());
        });
    }
}
