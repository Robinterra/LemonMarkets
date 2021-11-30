using System;
using LemonMarkets.Models.Enums;

namespace LemonMarkets.Models
{

    public class OHLCSearchFilter
    {

        #region get/set

        /// <summary>
        /// The ISIN of the instrument you want to get the OHLC data for. You can also specify multiple ISINs. Maximum 10 ISINs per request.
        /// </summary>
        public string Isin
        {
            get;
        }

        /// <summary>
        /// Specify the type of data you wish to retrieve: m1, h1, or d1.
        /// </summary>
        public OHLCTimeMode TimeMode
        {
            get;
        }

        /// <summary>
        /// Market Identifier Code of Trading Venue. Currently, only XMUN is supported.
        /// </summary>
        public string? Mic
        {
            get;
        }

        /// <summary>
        /// Start of Time Range for which you want to get the OHLC data. Use int/date-iso-string to define your timestamp range. Use from=latest to receive the latest quotes.
        /// </summary>
        public DateTime? From
        {
            get;
        }

        /// <summary>
        /// End of Time Range for which you want to get the OHLC data. Use int/date-iso-string to define your timestamp range.
        /// </summary>
        public DateTime? To
        {
            get;
        }

        /// <summary>
        /// Sort your API response, either ascending (asc) or descending (desc)
        /// </summary>
        public Sorting Sorting
        {
            get;
        }

        #endregion get/set

        #region ctor

        public OHLCSearchFilter ( string isin, OHLCTimeMode timeMode, string? mic = null, DateTime? from = null, DateTime? to = null, Sorting sorting = Sorting.None )
        {
            this.Isin = isin;
            this.TimeMode = timeMode;
            this.Mic = mic;
            this.From = from;
            this.To = to;
            this.Sorting = sorting;
        }

        #endregion ctor

    }

}