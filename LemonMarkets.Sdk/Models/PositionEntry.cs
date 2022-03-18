using System;

namespace LemonMarkets.Models
{

    public class PositionEntry
    {

        #region get/set

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
        /// Average buy-in price
        /// </summary>
        public int? Buy_price_avg
        {
            get;
            set;
        }

        /// <summary>
        /// Current holding valuation to the market trading price
        /// </summary>
        public int? Estimated_price_total
        {
            get;
            set;
        }

        /// <summary>
        /// Current market trading price
        /// </summary>
        public int? Estimated_price
        {
            get;
            set;
        }

        #endregion get/set

    }

}