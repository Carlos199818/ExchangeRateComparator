using ExchangeRateWebApi.Mock;
using ExchangeRateWebApi.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeRateWebApi.Controllers
{
    [ApiController]
    [Route("api/exchange-three")]
    public class ExchangeRateThreeController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] ExchangeRateNestedDTO exchangeRateNested)
        {
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
