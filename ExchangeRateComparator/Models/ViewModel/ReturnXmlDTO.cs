using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ExchangeRateComparator.Models.ViewModel
{
    [XmlRoot("ExchangeRateResponse")]
    public class ReturnXmlDTO
    {
        public decimal Result { get; set; }
    }
}
