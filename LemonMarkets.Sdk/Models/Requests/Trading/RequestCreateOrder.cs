using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using LemonMarkets.Models.Enums;

namespace LemonMarkets.Models.Requests.Trading
{
    public class RequestCreateOrder
    {

        #region get/set

        /// <summary>
        /// ISIN of an instrument
        /// </summary>
        [Required]
        public string Isin
        {
            get;
        }

        /// <summary>
        /// ISO String date in milliseconds. Maximum expiration date is 30 days in the future.
        /// Small hack: write expires_at = "pxd" to let the Order be valid for x days(e.g. "p7d").
        /// </summary>
        [Required]
        public DateTime Expires_at
        {
            get;
        }

        /// <summary>
        /// "buy" or "sell"
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [Required]
        public OrderSide Side
        {
            get;
        }

        /// <summary>
        /// The amount of shares you want to buy. Limited to 1000 per request.
        /// </summary>
        [Required]
        public int Quantity
        {
            get;
        }

        /// <summary>
        /// Market Identifier Code of Stock exchange
        /// </summary>
        [Required]
        public string Venue
        {
            get;
        }

        /// <summary>
        /// Stop Price for your Order.
        /// Please see here for information on the numbers format in the Trading API.
        /// </summary>
        public long? Stop_price
        {
            get;
        }

        /// <summary>
        /// Limit Price for your Order.
        /// Please see here for information on the numbers format in the Trading API.
        /// </summary>
        public long? Limit_price
        {
            get;
        }

        /// <summary>
        /// Your personal Notes you wish to attach to the Order
        /// </summary>
        public string? Notes
        {
            get;
        }

        #endregion get/set

        #region ctor

        public RequestCreateOrder(string isin, DateTime expires, OrderSide side, int quantity, string venue, decimal? stop = null, decimal? limit = null, string? notes = null)
        {
            this.Isin = isin;
            this.Expires_at = expires;
            this.Side = side;
            this.Quantity = quantity;
            this.Venue = venue;
            this.Stop_price = stop is null ? null : (long)(stop * 10000);
            this.Limit_price = limit is null ? null : (long)(limit * 10000);
            this.Notes = notes;
        }

        #endregion ctor

    }
}
