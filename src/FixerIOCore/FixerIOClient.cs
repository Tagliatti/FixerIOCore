using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FixerIoCore
{
    public class FixerIoClient
    {
        private readonly string _baseCurrency;
        private readonly bool _https;
        private ICollection<string> _symbols;

        public FixerIoClient()
        {
            _https = false;
            _baseCurrency = Symbol.EUR.ToString();
        }

        public FixerIoClient(Symbol baseCurrency)
        {
            _baseCurrency = baseCurrency.ToString();
        }

        public FixerIoClient(Symbol baseCurrency, IEnumerable<Symbol> symbols)
        {
            _baseCurrency = baseCurrency.ToString();
            SetSymbols(symbols);
        }

        public FixerIoClient(Symbol baseCurrency, IEnumerable<Symbol> symbols, bool https)
        {
            _baseCurrency = baseCurrency.ToString();
            SetSymbols(symbols);
            _https = https;
        }

        private void SetSymbols(IEnumerable<Symbol> symbols)
        {
            _symbols = new List<string>();

            foreach (var symbol in symbols)
            {
                _symbols.Add(symbol.ToString());
            }
        }

        public async Task<Quote> GetLatestAsync()
        {
            return await Request();
        }

        public Quote GetLatest()
        {
            return Request().Result;
        }

        public async Task<Quote> GetForDateAsync(DateTime date)
        {
            return await Request(date, null);
        }

        public Quote GetForDate(DateTime date)
        {
            return Request(date, null).Result;
        }

        public async Task<decimal> ConvertAsync(Symbol from, Symbol to)
        {
            return (await Request(null, from)).Rates[to.ToString()];
        }

        public decimal Convert(Symbol from, Symbol to)
        {
            return Request(null, from).Result.Rates[to.ToString()];
        }

        public async Task<decimal> ConvertAsync(Symbol from, Symbol to, DateTime date)
        {
            return (await Request(date, from)).Rates[to.ToString()];
        }

        public decimal Convert(Symbol from, Symbol to, DateTime date)
        {
            return Request(date, from).Result.Rates[to.ToString()];
        }

        private async Task<Quote> Request()
        {
            return await Request(null, null);
        }

        private string PrepareRequest(DateTime? date, Symbol? from)
        {
            var currency = (from.HasValue ? from.ToString() : _baseCurrency);
            var dateString = date == null ? "latest" : date.Value.ToString("yyyy-MM-dd");
            var queryString = $"{dateString}?base={currency}";

            if (_symbols != null && from == null)
            {
                queryString += $"&symbols={string.Join(",", _symbols)}";
            }

            return queryString;
        }

        private async Task<Quote> Request(DateTime? date, Symbol? from)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri($"{(_https ? "https" : "http")}://api.fixer.io/");

                var queryString = PrepareRequest(date, from);
                var response = await httpClient.GetAsync(queryString);

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
