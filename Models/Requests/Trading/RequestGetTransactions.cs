using System;

namespace LemonMarkets.Models.Requests.Trading
{

    public class RequestGetTransactions
    {

        #region get/set

        /// <summary>
        /// ID of Space you want to get the transactions for
        /// </summary>
        public string? Space_id
        {
            get;
        }

        /// <summary>
        /// Define ISO String date for your desired end date
        /// </summary>
        public DateTime? To
        {
            get;
        }

        /// <summary>
        /// Define ISO String date for your desired start date
        /// </summary>
        public DateTime? From
        {
            get;
        }

        /// <summary>
        /// Use this parameter to only show Transactions of specific Instrument
        /// </summary>
        public string? Isin
        {
            get;
        }

        #endregion get/set

        #region ctor

        public RequestGetTransactions ( string? spaceId = null, DateTime? to = null, DateTime? from = null, string? isin = null )
        {
            this.Space_id = spaceId;
            this.To = to;
            this.From = from;
            this.Isin = isin;
        }

        #endregion ctor

    }

}