using System;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Serilog;
using WebApi.Application.CreateToken;
using WebApi.Application.Interfaces;
using WebApi.Application.Validator;
using WebApi.Domain.Entity;

namespace WebApi.Application.Command
{
    public class CardApplication : ICardApplication
    {
        private readonly IValidator<CardRequest> _validator;
        private readonly ICardRepository _cardRepository;
        private readonly ICreateToken _createToken;

        public CardApplication(IValidator<CardRequest> validator, ICardRepository cardRepository, ICreateToken createToken)
        {
            _validator = validator;
            _cardRepository = cardRepository;
            _createToken = createToken;
        }

        public async Task<CardResponse> SaveCardAsync(CardRequest cardRequest)
        {
            var validator = await _validator.ValidateAsync(cardRequest);
            ValidedType(cardRequest.CustomerId, cardRequest.CardNumber, cardRequest.Cvv);

            Log.Information($"Card: {cardRequest.CardNumber}, valided...");

            if (validator.IsValid != true)
            {
                Log.Error($"Card not valided: {validator.Errors}");
                throw new Exception($"{validator.Errors}");
            }

            var card = new Card(cardRequest.CardNumber, cardRequest.CustomerId);

            await _cardRepository.InsertCardAsync(card);

            Log.Information("Creating token...");
            var token = _createToken.CreatTokenAsync(new CreateTokenRequest(cardRequest.CardNumber, cardRequest.Cvv));

            return new CardResponse(card.CardId, token);
        }

        public bool ValidedType(int customerId, long numberCard, int cvv)
        {
            if (customerId.GetType() != typeof(int) && numberCard.GetType() != typeof(long) && cvv.GetType() != typeof(int))
            {
                Log.Information("Type Invalided of card.");
                throw new Exception("Type Invalided of your card.");
            }

            return true;
        }
    }
}
