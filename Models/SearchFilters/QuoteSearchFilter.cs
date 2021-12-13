using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LemonMarkets.Models.Enums;

namespace LemonMarkets.Models
{

    public class QuoteSearchFilter
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
        /// Start of Time Range for which you want to get the quotes. Use int/date-iso-string to define your timestamp range. Use from=latest to receive the latest quotes.
        /// </summary>
        public DateTime? From
        {
            get;
        }

        /// <summary>
        /// End of Time Range for which you want to get the quotes. Use int/date-iso-string to define your timestamp range.
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

        public QuoteSearchFilter ( string isin, string? mic = null, DateTime? from = null, DateTime? to = null, Sorting sorting = Sorting.None )
        {
            this.Isins = new() { isin };
            this.Mic = mic;
            this.From = from;
            this.To = to;
            this.Sorting = sorting;
        }

        public QuoteSearchFilter ( List<string> isins, string? mic = null, DateTime? from = null, DateTime? to = null, Sorting sorting = Sorting.None )
        {
            this.Isins = isins;
            this.Mic = mic;
            this.From = from;
            this.To = to;
            this.Sorting = sorting;
        }

        #endregion ctor

    }

}