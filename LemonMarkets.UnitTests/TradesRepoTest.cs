using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ApiService;
using LemonMarkets.Models;
using LemonMarkets.Models.Responses;
using Xunit;
using LemonMarkets.Repos.V1;
using LemonMarkets.Interfaces;

namespace LemonMarkets.UnitTests
{
    public class TradesRepoTest
    {

        #region vars

        private List<Trade> trades;

        #endregion vars

        #region ctor

        public TradesRepoTest ()
        {
            this.trades = new List<Trade> ();
            this.trades.Add(new Trade () { Price = 12, Isin = "DE123456", Mic = "XMN", Time = DateTime.Parse("2022-03-13")});
            this.trades.Add(new Trade () { Isin = "DE123457", Mic = "MUNICH", Time = DateTime.Parse("2022-02-25")});
            this.trades.Add(new Trade () { Isin = "DE123456", Mic = "MUNICH", Time = DateTime.Parse("2022-02-10")});
            this.trades.Add(new Trade () { Isin = "DE123457", Mic = "XMN", Time = DateTime.Parse("2022-02-08")});
            this.trades.Add(new Trade () { Isin = "XMN", Mic = "DE123456", Time = DateTime.Parse("2022-03-10")});
            this.trades.Add(new Trade () { Isin = "DE123458", Mic = "XMN", Time = DateTime.Parse("2022-03-12")});
        }

        #endregion ctor

        #region methods

        #region GetLatest_ShouldReturn2Trades_WhenAskForTradesWith2IsinAndOneMic

        [Fact]
        public async Task GetLatest_ShouldReturn2Trades_WhenAskForTradesWith2IsinAndOneMic()
        {
            // Arrange
            IApiClient apiClient = new FakeApiClient(LemonApi.apiDataBaseUrl, "v1", get: ApiClient_GetLatest_ShouldReturn2Trades_WhenAskForTradesWith2IsinAndOneMic );
            ITradesRepo tradesRepo = new TradesRepo(apiClient);

            List<string> isins = new List<string> {"DE123456", "DE123458"};
            TradesLatestSearchFilter filter = new( isins, mic: "XMN");

            // Act
            LemonResults<Trade> results = await tradesRepo.GetLatestAsync ( filter );

            // Assert
            Assert.NotNull ( results );
            Assert.True(results.IsSuccess);
            Assert.Equal(200, results.HttpCode);
            Assert.Null(results.Exception);
            Assert.NotNull(results.Results);
            Assert.Equal(2, results.Results.Count);

            Trade trade = results.Results.Find ( t => t.Isin == "DE123456" );
            Assert.NotNull(trade);
            Assert.Equal("XMN", trade.Mic);

            trade = results.Results.Find ( t => t.Isin == "DE123458" );
            Assert.NotNull(trade);
            Assert.Equal("XMN", trade.Mic);
        }

        private Task<FakeApiResponse> ApiClient_GetLatest_ShouldReturn2Trades_WhenAskForTradesWith2IsinAndOneMic ( FakeApiRequest request )
        {
            Regex regex = new Regex ( "(isin=(?<isin>[A-Z0-9,]+))|(mic=(?<mic>[A-Z]+))" );

            Assert.NotNull ( request.Params );
            Assert.Equal("trades/latest", request.Params[0]);

            string httpParmas = request.Params[1].ToString ();
            Assert.NotNull(httpParmas);

            MatchCollection matchCollection = regex.Matches ( httpParmas );

            IEnumerable<string> isin = new string[0];
            string mic = null;
            foreach ( Match match in matchCollection )
            {
                if ( match.Groups.ContainsKey ( "isin" ) && match.Groups["isin"].Success ) isin = match.Groups["isin"].Value.Split(",");
                if ( match.Groups.ContainsKey ( "mic" ) && match.Groups["mic"].Success ) mic = match.Groups["mic"].Value;
            }

            List<Trade> trades = this.trades.Where ( t => isin.Contains ( t.Isin ) && t.Mic == mic ).ToList();

            FakeApiResponse response = new FakeApiResponse (HttpStatusCode.OK, new LemonResults<Trade>() { Results = trades, Status = "ok"});

            return Task.FromResult ( response );
        }

        #endregion GetLatest_ShouldReturn2Trades_WhenAskForTradesWith2IsinAndOneMic

        #endregion methods

    }
}
