using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using WebApi.Application.CreateToken;
using WebApi.Application.Interfaces;

namespace WebApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenChecksController : ControllerBase
    {
        private readonly ICheckTokenApplication _checkTokenApplication;

        public TokenChecksController(ICheckTokenApplication checkTokenApplication)
        {
            _checkTokenApplication = checkTokenApplication;
        }
        [HttpPost]
        public async Task<IActionResult> TokenCheks([FromBody] CheckTokenRequest tokenRequest)
        {
            try
            {
                Log.Information("Validating token...");
               bool validation = await _checkTokenApplication.CheckToken(tokenRequest);

                if (validation)
                {
                    Log.Information("Token Valided. ");
                    return Ok(true);
                }
                else
                {
                    Log.Information("Token is not Valided. ");
                    return NotFound(false);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
        }
    }
}
