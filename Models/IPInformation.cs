using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class IPInformation
    {
        public double Latitude { get; set; }
        
        public double Longitude { get; set; }

        public string CountryCode { get; set; }

        public string CountryName { get; set; }

        public List<Lenguages> Lenguages { get; set; }

        public DateTime CurrentTime { get; set; }

        public DateTime CurrentTimeLocal { get; set; }

        public string CurrentTimeFormatted { get; set; }

        public string EstimatedDistance { get; set; }

        public string NameCurrency { get; set; }

        public string SymbolCurrency { get; set; }

        public string DollarQuote { get; set; }

        public string FarDistance { get; set; }

        public string CloseDistance { get; set; }

        public string AverageDistance { get; set; }

        public int Invocation { get; set; }
    }
}
