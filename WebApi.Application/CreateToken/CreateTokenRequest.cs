using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi.Application.CreateToken
{
    public class CreateTokenRequest
    {
        public long CardNumber { get; set; }
        public int Cvv { get; set; }

        public CreateTokenRequest()
        {
        }

        public CreateTokenRequest(long cardNumber, int cvv)
        {
            CardNumber = cardNumber;
            Cvv = cvv;
        }
    }
}
