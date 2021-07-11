using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApi.Application.Interfaces;
using WebApi.Domain.Entity;

namespace WebApi.Infra.Repository
{
    public class CardRepository : ICardRepository
    {
        private readonly WebApi.Infra.Context.Context _context;
        private readonly ILogger<CardRepository> _logger;

        public CardRepository(Context.Context context, ILogger<CardRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task InsertCardAsync(Card card)
        {
            try
            {
                _context.Card.Add(card);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Card: {card.CardNumber}, Saved...");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error to save card: " + ex.Message);
                throw new Exception("Error to insert Card: " + ex.Message);
            }
        }

        public async Task<Card> FindCardAsynbc(int id)
        {
            try
            {
                StringBuilder query = new StringBuilder();
                query.Append("Select CardId, CardNumber, CustomerId, CreationAt From [dbo].[Card] where CardId == @id");

                var card = await _context.Card.FromSqlRaw(query.ToString(), id).FirstOrDefaultAsync();

                if (card.Excluded)
                {
                    _logger.LogWarning($"Card {card.CardNumber} Excluded...");
                    throw new Exception("Card Excluded...");
                }
                else if (card == null)
                {
                    _logger.LogInformation("Card not found...");
                    throw new Exception("card not found...");
                }
                else
                {
                    _logger.LogInformation($"Found Card: {card.CardNumber}");
                    return card;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error to find card: " + ex.Message);
                throw new Exception("Error to find Card: " + ex.Message);
            }
        }
    }
}
