using Newtonsoft.Json;

namespace Models
{
    public class Geolocalizacion
    {
        [JsonProperty("ip")]
        public string IP { get; set; } 

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("country_name")]
        public string CountryName { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("time_zone")]
        public TimeZone TimeZone { get; set; }

        [JsonProperty("currency")]
        public Currency Currency { get; set; }

    }
}