using System;
using LemonMarkets.Models.Enums;

namespace LemonMarkets.Models.Requests.Trading
{

    public class RequestGetTransactions
    {

        #region get/set

        /// <summary>
        /// ID of Space you want to get the transactions for
        /// </summary>
        /*public string? Space_id
        {
            get;
        }*/

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

        /// <summary>
        /// Filter for different types of bank statements: pay_in, pay_out, order_buy, order_sell, eod_balance, dividend
        /// </summary>
        public TransactionType Type
        {
            get;
        }

        #endregion get/set

        #region ctor

        public RequestGetTransactions ( /*string? spaceId = null,*/ DateTime? to = null, DateTime? from = null, string? isin = null, TransactionType type = TransactionType.None )
        {
            //this.Space_id = spaceId;
            this.To = to;
            this.From = from;
            this.Type = type;
            this.Isin = isin;
        }

        #endregion ctor

    }

}