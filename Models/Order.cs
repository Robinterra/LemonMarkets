using System;
using System.Text.Json.Serialization;
using LemonMarkets.Models.Enums;

namespace LemonMarkets.Models
{
    public class Order
    {
        /// <summary>
        /// International Securities Identification Number of instrument
        /// </summary>
        public string? Isin
        {
            get; set;
        }

        /// <summary>
        /// Timestamp of point in time until order is valid.
        /// </summary>
        public DateTime Expires_at
        {
            get; set;
        }

        /// <summary>
        /// Either "buy" or "sell"
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OrderSide Side
        {
            get; set;
        }

        /// <summary>
        /// Amount of instruments you specified for order.
        /// </summary>
        public int? Quantity
        {
            get; set;
        }

        /// <summary>
        /// Stop price for order. "null" if not specified.
        /// </summary>
        public int? Stop_price
        {
            get; set;
        }

        /// <summary>
        /// Limit price for order. "null" if not specified.
        /// </summary>
        public int? Limit_price
        {
            get; set;
        }

        /// <summary>
        /// Order id
        /// </summary>
        public string? Id
        {
            get; set;
        }

        /// <summary>
        /// ID of space you want to place the order with
        /// </summary>
        public string? Space_id
        {
            get;
            set;
        }

        /// <summary>
        /// Market Identifier Code for trading venue the order was placed at.
        /// </summary>
        public string? Venue
        {
            get;
            set;
        }

        /// <summary>
        /// Order status.
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OrderStatus Status
        {
            get; set;
        }

        /// <summary>
        /// Estimation from our end for what price the Order will be executed
        /// </summary>
        public int? Estimated_price
        {
            get; set;
        }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OrderType Type
        {
            get; set;
        }

        //[JsonConverter(typeof(DoubleDateTimeJsonConverter))]
        [JsonPropertyName("processed_at")]
        public DateTime ProcessedAt
        {
            get; set;
        }

        [JsonPropertyName("processed_quantity")] 
        public int? ProcessedQuantity
        {
            get; set;
        }
    }

    public class InstrumentShort
    {
        [JsonPropertyName("title")] 
        public string? Title
        {
            get; set;
        }

        [JsonPropertyName("isin")] 
        public string? Isin
        {
            get; set;
        }
    }

}
