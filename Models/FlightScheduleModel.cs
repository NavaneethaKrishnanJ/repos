using System.Text.Json.Serialization;

namespace Models
{
    public class FlightScheduleModel
    {
       
        public int FlightNumber { get; set; }
        [JsonPropertyName("destination")]
        public string Destination { get; set; }
        [JsonPropertyName("source")]
        public string source { get; set; }
        public int Day { get; set; }
        public List<OrderDataModel> Orders { get; set; } = new List<OrderDataModel>();
    }
}
