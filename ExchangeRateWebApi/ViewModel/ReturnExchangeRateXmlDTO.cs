using System.Xml.Serialization;

namespace ExchangeRateWebApi.ViewModel
{
    [XmlRoot("ExchangeRateResponse")]
    public class ReturnExchangeRateXmlDTO
    {
        public decimal Result { get; set; }
    }
}
