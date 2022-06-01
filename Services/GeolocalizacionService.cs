using Models;
using Newtonsoft.Json;
using System.Text.Json;
using System.Web;

namespace Services
{
    public class GeolocalizacionService : IGeolocalizacionService
    {
        private readonly string url = "http://api.ipapi.com/api";
        private readonly string accessKey = "access_key=ed808730399a275f2758d5adabb74090";

        public async Task<Geolocalizacion> Get(string ip)
        {
            var client = new HttpClient();
            var response = await client.GetAsync($"{url}/{ip}?{accessKey}");
            var body = await response.Content.ReadAsStringAsync();
            var geolocalizacion = JsonConvert.DeserializeObject<Geolocalizacion>(body);
            return geolocalizacion;
        }
    }
}