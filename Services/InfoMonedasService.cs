using Models;
using Newtonsoft.Json;
using System.Text.Json;
using System.Web;

namespace Services
{
    public class InfoMonedasService: IInfoMonedasService
    {
        private readonly string url = "https://api.apilayer.com/fixer/latest";
        private readonly string apiKey = "jC7gWtsSKtHwQQ5X1nVN6LGsfcIvVNpK";

        public async Task<InfoMonedas> Get(string symbol)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("ApiKey", apiKey);
            var response = await client.GetAsync($"{url}?base=USD&symbols={symbol}");
            var body = await response.Content.ReadAsStringAsync();
            var infoMonedas = JsonConvert.DeserializeObject<InfoMonedas>(body);
            return infoMonedas;
        }
    }
}