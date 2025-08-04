using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ExchangeRateComparator.Models.ViewModel
{
    [XmlRoot("ExchangeRateXmlDTO")]
    public class XmlDTO
    {
        [XmlElement("sourceCurrency")]
        public string SourceCurrency { get; set; } = string.Empty;

        [XmlElement("targetCurrency")]
        public string TargetCurrency { get; set; } = string.Empty;

        [XmlElement("amount")]
        public decimal Amount { get; set; }
    }
}
