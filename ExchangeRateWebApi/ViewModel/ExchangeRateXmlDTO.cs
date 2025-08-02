using System.Xml.Serialization;

namespace ExchangeRateWebApi.ViewModel
{
    [XmlRoot("ExchangeRateXmlDTO")]
    public class ExchangeRateXmlDTO
    {
        [XmlElement("sourceCurrency")]
        public string SourceCurrency { get; set; }

        [XmlElement("targetCurrency")]
        public string TargetCurrency { get; set; }

        [XmlElement("amount")]
        public decimal Amount { get; set; }
    }
}
