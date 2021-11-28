namespace LemonMarkets.Models
{

    public class VenueSearchFilter
    {

        #region get/set

        /// <summary>
        /// Enter a Market Identifier Code (MIC) in there. Default is XMUN.
        /// </summary>
        public string? Mic
        {
            get;
        }

        #endregion get/set

        #region ctor

        public VenueSearchFilter ( string? mic )
        {
            this.Mic = mic;
        }

        #endregion ctor

    }

}