using System;

namespace LemonMarkets
{

    public class WertEntry
    {

        #region get/set

        public string Isin
        {
            get;
            set;
        }

        public decimal MinimumBuy
        {
            get;
            set;
        }

        public decimal MaximumBuy
        {
            get;
            set;
        }

        public decimal LastBuyValue
        {
            get;
            set;
        }

        public decimal LastSellValue
        {
            get;
            set;
        }

        public DateTime LastSellDate
        {
            get;
            set;
        }

        #endregion get/set

    }

}