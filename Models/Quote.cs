using System;
using System.Text.Json.Serialization;

namespace LemonMarkets.Models
{

    public class Quote
    {

        public string? Isin
        {
            get;
            set;
        }

        [JsonPropertyName ( "b_v" )]
        public int BidVolume
        {
            get;
            set;
        }
        
        [JsonPropertyName ( "a_v" )]
        public int AskVolume
        {
            get;
            set;
        }

        [JsonPropertyName ( "b" )]
        public decimal Bid
        {
            get;
            set;
        }

        [JsonPropertyName ( "a" )]
        public decimal Ask
        {
            get;
            set;
        }

        [JsonPropertyName ( "t" )]
        public DateTime Time
        {
            get;
            set;
        }

        public string? Mic
        {
            get;
            set;
        }

    }

}