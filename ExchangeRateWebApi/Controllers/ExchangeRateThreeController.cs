using ExchangeRateWebApi.Mock;
using ExchangeRateWebApi.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExchangeRateWebApi.Controllers
{
    [ApiController]
    [Route("api/exchange-three")]
    public class ExchangeRateThreeController : ControllerBase
    {
        private readonly ILogger<ExchangeRateThreeController> _logger;

        public ExchangeRateThreeController(ILogger<ExchangeRateThreeController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Post([FromBody] ExchangeRateNestedDTO exchangeRateNested)
        {
            _logger.LogInformation("*****ExchangeRateThreeController***** Recibida solicitud: {SourceCurrency} -> {TargetCurrency}, monto: {Amount}",
                exchangeRateNested.Exchange.SourceCurrency, exchangeRateNested.Exchange.TargetCurrency, exchangeRateNested.Exchange.Amount);
            
            if (exchangeRateNested.Exchange.SourceCurrency == exchangeRateNested.Exchange.TargetCurrency)
            {
                _logger.LogInformation("El usuario esta utilizando la misma moneda para origen y para destino");
                return BadRequest("Disculpa, tiene que utilizar monedas distintas para hacer la conversion X( ");
            }

            var data = new ExchangeRateDTO
            {
                SourceCurrency = exchangeRateNested.Exchange.SourceCurrency,
                TargetCurrency = exchangeRateNested.Exchange.TargetCurrency,
                Amount = exchangeRateNested.Exchange.Amount
            };


            var result = ExchangeRateMockThree.GetRateThree(data);

            if (result == null)
            {
                return BadRequest(new ReturnExchangeRateNestedDTO
                {
                    StatusCode = 400,
                    Message = "Disculpa, aun no tenemos esas divisas en nuestro sistema :( ",
                    Data = null
                });
            }

            return Ok(new ReturnExchangeRateNestedDTO
            {
                StatusCode = 200,
                Message = "OK",
                Data = new ReturnExchangeRateNestedDTO.DataPayload
                {
                    Total = result.Value
                }
            });
        }
    }
}
