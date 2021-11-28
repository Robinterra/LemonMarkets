namespace LemonMarkets.Models.Requests.Trading
{

    public class RequestGetPortfolio
    {

        #region get/set

        /// <summary>
        /// Filter for a specific Instrument in your portfolio
        /// </summary>
        public string? Isin
        {
            get;
        }

        public string? Space_id
        {
            get;
        }

        #endregion get/set

        #region ctor

        public RequestGetPortfolio ( string? isin = null, string? spaceId = null)
        {
            this.Isin = isin;
            this.Space_id = spaceId;
        }

        #endregion ctor

    }

}