using System;
using System.Text.Json.Serialization;
using LemonMarkets.Helper;

namespace LemonMarkets.Models
{

    public class OHLCEntry
    {

        #region get/set

        /// <summary>
        /// ISIN of instrument you want to retrieve the OHLC data for
        /// </summary>
        public string? Isin
        {
            get;
            set;
        }

        /// <summary>
        /// Open Price in specific time period
        /// </summary>
        [JsonPropertyName("o")]
        [JsonConverter(typeof(JsonDecimalIntConverter))]
        public decimal Open
        {
            get;
            set;
        }

        /// <summary>
        /// High Price in specific time period
        /// </summary>
        [JsonPropertyName("h")]
        [JsonConverter(typeof(JsonDecimalIntConverter))]
        public decimal High
        {
            get;
            set;
        }

        /// <summary>
        /// Low Price in specific time period
        /// </summary>
        [JsonPropertyName("l")]
        [JsonConverter(typeof(JsonDecimalIntConverter))]
        public decimal Low
        {
            get;
            set;
        }

        /// <summary>
        /// Close Price in specific time period
        /// </summary>
        [JsonPropertyName("c")]
        [JsonConverter(typeof(JsonDecimalIntConverter))]
        public decimal Close
        {
            get;
            set;
        }

        /// <summary>
        /// Timestamp of time period the OHLC data is based on
        /// </summary>
        [JsonPropertyName("t")]
        public DateTime Time
        {
            get;
            set;
        }

        /// <summary>
        /// Market Identifier Code of Trading Venue the OHLC data occured at
        /// </summary>
        public string? Mic
        {
            get;
            set;
        }

        /// <summary>
        /// Price by Volume (Sum of (quantity * last price)) of instrument in specific time period
        /// </summary>
        [JsonPropertyName("pbv")]
        [JsonConverter(typeof(JsonDecimalIntConverter))]
        public decimal PriceByVolume
        {
            get;
            set;
        }

        /// <summary>
        /// Aggegrated volume (Number of trades) of instrument in specific time periods
        /// </summary>
        [JsonPropertyName("v")]
        public int Volume
        {
            get;
            set;
        }

        #endregion get/set

    }

}