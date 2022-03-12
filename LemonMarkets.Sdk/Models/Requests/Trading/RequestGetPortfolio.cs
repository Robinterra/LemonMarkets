using System.Collections.Generic;

namespace LemonMarkets.Models.Requests.Trading
{

    public class RequestGetPortfolio
    {

        #region get/set

        /// <summary>
        /// Filter for a specific Instrument in your portfolio
        /// </summary>
        public List<string> Isins
        {
            get;
        }

        #endregion get/set

        #region ctor

        public RequestGetPortfolio ( string? isin = null, List<string>? isins = null)
        {
            if (isins != null) this.Isins = isins;
            else this.Isins = new List<string>();

            if (isin != null) this.Isins.Add(isin);
        }

        #endregion ctor

    }

}