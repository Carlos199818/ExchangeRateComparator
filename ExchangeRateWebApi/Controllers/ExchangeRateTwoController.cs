using ExchangeRateWebApi.Mock;
using ExchangeRateWebApi.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ExchangeRateWebApi.Controllers
{
    [ApiController]
    [Route("api/exchange-two")]
    [Consumes("application/xml")]
    [Produces("application/xml")]
    public class ExchangeRateTwoController : ControllerBase
    {
        private readonly ILogger<ExchangeRateTwoController> _logger;

        public ExchangeRateTwoController(ILogger<ExchangeRateTwoController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Post([FromBody] ExchangeRateXmlDTO exchangeRateXml)
        {
            _logger.LogInformation("*****ExchangeRateTwoController***** Recibida solicitud: {Source} -> {Target}, monto: {Amount}",
                exchangeRateXml.SourceCurrency, exchangeRateXml.TargetCurrency, exchangeRateXml.Amount);

            var data = new ExchangeRateDTO()
            {
                SourceCurrency = exchangeRateXml.SourceCurrency,
                TargetCurrency = exchangeRateXml.TargetCurrency,
                Amount = exchangeRateXml.Amount,
            };

            var rate = ExchangeRateMockTwo.GetRateTwo(data);

            if (rate == null)
            {
                return BadRequest("Disculpa, aun no tenemos esas divisas en nuestro sistema :( ");
            }

            return Ok(new ReturnExchangeRateXmlDTO { Result = rate.Value });
        }
    }
}
