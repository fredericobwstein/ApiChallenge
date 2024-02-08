using System.Text.Json.Serialization;

namespace FulltechAPI.Core.Entities
{
    public class Holiday
    {
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }
    }
}
