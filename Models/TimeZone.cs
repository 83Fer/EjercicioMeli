using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class TimeZone
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("current_time")]
        public DateTime CurrentTime { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

    }
}
