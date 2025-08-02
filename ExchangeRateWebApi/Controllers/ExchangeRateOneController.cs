using ExchangeRateWebApi.Mock;
using ExchangeRateWebApi.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeRateWebApi.Controllers
{
    [ApiController]
    [Route("api/exchange-one")]
    public class ExchangeRateOneController : ControllerBase
    {
        private readonly ILogger<ExchangeRateOneController> _logger;

        public ExchangeRateOneController(ILogger<ExchangeRateOneController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public ActionResult<ReturnExchangeRateDTO> Post([FromBody] ExchangeRateDTO exchangeRateDTO)
        {
            _logger.LogInformation("*****ReturnExchangeRateDTO***** Recibida solicitud: {Source} -> {Target}, monto: {Amount}",
            exchangeRateDTO.SourceCurrency, exchangeRateDTO.TargetCurrency, exchangeRateDTO.Amount);


            var rate = ExchangeRateMockOne.GetRateOne(exchangeRateDTO);

            if (rate == null)
            {
                return BadRequest("Disculpa, aun no tenemos esas divisas en nuestro sistema :( ");
            }

            //var result = rate.Value * exchangeRateDTO.Amount;

            return Ok(new ReturnExchangeRateDTO { rate = rate.Value });
        }
    }
}
