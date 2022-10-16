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
    public class OHLCRepoTest
    {

        #region vars

        private List<OHLCEntry> ohlcs;

        #endregion vars

        #region ctor

        public OHLCRepoTest ()
        {
            this.ohlcs = new List<OHLCEntry> ();
            this.ohlcs.Add(new OHLCEntry () { Isin = "DE123456", Mic = "XMN", Time = DateTime.Parse("2022-03-13")});
            this.ohlcs.Add(new OHLCEntry () { Isin = "DE123457", Mic = "MUNICH", Time = DateTime.Parse("2022-02-25")});
            this.ohlcs.Add(new OHLCEntry () { Isin = "DE123456", Mic = "MUNICH", Time = DateTime.Parse("2022-02-10")});
            this.ohlcs.Add(new OHLCEntry () { Isin = "DE123457", Mic = "XMN", Time = DateTime.Parse("2022-02-08")});
            this.ohlcs.Add(new OHLCEntry () { Isin = "XMN", Mic = "DE123456", Time = DateTime.Parse("2022-03-10")});
            this.ohlcs.Add(new OHLCEntry () { Isin = "DE123458", Mic = "XMN", Time = DateTime.Parse("2022-03-12")});
        }

        #endregion ctor

        #region methods

        #region Get_ShouldReturn2OHLC_WhenAskForOHLCWith2IsinAndOneMic

        [Fact]
        public async Task Get_ShouldReturn2OHLC_WhenAskForOHLCWith2IsinAndOneMic()
        {
            // Arrange
            IApiClient apiClient = new FakeApiClient(LemonApi.apiDataBaseUrl, "v1", get: ApiClient_Get_ShouldReturn2OHLC_WhenAskForOHLCWith2IsinAndOneMic );
            IOpenHighLowCloseRepo ohlcRepo = new OpenHighLowCloseRepo(apiClient);

            List<string> isins = new List<string> {"DE123456", "DE123458"};
            OHLCSearchFilter filter = new( isins, Models.Enums.OHLCTimeMode.Minutely, mic: "XMN");

            // Act
            LemonResults<OHLCEntry> results = await ohlcRepo.GetAsync ( filter );

            // Assert
            Assert.NotNull ( results );
            Assert.True(results.IsSuccess);
            Assert.Equal(200, results.HttpCode);
            Assert.Null(results.Exception);
            Assert.NotNull(results.Results);
            Assert.Equal(2, results.Results.Count);

            OHLCEntry ohlcEntry = results.Results.Find ( t => t.Isin == "DE123456" );
            Assert.NotNull(ohlcEntry);
            Assert.Equal("XMN", ohlcEntry.Mic);

            ohlcEntry = results.Results.Find ( t => t.Isin == "DE123458" );
            Assert.NotNull(ohlcEntry);
            Assert.Equal("XMN", ohlcEntry.Mic);
        }

        private Task<FakeApiResponse> ApiClient_Get_ShouldReturn2OHLC_WhenAskForOHLCWith2IsinAndOneMic ( FakeApiRequest request )
        {
            Regex regex = new Regex ( "(isin=(?<isin>[A-Z0-9,]+))|(mic=(?<mic>[A-Z]+))" );

            Assert.NotNull ( request.Params );
            Assert.Equal("ohlc", request.Params[0]);
            Assert.Equal("m1", request.Params[1]);

            string httpParmas = request.Params[2].ToString ();
            Assert.NotNull(httpParmas);

            MatchCollection matchCollection = regex.Matches ( httpParmas );

            IEnumerable<string> isin = new string[0];
            string mic = null;
            foreach ( Match match in matchCollection )
            {
                if ( match.Groups.ContainsKey ( "isin" ) && match.Groups["isin"].Success ) isin = match.Groups["isin"].Value.Split(",");
                if ( match.Groups.ContainsKey ( "mic" ) && match.Groups["mic"].Success ) mic = match.Groups["mic"].Value;
            }

            List<OHLCEntry> ohlcs = this.ohlcs.Where ( t => isin.Contains ( t.Isin ) && t.Mic == mic ).ToList();

            FakeApiResponse response = new FakeApiResponse (HttpStatusCode.OK, new LemonResultsInternal<OHLCEntry>("ok", ohlcs));

            return Task.FromResult ( response );
        }

        #endregion Get_ShouldReturn2OHLC_WhenAskForOHLCWith2IsinAndOneMic

        #region Get_ShouldReturn3OHLC_WhenAskForOHLCWith2IsinAndATimeRange

        [Fact]
        public async Task Get_ShouldReturn3OHLC_WhenAskForOHLCWith2IsinAndATimeRange()
        {
            // Arrange
            IApiClient apiClient = new FakeApiClient(LemonApi.apiDataBaseUrl, "v1", get: ApiClient_Get_ShouldReturn3OHLC_WhenAskForOHLCWith2IsinAndATimeRange );
            IOpenHighLowCloseRepo ohlcRepo = new OpenHighLowCloseRepo(apiClient);

            List<string> isins = new List<string> {"DE123456", "DE123457"};
            DateTime to = DateTime.Parse("2022-03-13");
            DateTime from = DateTime.Parse("2022-02-10");
            OHLCSearchFilter filter = new( isins, Models.Enums.OHLCTimeMode.Daily, to: to, from: from);

            // Act
            LemonResults<OHLCEntry> results = await ohlcRepo.GetAsync ( filter );

            // Assert
            Assert.NotNull ( results );
            Assert.True(results.IsSuccess);
            Assert.Equal(200, results.HttpCode);
            Assert.Null(results.Exception);
            Assert.NotNull(results.Results);
            Assert.Equal(3, results.Results.Count);

            OHLCEntry ohlc = results.Results.Find ( t => t.Isin == "DE123456" );
            Assert.NotNull(ohlc);
            Assert.Equal(to, ohlc.Time);

            ohlc = results.Results.Find ( t => t.Isin == "DE123457" );
            Assert.NotNull(ohlc);
            Assert.Equal(DateTime.Parse("2022-02-25"), ohlc.Time);

            ohlc = results.Results.LastOrDefault ( t => t.Isin == "DE123456" );
            Assert.NotNull(ohlc);
            Assert.Equal(from, ohlc.Time);
        }

        private Task<FakeApiResponse> ApiClient_Get_ShouldReturn3OHLC_WhenAskForOHLCWith2IsinAndATimeRange ( FakeApiRequest request )
        {
            Regex regex = new Regex ( "(isin=(?<isin>[A-Z0-9,]+))|(to=(?<to>[0-9-T.:]+))|(from=(?<from>[0-9-T.:]+))" );

            Assert.NotNull ( request.Params );
            Assert.Equal("ohlc", request.Params[0]);
            Assert.Equal("d1", request.Params[1]);

            string httpParmas = request.Params[2].ToString ();
            Assert.NotNull(httpParmas);

            MatchCollection matchCollection = regex.Matches ( httpParmas );

            IEnumerable<string> isin = new string[0];
            DateTime to = DateTime.MaxValue;
            DateTime from = DateTime.MinValue;
            foreach ( Match match in matchCollection )
            {
                if ( match.Groups.ContainsKey ( "isin" ) && match.Groups["isin"].Success ) isin = match.Groups["isin"].Value.Split(",");
                if ( match.Groups.ContainsKey ( "to" ) && match.Groups["to"].Success ) to = DateTime.Parse(match.Groups["to"].Value);
                if ( match.Groups.ContainsKey ( "from" ) && match.Groups["from"].Success ) from = DateTime.Parse(match.Groups["from"].Value);
            }

            List<OHLCEntry> ohlcs = this.ohlcs.Where ( t => isin.Contains ( t.Isin ) && t.Time <= to && t.Time >= from ).ToList();

            FakeApiResponse response = new FakeApiResponse (HttpStatusCode.OK, new LemonResultsInternal<OHLCEntry>("ok", ohlcs));

            return Task.FromResult ( response );
        }

        #endregion Get_ShouldReturn3OHLC_WhenAskForOHLCWith2IsinAndATimeRange

        #endregion methods

    }
}
