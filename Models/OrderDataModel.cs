using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models
{
    public class OrderDataModel
    {
        
        public int Id { get; set; }
        [JsonPropertyName("destination")]
        public string Destination { get; set; }
    }
}
