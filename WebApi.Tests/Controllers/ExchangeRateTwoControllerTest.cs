using ExchangeRateWebApi.Controllers;
using ExchangeRateWebApi.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Tests.Controllers
{
    public class ExchangeRateTwoControllerTest
    {
        [Fact]
        public void Post_ReturnOk_IfRateExists()
        {
            var mockLogger = new Mock<ILogger<ExchangeRateTwoController>>();
            var controller = new ExchangeRateTwoController(mockLogger.Object);

            var input = new ExchangeRateXmlDTO
            {
                SourceCurrency = "USD",
                TargetCurrency = "DOP",
                Amount = 100
            };

            var result = controller.Post(input);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnDto = Assert.IsType<ReturnExchangeRateXmlDTO>(okResult.Value);
            Assert.True(returnDto.Result > 0);
        }

        [Fact]
        public void Post_DevuelveBadRequest_SiLaTasaEsNull()
        {
            var mockLogger = new Mock<ILogger<ExchangeRateTwoController>>();
            var controller = new ExchangeRateTwoController(mockLogger.Object);

            var input = new ExchangeRateXmlDTO
            {
                SourceCurrency = "COP",
                TargetCurrency = "USD",
                Amount = 100
            };

            var result = controller.Post(input);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Disculpa, aun no tenemos esas divisas en nuestro sistema :( ", badRequestResult.Value);
        }
    }
}