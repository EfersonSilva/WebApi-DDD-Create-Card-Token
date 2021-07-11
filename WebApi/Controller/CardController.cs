using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using WebApi.Application.Command;
using WebApi.Application.Interfaces;

namespace WebApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICardApplication _cardApplication;
        private readonly ILogger<CardController> _logger;

        public CardController(ICardApplication cardApplication, ILogger<CardController> logger)
        {
            _cardApplication = cardApplication;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> SaveCard([FromBody] CardRequest cardRequest)
        {
            try
            {
                _logger.LogInformation($"Saving card: {cardRequest.CardNumber}.");
                CardResponse returnCard = await _cardApplication.SaveCardAsync(cardRequest);

                return Ok(returnCard);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error to Save Card..." + ex.Message);

                return new StatusCodeResult(500);
            }
        }
    }
}
