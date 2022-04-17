using System;
using System.Text.Json.Serialization;
using LemonMarkets.Models.Enums;

namespace LemonMarkets.Models
{

    public class Statement
    {

        #region get/set

        /// <summary>
        /// This is the unique Identification Number for the statement
        /// </summary>
        public string? Id
        {
            get;
            set;
        }

        /// <summary>
        /// This is the unique Identification Number for the related order (if type = order_buy or order_sell, otherwise null)
        /// </summary>
        public string? Order_id
        {
            get;
            set;
        }

        /// <summary>
        /// If you imported a position from an external source, we provide the external identification number here
        /// </summary>
        public string? External_id
        {
            get;
            set;
        }

        /// <summary>
        /// International Securities Identification Number of instrument
        /// </summary>
        public string? Isin
        {
            get;
            set;
        }

        /// <summary>
        /// Title of instrument
        /// </summary>
        public string? Isin_title
        {
            get;
            set;
        }

        /// <summary>
        /// Number of current Instrument items in your Portfolio
        /// </summary>
        public int? Quantity
        {
            get;
            set;
        }

        /// <summary>
        /// Timestamp for when the statement is processed by us internally. It can be the case that we receive data 1-3 days later.
        /// So, for example when a position change occurs on a Friday afternoon, we only receive the data on Monday morning. Therefore, date would then be YYYY-MM-DD of the Friday, while
        /// </summary>
        public DateTime Created_at
        {
            get;
            set;
        }

        /// <summary>
        /// This is the type of statement that tells you about the type of change that happened to your position : order_buy | order_sell | split | import | snx
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PositionStatementType Type
        {
            get;
            set;
        }

        #endregion get/set

    }

}