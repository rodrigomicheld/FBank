using System.Text.Json.Serialization;

namespace FBank.Function.Model
{
    public class PixTransferOperation
    {
        [JsonPropertyName("accountNumberFrom")]
        public int AccountNumberFrom { get; set; }

        [JsonPropertyName("accountNumberTo")]
        public int AccountNumberTo { get; set; }

        [JsonPropertyName("value")]
        public decimal Value { get; set; }
    }
}
