using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi.Application.Command
{
    public class CardRequest
    {
        public int CustomerId { get; set; }
        public long CardNumber { get; set; }
        public int Cvv { get; set; }
    }
}
