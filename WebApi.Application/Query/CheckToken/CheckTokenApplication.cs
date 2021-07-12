using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Serilog;
using WebApi.Application.CreateToken;
using WebApi.Application.Interfaces;
using WebApi.Domain.Entity;

namespace WebApi.Application.Query.CheckToken
{
    public class CheckTokenApplication : ICheckTokenApplication
    {
        private readonly ICreateToken _createToken;
        private readonly ICardRepository _cardRepository;
        private readonly IValidator<CheckTokenRequest> _validator;

        public CheckTokenApplication(ICreateToken createToken, ICardRepository cardRepository, IValidator<CheckTokenRequest> validator)
        {
            _createToken = createToken;
            _cardRepository = cardRepository;
            _validator = validator;
        }

        public async Task<bool> CheckToken(CheckTokenRequest checkTokenRequest)
        {
            var validator = await _validator.ValidateAsync(checkTokenRequest);
            if (validator.IsValid != true)
            {
                Log.Error($"token not valided: {validator.Errors}");
                throw new Exception($"{validator.Errors}");
            }

            var card = await _cardRepository.FindCardAsync(checkTokenRequest.CardId);

            var timeStop = card.CreationAt + TimeSpan.FromMinutes(30);

            if (DateTime.Now < timeStop)
            {
                long token = _createToken.CreatTokenAsync(new CreateTokenRequest(card.CardNumber, checkTokenRequest.Cvv));

                return ComparerData(checkTokenRequest, card, token);
            }
            else
            {
                Log.Error($"token Expired...");
                return false;
            }
        }

        public bool ComparerData(CheckTokenRequest cardRequest, Card card, long token)
        {
            if (cardRequest.CardId == card.CardId && cardRequest.CustomerId == card.CustomerId && token == cardRequest.Token)
            {
                return true;
            }
            else
            {
                Log.Error("Error, Saved data does not correspond to entered data...");
                return false;
            }
        }
    }
}
