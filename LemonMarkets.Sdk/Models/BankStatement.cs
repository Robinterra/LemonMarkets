using System;
using System.Text.Json.Serialization;
using LemonMarkets.Models.Enums;

namespace LemonMarkets.Models
{

    public class BankStatement
    {

        #region get/set

        /// <summary>
        /// ID of transaction
        /// </summary>
        public string? Id
        {
            get;
            set;
        }

        /// <summary>
        /// ID of related account
        /// </summary>
        public string? Account_id
        {
            get;
            set;
        }

        /// <summary>
        /// Type of transaction, currently possible: pay_in, pay_out, order_buy, order_sell, dividend, tax
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public BankstatementType Type
        {
            get;
            set;
        }

        /// <summary>
        /// Amount of transaction
        /// </summary>
        public int Amount
        {
            get;
            set;
        }

        /// <summary>
        /// Related instrument of transaction
        /// </summary>
        public string? Isin
        {
            get;
            set;
        }

        /// <summary>
        /// Title of related International Securities Identification Number (ISIN). Only for type order_buy and order_sell, otherwise null
        /// </summary>
        public string? Isin_title
        {
            get;
            set;
        }

        /// <summary>
        /// Date of bank statement (YYYY-MM-DD)
        /// </summary>
        public DateTime Date
        {
            get;
            set;
        }

        /// <summary>
        /// Exact timestamp the bank statement was created
        /// </summary>
        public DateTime Created_at
        {
            get;
            set;
        }

        #endregion get/set

    }

}