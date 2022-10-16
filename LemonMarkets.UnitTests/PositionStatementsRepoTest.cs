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
using LemonMarkets.Models.Enums;

namespace LemonMarkets.UnitTests
{
    public class PositionStatementsRepoTest
    {

        #region vars

        private List<Statement> statements;

        #endregion vars

        #region ctor

        public PositionStatementsRepoTest ()
        {
            this.statements = new List<Statement> ();
            this.statements.Add(new Statement() { Type = Models.Enums.PositionStatementType.Import });
            this.statements.Add(new Statement() { Type = Models.Enums.PositionStatementType.Order_buy });
            this.statements.Add(new Statement() { Type = Models.Enums.PositionStatementType.Order_sell });
            this.statements.Add(new Statement() { Type = Models.Enums.PositionStatementType.Snx });
            this.statements.Add(new Statement() { Type = Models.Enums.PositionStatementType.Split });
        }

        #endregion ctor

        #region methods

        #region Get_ShouldReturn1Statements_WhenAskForStatementWithAType

        [Fact]
        public async Task Get_ShouldReturn1Statements_WhenAskForStatementWithAType()
        {
            // Arrange
            IApiClient apiClient = new FakeApiClient(LemonApi.apiRealTradingBaseUrl, "v1", get: ApiClient_Get_Statements );
            IPositionStatementsRepo positionRepo = new PositionStatementsRepo(apiClient);

            StatementSearchFilter filter = new( type: PositionStatementType.Order_buy );

            // Act
            LemonResults<Statement> results = await positionRepo.GetAsync ( filter );

            // Assert
            Assert.NotNull ( results );
            Assert.True(results.IsSuccess);
            Assert.Equal(200, results.HttpCode);
            Assert.Null(results.Exception);
            Assert.NotNull(results.Results);
            Assert.Single(results.Results);

            Statement statement = results.Results.Find ( t => t.Type == PositionStatementType.Order_buy );
            Assert.NotNull(statement);
        }

        #endregion Get_ShouldReturn1Statements_WhenAskForStatementWithAType

        #region Get_ShouldReturnAllStatements_WhenAskForAllStatement

        [Fact]
        public async Task Get_ShouldReturnAllStatements_WhenAskForAllStatement()
        {
            // Arrange
            IApiClient apiClient = new FakeApiClient(LemonApi.apiRealTradingBaseUrl, "v1", get: ApiClient_Get_Statements );
            IPositionStatementsRepo positionRepo = new PositionStatementsRepo(apiClient);

            // Act
            LemonResults<Statement> results = await positionRepo.GetAsync (  );

            // Assert
            Assert.NotNull ( results );
            Assert.True(results.IsSuccess);
            Assert.Equal(200, results.HttpCode);
            Assert.Null(results.Exception);
            Assert.NotNull(results.Results);
            Assert.NotEmpty(results.Results);

            Assert.Equal(this.statements.Count, results.Results.Count);
        }

        #endregion Get_ShouldReturnAllStatements_WhenAskForAllStatement

        private Task<FakeApiResponse> ApiClient_Get_Statements(FakeApiRequest request)
        {
            Regex regex = new Regex ( "(type=(?<type>[a-z_0-9,]+))" );

            Assert.NotNull ( request.Params );
            Assert.Equal("positions/statements", request.Params[0]);

            string httpParmas = request.Params.Length > 1 ? request.Params[1].ToString () : string.Empty;
            Assert.NotNull(httpParmas);

            MatchCollection matchCollection = regex.Matches ( httpParmas );

            PositionStatementType type = PositionStatementType.All;
            foreach ( Match match in matchCollection )
            {
                if ( match.Groups.ContainsKey ( "type" ) && match.Groups["type"].Success ) type = Enum.Parse<PositionStatementType>(match.Groups["type"].Value, true);
            }

            List<Statement> statements = this.statements.Where ( t => type == PositionStatementType.All || type == t.Type ).ToList();

            FakeApiResponse response = new FakeApiResponse (HttpStatusCode.OK, new LemonResults<Statement>("ok", statements));

            return Task.FromResult ( response );
        }

        #endregion methods

    }
}
