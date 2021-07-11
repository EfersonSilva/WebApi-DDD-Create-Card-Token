using System;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<CardApplication> _logger;

        public CardApplication(IValidator<CardRequest> validator, ICardRepository cardRepository, ICreateToken createToken, ILogger<CardApplication> logger)
        {
            _validator = validator;
            _cardRepository = cardRepository;
            _createToken = createToken;
            _logger = logger;
        }

        public async Task<CardResponse> SaveCardAsync(CardRequest cardRequest)
        {
            var validator = await _validator.ValidateAsync(cardRequest);

            _logger.LogInformation($"Card: {cardRequest.CardNumber}, valided...");

            if (validator.IsValid != true)
            {
                _logger.LogError($"Card not valided: {validator.Errors}");
                throw new Exception($"{validator.Errors}");
            }

            var card = new Card(cardRequest.CardNumber, cardRequest.CustomerId);

            await _cardRepository.InsertCardAsync(card);

            _logger.LogInformation("Creating token...");
            var token = _createToken.CreatTokenAsync(new TokenCreate(cardRequest.CardNumber, cardRequest.Cvv));

            return new CardResponse(card.CardId, token);
        }
    }
}
