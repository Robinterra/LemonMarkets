using System.Collections.Generic;
using LemonMarkets.Models.Enums;

namespace LemonMarkets.Models
{
    public class InstrumentSearchFilter
    {

        #region get/set

        /// <summary>
        /// Search for Name/Title, ISIN, WKN or symbol. You can also perform a partial search by only specifiying the first 4 symbols.
        /// </summary>
        public string? Search
        {
            get;
        }

        /// <summary>
        /// Enter a Market Identifier Code (MIC) in there. We currently only offer data from the Munich Stock Exchange (XMUN).
        /// </summary>
        public string? Mic
        {
            get;
        }

        /// <summary>
        /// Specify the ISIN you are interested in. You can also specify multiple ISINs. Maximum 10 ISINs per Request.
        /// </summary>
        public string? Isin
        {
            get;
        }

        /// <summary>
        /// 3 letter abbreviation, e.g. "EUR" or "USD"
        /// </summary>
        public Currency Currency
        {
            get;
        }

        /// <summary>
        /// Filter for tradable or non-tradable Instruments with true or false
        /// </summary>
        public bool? IsTradable
        {
            get;
        }

        #endregion get/set

        #region ctor

        public InstrumentSearchFilter ( string? isin = null, string? mic = null, Currency currency = Currency.None, bool? isTradable = null, string? search = null )
        {
            this.Isin = isin;
            this.Currency = currency;
            this.IsTradable = isTradable;
            this.Search = search;
            this.Mic = mic;
        }

        #endregion ctor

    }
}
