using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi.Application.CreateToken
{
    public class CheckTokenRequest
    {
        public int CustomerId { get; set; }
        public int CardId { get; set; }
        public long Token { get; set; }
        public int Cvv { get; set; }

        public CheckTokenRequest()
        {
        }

        public CheckTokenRequest(int customerId, int cardId, long token, int cvv)
        {
            CustomerId = customerId;
            CardId = cardId;
            Token = token;
            Cvv = cvv;
        }
    }
}
