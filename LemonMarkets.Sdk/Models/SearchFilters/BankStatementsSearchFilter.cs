using System;
using LemonMarkets.Models.Enums;

namespace LemonMarkets.Models
{

    public class BankStatementsFilter
    {

        #region get/set

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
        public BankstatementType Type
        {
            get;
        }

        /// <summary>
        /// Use asc_ to sort your bank statements in ascending order (oldest ones first), or desc to sort your bank statements in descending order (newest ones first).
        /// </summary>
        public Sorting Sorting
        {
            get;
        }

        #endregion get/set

        #region ctor

        public BankStatementsFilter ( DateTime? to = null, DateTime? from = null, string? isin = null, BankstatementType type = BankstatementType.None, Sorting sorting = Sorting.None )
        {
            this.To = to;
            this.From = from;
            this.Type = type;
            this.Isin = isin;
            this.Sorting = sorting;
        }

        #endregion ctor


    }

}