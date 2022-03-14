using System;
using System.Collections.Generic;
using LemonMarkets.Models.Enums;

namespace LemonMarkets.Models
{
    public class OrderSearchFilter
    {

        #region get/set

        public OrderSide Side
        {
            get;
        }

        public DateTime? From
        {
            get;
        }

        public DateTime? To
        {
            get;
        }

        public OrderType Type
        {
            get;
        }

        public OrderStatus Status
        {
            get;
        }

        public bool WithPaging
        {
            get;
        }

        public List<string> Isins
        {
            get;
        }

        #endregion get/set

        #region ctor

        public OrderSearchFilter(List<string>? isins = null, string? isin = null, OrderStatus orderStatus = OrderStatus.All, OrderType orderType = OrderType.All, OrderSide orderSide = OrderSide.All, DateTime? to = null, DateTime? from = null)
        {
            if (isins != null) this.Isins = isins;
            else this.Isins = new List<string>();

            if (isin != null) this.Isins.Add(isin);
            this.Status = orderStatus;
            this.Type = orderType;
            this.Side = orderSide;
            this.From = from;
            this.To = to;
        }

        #endregion ctor

    }
}
