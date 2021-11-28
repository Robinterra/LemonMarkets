﻿using System;
using System.Text.Json.Serialization;
using LemonMarkets.Models.Enums;

namespace LemonMarkets.Models
{

    public class Transaction
    {

        #region get/set

        /// <summary>
        /// ID of transaction
        /// </summary>
        public string Id
        {
            get;
            set;
        }

        /// <summary>
        /// ID of related account
        /// </summary>
        public string Account_id
        {
            get;
            set;
        }

        /// <summary>
        /// ID of related space
        /// </summary>
        public string Space_id
        {
            get;
            set;
        }

        /// <summary>
        /// ID of related order
        /// </summary>
        public string Order_id
        {
            get;
            set;
        }

        /// <summary>
        /// Type of transaction, currently possible: pay_in, pay_out, order_buy, order_sell, dividend, tax
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TransactionType Type
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
        public string Isin
        {
            get;
            set;
        }

        /// <summary>
        /// Timestamp the Transaction was created at
        /// </summary>
        public DateTime Created_at
        {
            get;
            set;
        }

        #endregion get/set

    }

}