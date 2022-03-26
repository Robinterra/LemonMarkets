using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LemonMarkets.Models.Enums;

namespace LemonMarkets.Models
{

    public class QuoteLatestSearchFilter
    {

        #region get/set

        /// <summary>
        /// The ISIN of the instrument you want to get the quotes for. You can also specify multiple ISINs. Maximum 10 ISINs per request.
        /// </summary>
        [Required]
        public List<string> Isins
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
        /// Sort your API response, either ascending (asc) or descending (desc)
        /// </summary>
        public Sorting Sorting
        {
            get;
        }

        #endregion get/set

        #region ctor

        public QuoteLatestSearchFilter ( string isin, string? mic = null, Sorting sorting = Sorting.None )
        {
            this.Isins = new() { isin };
            this.Mic = mic;
            this.Sorting = sorting;
        }

        public QuoteLatestSearchFilter ( List<string> isins, string? mic = null, Sorting sorting = Sorting.None )
        {
            this.Isins = isins;
            this.Mic = mic;
            this.Sorting = sorting;
        }

        #endregion ctor

    }

}