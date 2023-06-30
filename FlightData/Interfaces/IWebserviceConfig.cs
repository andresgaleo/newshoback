using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightData.Interfaces
{
    public interface IWebserviceConfig
    {
        Task<string> GetData(string url);
        Task<string> GetDataFromUrl(string url);
    }
}
