using System;

namespace LemonMarkets.Models
{

    public class PortfolioEntry
    {

        #region get/set

        /// <summary>
        /// Number of current Instrument items in your Portfolio
        /// </summary>
        public int? Quantity
        {
            get;
            set;
        }

        /// <summary>
        /// Number of purchased Instrument items
        /// </summary>
        public int? Buy_quantity
        {
            get;
            set;
        }

        /// <summary>
        /// Number of sold Instrument items
        /// </summary>
        public int? Sell_quantity
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
        /// Minimum buy-in price
        /// </summary>
        public int? Buy_price_min
        {
            get;
            set;
        }

        /// <summary>
        /// Maximum buy-in price
        /// </summary>
        public int? Buy_price_max
        {
            get;
            set;
        }

        /// <summary>
        /// Average historical buy-in price
        /// </summary>
        public int? Buy_price_avg_historical
        {
            get;
            set;
        }

        /// <summary>
        /// Minimum sell price
        /// </summary>
        public int? Sell_price_min
        {
            get;
            set;
        }

        /// <summary>
        /// Maximum sell price
        /// </summary>
        public int? Sell_price_max
        {
            get;
            set;
        }

        /// <summary>
        /// Average historical sell price
        /// </summary>
        public int? Sell_price_avg_historical
        {
            get;
            set;
        }

        /// <summary>
        /// Total number of orders for Portfolio item
        /// </summary>
        public int? Orders_total
        {
            get;
            set;
        }

        /// <summary>
        /// Total number of sell orders for Portfolio item
        /// </summary>
        public int? Sell_orders_total
        {
            get;
            set;
        }

        /// <summary>
        /// Total number of buy orders for Portfolio item
        /// </summary>
        public int? Buy_orders_total
        {
            get;
            set;
        }

        #endregion get/set

    }

}