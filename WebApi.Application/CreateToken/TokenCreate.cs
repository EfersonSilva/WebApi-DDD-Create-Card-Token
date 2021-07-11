using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi.Application.CreateToken
{
    public class TokenCreate
    {
        public long CardNumber { get; set; }
        public int Cvv { get; set; }

        public TokenCreate(long cardNumber, int cvv)
        {
            CardNumber = cardNumber;
            Cvv = cvv;
        }
    }
}
