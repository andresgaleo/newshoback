using FlightData.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightData.Configuration
{
    public class WebserviceConfig : IWebserviceConfig
    {
        private readonly HttpClient _httpClient;
        public WebserviceConfig()
        {
            _httpClient = new HttpClient();
        }
        public async Task<string> GetDataFromUrl(string url)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseB = await response.Content.ReadAsStringAsync();
            return responseB;
        }

        public async Task<string> GetData(string url)
        {
            string data = await GetDataFromUrl(url);
            return data;
        }
    }
}
