using System.ComponentModel.DataAnnotations;

namespace LemonMarkets.Models
{

    public class QuoteSearchFilter
    {

        #region get/set

        /// <summary>
        /// The ISIN of the instrument you want to get the quotes for. You can also specify multiple ISINs. Maximum 10 ISINs per request.
        /// </summary>
        [Required]
        public string Isin
        {
            get;
        }

        #endregion get/set

        #region ctor

        public QuoteSearchFilter ( string isin )
        {
            this.Isin = isin;
        }

        #endregion ctor

    }

}