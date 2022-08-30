using System;
using System.Text.Json.Serialization;
using LemonMarkets.Helper;

namespace LemonMarkets.Models
{

    public class Quote
    {

        #region get/set

        /// <summary>
        /// The International Securities Identification Number of the instrument you requested the quotes for.
        /// </summary>
        public string? Isin
        {
            get;
            set;
        }

        /// <summary>
        /// Number of instruments sold at bid price.
        /// </summary>
        [JsonPropertyName ( "b_v" )]
        public int BidVolume
        {
            get;
            set;
        }

        /// <summary>
        /// Number of instruments sold at ask price.
        /// </summary>
        [JsonPropertyName ( "a_v" )]
        public int AskVolume
        {
            get;
            set;
        }

        /// <summary>
        /// The maximum price the buyer is willing to pay for a specific instrument.
        /// </summary>
        [JsonPropertyName ( "b" )]
        [JsonConverter(typeof(JsonDecimalIntConverter))]
        public decimal Bid
        {
            get;
            set;
        }

        /// <summary>
        /// The minimum price the seller is willing to sell the specific instrument for.
        /// </summary>
        [JsonPropertyName ( "a" )]
        [JsonConverter(typeof(JsonDecimalIntConverter))]
        public decimal Ask
        {
            get;
            set;
        }

        /// <summary>
        /// Timestamp of point in time the Quote occured at
        /// </summary>
        [JsonPropertyName ( "t" )]
        [JsonConverter(typeof(JsonUnixDateTimeConverter))]
        public DateTime Time
        {
            get;
            set;
        }

        /// <summary>
        /// Market Identifier Code of Trading Venue the Quote occured at
        /// </summary>
        public string? Mic
        {
            get;
            set;
        }

        #endregion get/set

    }

}