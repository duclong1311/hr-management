using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terp.UI.Services.ExchangeRateCurrency
{
    public class ExchangRateAPI
    {
        public string Result { get; set; }
        public string Documentation { get; set; }
        public string Terms_of_use { get; set; }
        public string Time_last_update_unix { get; set; }
        public string Time_last_update_utc { get; set; }
        public string Time_next_update_unix { get; set; }
        public string Time_next_update_utc { get; set; }
        public string Tase_code { get; set; }
        public ConversionRate Conversion_rates { get; set; }
    }
}
