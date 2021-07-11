using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi.Domain.Entity
{
    public class Card
    {
        public int CardId { get; set; }
        public long CardNumber { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreationAt { get; set; }
        public bool Excluded { get; set; }

        public Card()
        {
        }

        public Card(long cardNumber, int customerId)
        {
            CardId = new Random().Next();
            CardNumber = cardNumber;
            CustomerId = customerId;
            CreationAt = DateTime.Now;
        }
    }
}
