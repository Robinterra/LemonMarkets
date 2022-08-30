using System;
using System.Text.Json.Serialization;
using LemonMarkets.Helper;

namespace LemonMarkets.Models
{

    public class Trade
    {

        #region get/set

        /// <summary>
        /// The International Securities Identification Number of the instrument you requested the trades for.
        /// </summary>
        public string? Isin
        {
            get;
            set;
        }

        /// <summary>
        /// Price the trade happend at
        /// </summary>
        [JsonPropertyName("p")]
        [JsonConverter(typeof(JsonDecimalIntConverter))]
        public decimal Price
        {
            get;
            set;
        }

        /// <summary>
        /// Volume for the trade (quantity)
        /// </summary>
        [JsonPropertyName("v")]
        public int Volume
        {
            get;
            set;
        }

        /// <summary>
        /// Timestamp of time period the trade occured at
        /// </summary>
        [JsonPropertyName("t")]
        public DateTime Time
        {
            get;
            set;
        }

        /// <summary>
        /// Market Identifier Code of Trading Venue the trade occured at
        /// </summary>
        public string? Mic
        {
            get;
            set;
        }

        #endregion get/set

    }

}