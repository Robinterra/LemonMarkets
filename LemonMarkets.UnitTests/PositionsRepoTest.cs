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
using LemonMarkets.Models.Filters;

namespace LemonMarkets.UnitTests
{
    public class PositionsRepoTest
    {

        #region vars

        private List<PositionEntry> portfolio;

        #endregion vars

        #region ctor

        public PositionsRepoTest ()
        {
            this.portfolio = new List<PositionEntry> ();
            this.portfolio.Add(new PositionEntry() { Isin = "DE123456" });
            this.portfolio.Add(new PositionEntry() { Isin = "DE123457" });
            this.portfolio.Add(new PositionEntry() { Isin = "DE123458" });
            this.portfolio.Add(new PositionEntry() { Isin = "DE123458" });
            this.portfolio.Add(new PositionEntry() { Isin = "DE123459" });
            this.portfolio.Add(new PositionEntry() { Isin = "DE123454" });
        }

        #endregion ctor

        #region methods

        #region Get_ShouldReturn2Positions_WhenAskForPositionsWith2Isin

        [Fact]
        public async Task Get_ShouldReturn2Positions_WhenAskForPositionsWith2Isin()
        {
            // Arrange
            IApiClient apiClient = new FakeApiClient(LemonApi.apiRealTradingBaseUrl, "v1", get: ApiClient_Get_Positions );
            IPositionsRepo positionRepo = new PositionsRepo(apiClient);

            List<string> isins = new List<string> {"DE123456", "DE123457"};
            PositionSearchFilter filter = new( isins: isins );

            // Act
            LemonResults<PositionEntry> results = await positionRepo.GetAsync ( filter );

            // Assert
            Assert.NotNull ( results );
            Assert.True(results.IsSuccess);
            Assert.Equal(200, results.HttpCode);
            Assert.Null(results.Exception);
            Assert.NotNull(results.Results);
            Assert.Equal(2, results.Results.Count);

            PositionEntry position = results.Results.Find ( t => t.Isin == "DE123456" );
            Assert.NotNull(position);

            position = results.Results.Find ( t => t.Isin == "DE123457" );
            Assert.NotNull(position);
        }

        #endregion Get_ShouldReturn2Positions_WhenAskForPositionsWith2Isin

        #region Get_ShouldReturn1Positions_WhenAskForPositionsWith1Isin

        [Fact]
        public async Task Get_ShouldReturn1Positions_WhenAskForPositionsWith1Isin()
        {
            // Arrange
            IApiClient apiClient = new FakeApiClient(LemonApi.apiRealTradingBaseUrl, "v1", get: ApiClient_Get_Positions );
            IPositionsRepo positionRepo = new PositionsRepo(apiClient);

            PositionSearchFilter filter = new( "DE123459" );

            // Act
            LemonResults<PositionEntry> results = await positionRepo.GetAsync ( filter );

            // Assert
            Assert.NotNull ( results );
            Assert.True(results.IsSuccess);
            Assert.Equal(200, results.HttpCode);
            Assert.Null(results.Exception);
            Assert.NotNull(results.Results);
            Assert.Single(results.Results);

            PositionEntry position = results.Results.Find ( t => t.Isin == "DE123459" );
            Assert.NotNull(position);
        }

        #endregion Get_ShouldReturn1Positions_WhenAskForPositionsWith1Isin

        private Task<FakeApiResponse> ApiClient_Get_Positions(FakeApiRequest request)
        {
            Regex regex = new Regex ( "(isin=(?<isin>[A-Z0-9,]+))" );

            Assert.NotNull ( request.Params );
            Assert.Equal("positions", request.Params[0]);

            string httpParmas = request.Params[1].ToString ();
            Assert.NotNull(httpParmas);

            MatchCollection matchCollection = regex.Matches ( httpParmas );

            IEnumerable<string> isin = new string[0];
            foreach ( Match match in matchCollection )
            {
                if ( match.Groups.ContainsKey ( "isin" ) && match.Groups["isin"].Success ) isin = match.Groups["isin"].Value.Split(",");
            }

            List<PositionEntry> positions = this.portfolio.Where ( t => isin.Contains ( t.Isin ) ).ToList();

            FakeApiResponse response = new FakeApiResponse (HttpStatusCode.OK, new LemonResults<PositionEntry>("ok", positions));

            return Task.FromResult ( response );
        }

        #endregion methods

    }
}
