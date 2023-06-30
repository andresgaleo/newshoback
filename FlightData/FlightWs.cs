using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightData.Configuration;

namespace FlightData
{
    public class FlightWs
    {
        private readonly WebserviceConfig _webserviceConfig;

        public FlightWs(WebserviceConfig webserviceConfig)
        {
            _webserviceConfig = webserviceConfig;
        }
        
    }
}
