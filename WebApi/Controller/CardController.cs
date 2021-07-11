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

        public CardController(ICardApplication cardApplication)
        {
            _cardApplication = cardApplication;
        }

        [HttpPost]
        public async Task<IActionResult> SaveCard([FromBody] CardRequest cardRequest)
        {
            try
            {
                Log.Information($"Saving card: {cardRequest.CardNumber}.");
                CardResponse returnCard = await _cardApplication.SaveCardAsync(cardRequest);

                return Ok(returnCard);
            }
            catch (Exception ex)
            {
                Log.Error("Error to Save Card..." + ex.Message);

                return new StatusCodeResult(500);
            }
        }
    }
}
