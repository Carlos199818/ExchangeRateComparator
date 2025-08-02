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
        [HttpPost]
        public IActionResult Post([FromBody] ExchangeRateXmlDTO exchangeRateXml)
        {
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
            //var result = Math.Round(rate.Value * data.Amount, 2);

            return Ok(new ReturnExchangeRateXmlDTO { Result = rate.Value });
        }
    }
}
