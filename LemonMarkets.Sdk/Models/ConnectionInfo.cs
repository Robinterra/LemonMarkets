using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonMarkets.Models
{
    public class ConnectionInfo
    {

        #region get/set

        public string MarketDataAdress
        {
            get;
        }

        public string TradingAdress
        {
            get;
        }

        public string RealtimeAdress
        {
            get;
        }

        #endregion get/set

        #region ctor

        public ConnectionInfo(string marketDataAdress, string tradingAdress, string realtimeAdress)
        {
            this.MarketDataAdress = marketDataAdress;
            this.TradingAdress = tradingAdress;
            this.RealtimeAdress = realtimeAdress;
        }

        #endregion ctor

    }
}
