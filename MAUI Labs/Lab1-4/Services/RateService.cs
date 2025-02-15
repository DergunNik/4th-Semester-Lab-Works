using Lab1.Entities;
using Lab1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Services
{
    public class RateService : IRateService
    {
        private readonly HttpClient httpClient;

        public RateService(IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient();
        }

        public async Task<IEnumerable<Rate>> GetRatesAsync(DateTime date)
        {
            var date_string = date.ToString("yyyy-MM-dd");
            var response = await httpClient.GetAsync($"https://api.nbrb.by/exrates/rates?ondate={date_string}&periodicity=0");
            var rates = await response.Content.ReadFromJsonAsync(RateContext.Default.ListRate);
            return rates;
        }
    }
}
