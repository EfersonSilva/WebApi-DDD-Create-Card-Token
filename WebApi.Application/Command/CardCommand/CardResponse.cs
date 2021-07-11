using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi.Application.Command
{
    public class CardResponse
    {
        public int CardId { get; set; }
        public long Token { get; set; }
        public DateTime CreatedAt { get; set; }

        public CardResponse(int cardId, long token)
        {
            CardId = cardId;
            Token = token;
            CreatedAt = DateTime.Now;
        }
    }
}
