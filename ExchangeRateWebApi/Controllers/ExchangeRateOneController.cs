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
            _logger.LogInformation("*****ExchangeRateOneController***** Recibida solicitud: {SourceCurrency} -> {TargetCurrency}, monto: {Amount}",
            exchangeRateDTO.SourceCurrency, exchangeRateDTO.TargetCurrency, exchangeRateDTO.Amount);

            if (exchangeRateDTO.SourceCurrency == exchangeRateDTO.TargetCurrency)
            {
                _logger.LogInformation("El usuario esta utilizando la misma moneda para origen y para destino");
                return BadRequest("Disculpa, tiene que utilizar monedas distintas para hacer la conversion X( ");
            }

            var rate = ExchangeRateMockOne.GetRateOne(exchangeRateDTO);

            if (rate == null)
            {
                return BadRequest("Disculpa, aun no tenemos esas divisas en nuestro sistema :( ");
            }

            return Ok(new ReturnExchangeRateDTO { rate = rate.Value });
        }
    }
}
