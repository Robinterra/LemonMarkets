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
    public class BankstatmentsRepoTest
    {

        #region vars

        private List<BankStatement> statements;

        #endregion vars

        #region ctor

        public BankstatmentsRepoTest ()
        {
            this.statements = new List<BankStatement> ();
            this.statements.Add(new BankStatement() { Isin = "DE123456", Type = BankstatementType.Order_buy, Created_at = DateTime.Parse("2022-02-09")});
            this.statements.Add(new BankStatement() { Isin = null, Type = BankstatementType.Pay_in, Created_at = DateTime.Parse("2022-02-10")});
            this.statements.Add(new BankStatement() { Isin = null, Type = BankstatementType.Pay_out, Created_at = DateTime.Parse("2022-02-20")});
            this.statements.Add(new BankStatement() { Isin = "DE123458", Type = BankstatementType.Dividend, Created_at = DateTime.Parse("2022-02-21")});
            this.statements.Add(new BankStatement() { Isin = "DE123459", Type = BankstatementType.Order_buy, Created_at = DateTime.Parse("2022-02-22") });
            this.statements.Add(new BankStatement() { Isin = "DE123454", Type = BankstatementType.Order_buy, Created_at = DateTime.Parse("2022-02-10") });
        }

        #endregion ctor

        #region methods

        private Task<FakeApiResponse> ApiClient_Get_Bankstatements(FakeApiRequest request)
        {
            Regex regex = new Regex ( "(type=(?<type>[a-zA-Z_]+))|(isin=(?<isin>[A-Z0-9,]+))|(to=(?<to>[0-9-T.:]+))|(from=(?<from>[0-9-T.:]+))" );

            Assert.NotNull ( request.Params );
            Assert.Equal("account/bankstatements", request.Params[0]);

            string httpParmas = request.Params[1].ToString ();
            Assert.NotNull(httpParmas);

            MatchCollection matchCollection = regex.Matches ( httpParmas );

            IEnumerable<string> isin = new string[0];
            DateTime to = DateTime.MaxValue;
            DateTime from = DateTime.MinValue;
            BankstatementType type = BankstatementType.None;
            foreach ( Match match in matchCollection )
            {
                if ( match.Groups.ContainsKey ( "isin" ) && match.Groups["isin"].Success ) isin = match.Groups["isin"].Value.Split(",");
                if ( match.Groups.ContainsKey ( "to" ) && match.Groups["to"].Success ) to = DateTime.Parse(match.Groups["to"].Value);
                if ( match.Groups.ContainsKey ( "from" ) && match.Groups["from"].Success ) from = DateTime.Parse(match.Groups["from"].Value);
                if ( match.Groups.ContainsKey ( "type" ) && match.Groups["type"].Success ) type = Enum.Parse<BankstatementType>(match.Groups["type"].Value, true);
            }

            List<BankStatement> statements = this.statements.Where ( t => (isin.Any() ? isin.Contains ( t.Isin ) : true) && t.Created_at <= to && t.Created_at >= from && (type == BankstatementType.None ? true : type == t.Type) ).ToList();

            FakeApiResponse response = new FakeApiResponse (HttpStatusCode.OK, new LemonResults<BankStatement>("ok", statements));

            return Task.FromResult(response);
        }

        #region Get_ShouldReturn2Bankstatements_WhenAskForBankstatementInTypeOrderBuyAndInATimeRange

        [Fact]
        public async Task Get_ShouldReturn2Bankstatements_WhenAskForBankstatementInTypeOrderBuyAndInATimeRange()
        {
            // Arrange
            IApiClient apiClient = new FakeApiClient(LemonApi.apiRealTradingBaseUrl, "v1", get: ApiClient_Get_Bankstatements );
            IBankstatementsRepo bankstatmentRepo = new BankstatementsRepo(apiClient);

            DateTime from = DateTime.Parse("2022-02-01");
            DateTime to = DateTime.Parse("2022-02-20");
            BankStatementsFilter filter = new( type: BankstatementType.Order_buy, from: from, to: to);

            // Act
            LemonResults<BankStatement> results = await bankstatmentRepo.GetAsync ( filter );

            // Assert
            Assert.NotNull ( results );
            Assert.True(results.IsSuccess);
            Assert.Equal(200, results.HttpCode);
            Assert.Null(results.Exception);
            Assert.NotNull(results.Results);
            Assert.Equal(2, results.Results.Count);

            BankStatement statement = results.Results.Find ( t => t.Isin == "DE123456" );
            Assert.NotNull(statement);

            statement = results.Results.Find ( t => t.Isin == "DE123454" );
            Assert.NotNull(statement);
        }

        #endregion Get_ShouldReturn2Bankstatements_WhenAskForBankstatementInTypeOrderBuyAndInATimeRange

        #endregion methods

    }
}
