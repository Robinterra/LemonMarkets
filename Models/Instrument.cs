using System.Collections.Generic;
using LemonMarkets.Models.Enums;
using System.Text.Json.Serialization;

namespace LemonMarkets.Models
{
    public class Instrument
    {
        [JsonPropertyName("isin")]
        public string? ISIN
        {
            get; set;
        }

        [JsonPropertyName("wkn")]
        public string? WKN
        {
            get; set;
        }

        [JsonPropertyName("name")]
        public string? Name
        {
            get; set;
        }

        [JsonPropertyName("title")]
        public string? Title
        {
            get; set;
        }

        [JsonPropertyName("symbol")]
        public string? Symbol
        {
            get; set;
        }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public InstrumentType Type
        {
            get; set;
        }

        [JsonPropertyName("venues")]
        public List<TradingVenue> TradingVenues
        {
            get; set;
        } = new List<TradingVenue>();

    }

    public class TradingVenue
    {
        [JsonPropertyName("title")]
        public string? Title
        {
            get; set;
        }

        [JsonPropertyName("mic")]
        public string? Mic
        {
            get; set;
        }

        public string? Name
        {
            get;
            set;
        }

        public bool Tradable
        {
            get;
            set;
        }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Currency Currency
        {
            get;
            set;
        }

        public bool Is_open
        {
            get;
            set;
        }
    }

    
}
