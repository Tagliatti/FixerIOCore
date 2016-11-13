using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FixerIOCore
{
    public class FixerIOClient
    {
        private String _date;
        private String _base;
        private bool _https;
        private ICollection<string> _rates;

        public FixerIOClient(bool https = false)
        {
            _date = "latest";
            _base = Symbol.EUR.ToString();
            _rates = new List<string>();
        }

        public async Task<Quote> Quote()
        {
            return await Request();
        }

        public async Task<Quote> Quote(Symbol baseSymbol)
        {
            _base = baseSymbol.ToString();

            return await Request();
        }

        public async Task<Quote> Quote(Symbol baseSymbol, DateTime date)
        {
            _base = baseSymbol.ToString();
            _date = date.ToString("yyyy-MM-dd");

            return await Request();
        }

        public async Task<Quote> Quote(Symbol baseSymbol, DateTime date, Symbol[] rates)
        {
            _base = baseSymbol.ToString();
            _date = date.ToString("yyyy-MM-dd");

            foreach (var r in rates)
            {
                _rates.Add(r.ToString());
            }

            return await Request();
        }

        private async Task<Quote> Request()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri($"{(_https ? "https" : "http")}://api.fixer.io/");

                var queryString = $"{_date}?base={_base}";

                if (_rates.Count > 0)
                {
                    queryString += string.Join(",", _rates);
                }

                var response = await client.GetAsync(queryString);

                if (response.IsSuccessStatusCode)
                {
                    var stringResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Quote>(stringResponse);
                }

                throw new Exception($"Request failed ({response.StatusCode})");
            }
        }
    }
}
