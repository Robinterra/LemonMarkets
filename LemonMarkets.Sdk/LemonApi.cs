using LemonMarkets.Interfaces;
using LemonMarkets.Models;
using System.Linq;
using System.Threading;
using WsApiCore;
using System.Security.Cryptography.X509Certificates;
using LemonMarkets.Repos.V1;
using ApiService;
using System;
using LemonMarkets.Services;

namespace LemonMarkets
{

    /// <summary>
    /// Defines the <see cref="LemonApi" />.
    /// </summary>
    public class LemonApi : ILemonApi
    {

        #region vars

        public static string apiDataBaseUrl = "https://data.lemon.markets";

        public static string apiRealtimeUrl = "https://realtime.lemon.markets";

        public static string apiPaperTradingBaseUrl = "https://paper-trading.lemon.markets";

        public static string apiRealTradingBaseUrl = "https://trading.lemon.markets";

        #endregion vars

        #region get/set

        public ConnectionInfo ConnectionInfo
        {
            get;
        }

        public string MarketKey
        {
            get;
        }

        public string TradingKey
        {
            get;
        }

        #region trading

        internal IApiClient TradingApi
        {
            get;
        }

        public IOrdersRepo Orders
        {
            get;
        }

        public IPositionsRepo Positions
        {
            get;
        }

        public IPositionStatementsRepo PositionStatements
        {
            get;
        }

        public IBankstatementsRepo Bankstatements
        {
            get;
        }

        public IAccountRepo Account
        {
            get;
        }

        #endregion trading

        #region MarketApi

        internal IApiClient MarketDataApi
        {
            get;
        }

        public ILivestreamService Livestream
        {
            get;
        }

        public IQuotesRepo Quotes
        {
            get;
        }


        public ITradesRepo Trades
        {
            get;
        }

        public IVenuesRepo Venues
        {
            get;
        }


        public IInstrumentsRepo Instruments
        {
            get;
        }

        public IOpenHighLowCloseRepo OHLC
        {
            get;
        }

        #endregion MarketApi

        #endregion get/set

        #region ctor

        public LemonApi(string marketKey, string tradingKey, ConnectionInfo connectionInfo, IApiClient tradingApi, IApiClient marketDataApi, IApiClient realtimeApi)
        {
            this.ConnectionInfo = connectionInfo;
            this.MarketKey = marketKey;
            this.TradingKey = tradingKey;

            this.TradingApi = tradingApi;
            this.MarketDataApi = marketDataApi;

            this.Account = new AccountRepo(this.TradingApi);
            this.Orders = new OrdersRepo(this.TradingApi);
            this.Positions = new PositionsRepo(this.TradingApi);
            this.PositionStatements = new PositionStatementsRepo(this.TradingApi);
            this.Bankstatements = new BankstatementsRepo ( this.TradingApi );

            this.Venues = new VenuesRepo (this.MarketDataApi);
            this.Quotes = new QuotesRepo ( this.MarketDataApi );
            this.Instruments = new InstrumentsRepo ( this.MarketDataApi );
            this.OHLC = new OpenHighLowCloseRepo(this.MarketDataApi);
            this.Trades = new TradesRepo(this.MarketDataApi);

            this.Livestream = new MqttQoutesLivestreamService(realtimeApi, this.TradingApi);
        }

        #endregion ctor

        #region methods

        public static ILemonApi Build(string marketKey, string tradingKey, MoneyTradingMode mode)
        {
            string tradingUrl = mode == MoneyTradingMode.Paper ? apiPaperTradingBaseUrl : apiRealTradingBaseUrl;

            ConnectionInfo connectionInfo = new ConnectionInfo(apiDataBaseUrl, tradingUrl, apiRealtimeUrl);

            ApiClient tradingApi = new ApiClient(connectionInfo.TradingAdress, "v1");
            tradingApi.CheckCertEasy += Api_CheckCertEasy;
            tradingApi.SetNewAuth ( tradingKey );

            ApiClient marketDataApi = new ApiClient(connectionInfo.MarketDataAdress, "v1");
            marketDataApi.CheckCertEasy += Api_CheckCertEasy;
            marketDataApi.SetNewAuth ( marketKey );

            ApiClient realtimeApi = new ApiClient(connectionInfo.RealtimeAdress, "v1");
            realtimeApi.CheckCertEasy += Api_CheckCertEasy;
            realtimeApi.SetNewAuth ( marketKey );

            return new LemonApi(marketKey, tradingKey, connectionInfo, tradingApi, marketDataApi, realtimeApi);
        }

        #endregion methods

        #region events

        private static bool Api_CheckCertEasy(string hostname, X509Certificate2 x509Certificate2, X509Chain x509Chain)
        {
            if (x509Chain.ChainStatus.Any(status => status.Status == X509ChainStatusFlags.UntrustedRoot)) return false;//Assert.Fail("certifcate has no trusted root");
            //if (!x509Certificate2.Subject.Contains(string.Format("CN={0}", hostname))) return false;//Assert.Fail("Hostname of the certificate not matched");

            return true;
        }

        #endregion events

        #region enums

        public enum MoneyTradingMode
        {
            Paper,
            Real
        }

        #endregion enums

    }
}
