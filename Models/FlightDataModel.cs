using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models
{
    public class FlightDataModel
    {
        [JsonPropertyName("destination")]
        public string Destination { get; set; }

        [JsonPropertyName("source")]
        public string Source { get; set; }
        public string FlightNumber { get; set; }

     
    }
}
