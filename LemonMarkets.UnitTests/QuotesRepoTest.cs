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
    public class QuotesRepoTest
    {

        #region vars

        private List<Quote> quotes;

        #endregion vars

        #region ctor

        public QuotesRepoTest ()
        {
            this.quotes = new List<Quote> ();
            this.quotes.Add(new Quote () { Ask = 14, Bid = 11, AskVolume = 100, BidVolume = 200, Isin = "DE123456", Mic = "XMN", Time = DateTime.Parse("2022-03-13")});
            this.quotes.Add(new Quote () { Ask = 12, Bid = 13, AskVolume = 100, BidVolume = 100, Isin = "DE123457", Mic = "MUNICH", Time = DateTime.Parse("2022-02-25")});
            this.quotes.Add(new Quote () { Ask = 12, Bid = 14, AskVolume = 100, BidVolume = 100, Isin = "DE123456", Mic = "MUNICH", Time = DateTime.Parse("2022-02-10")});
            this.quotes.Add(new Quote () { Ask = 12, Bid = 15, AskVolume = 100, BidVolume = 100, Isin = "DE123457", Mic = "XMN", Time = DateTime.Parse("2022-02-08")});
            this.quotes.Add(new Quote () { Ask = 12, Bid = 16, AskVolume = 100, BidVolume = 100, Isin = "XMN", Mic = "DE123456", Time = DateTime.Parse("2022-03-10")});
            this.quotes.Add(new Quote () { Ask = 13, Bid = 11, AskVolume = 100, BidVolume = 200, Isin = "DE123458", Mic = "XMN", Time = DateTime.Parse("2022-03-12")});
        }

        #endregion ctor

        #region methods

        #region GetLatest_ShouldReturn2Quotes_WhenAskForQuotesWith2IsinAndOneMic

        [Fact]
        public async Task GetLatest_ShouldReturn2Quotes_WhenAskForQuotesWith2IsinAndOneMic()
        {
            // Arrange
            IApiClient apiClient = new FakeApiClient(LemonApi.apiDataBaseUrl, "v1", get: ApiClient_GetLatest_ShouldReturn2Quotes_WhenAskForQuotesWith2IsinAndOneMic );
            IQuotesRepo quotesRepo = new QuotesRepo(apiClient);

            List<string> isins = new List<string> {"DE123456", "DE123458"};
            QuoteLatestSearchFilter filter = new( isins, mic: "XMN");

            // Act
            LemonResults<Quote> results = await quotesRepo.GetLatestAsync ( filter );

            // Assert
            Assert.NotNull ( results );
            Assert.True(results.IsSuccess);
            Assert.Equal(200, results.HttpCode);
            Assert.Null(results.Exception);
            Assert.NotNull(results.Results);
            Assert.Equal(2, results.Results.Count);

            Quote quote = results.Results.Find ( t => t.Isin == "DE123456" );
            Assert.NotNull(quote);
            Assert.Equal("XMN", quote.Mic);

            quote = results.Results.Find ( t => t.Isin == "DE123458" );
            Assert.NotNull(quote);
            Assert.Equal("XMN", quote.Mic);
        }

        private Task<FakeApiResponse> ApiClient_GetLatest_ShouldReturn2Quotes_WhenAskForQuotesWith2IsinAndOneMic ( FakeApiRequest request )
        {
            Regex regex = new Regex ( "(isin=(?<isin>[A-Z0-9,]+))|(mic=(?<mic>[A-Z]+))" );

            Assert.NotNull ( request.Params );
            Assert.Equal("quotes/latest", request.Params[0]);

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

            List<Quote> quotes = this.quotes.Where ( t => isin.Contains ( t.Isin ) && t.Mic == mic ).ToList();

            FakeApiResponse response = new FakeApiResponse (HttpStatusCode.OK, new LemonResults<Quote>("ok", quotes));

            return Task.FromResult ( response );
        }

        #endregion GetLatest_ShouldReturn2Quotes_WhenAskForQuotesWith2IsinAndOneMic

        #region Get_ShouldReturn3Quotes_WhenAskForQuotesWith2IsinAndATimeRange

        [Fact]
        public async Task Get_ShouldReturn3Quotes_WhenAskForQuotesWith2IsinAndATimeRange()
        {
            // Arrange
            IApiClient apiClient = new FakeApiClient(LemonApi.apiDataBaseUrl, "v1", get: ApiClient_Get_ShouldReturn3Quotes_WhenAskForQuotesWith2IsinAndATimeRange );
            IQuotesRepo quotesRepo = new QuotesRepo(apiClient);

            List<string> isins = new List<string> {"DE123456", "DE123457"};
            QuoteLatestSearchFilter filter = new( isins );

            // Act
            LemonResults<Quote> results = await quotesRepo.GetLatestAsync ( filter );

            // Assert
            Assert.NotNull ( results );
            Assert.True(results.IsSuccess);
            Assert.Equal(200, results.HttpCode);
            Assert.Null(results.Exception);
            Assert.NotNull(results.Results);
            Assert.Equal(4, results.Results.Count);

            Quote quote = results.Results.Find ( t => t.Isin == "DE123456" );
            Assert.NotNull(quote);

            quote = results.Results.Find ( t => t.Isin == "DE123457" );
            Assert.NotNull(quote);

            quote = results.Results.LastOrDefault ( t => t.Isin == "DE123456" );
            Assert.NotNull(quote);
        }

        private Task<FakeApiResponse> ApiClient_Get_ShouldReturn3Quotes_WhenAskForQuotesWith2IsinAndATimeRange ( FakeApiRequest request )
        {
            Regex regex = new Regex ( "(isin=(?<isin>[A-Z0-9,]+))|(to=(?<to>[0-9-T.:]+))|(from=(?<from>[0-9-T.:]+))" );

            Assert.NotNull ( request.Params );
            Assert.Equal("quotes/latest", request.Params[0]);

            string httpParmas = request.Params[1].ToString ();
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

            List<Quote> quotes = this.quotes.Where ( t => isin.Contains ( t.Isin ) && t.Time <= to && t.Time >= from ).ToList();

            FakeApiResponse response = new FakeApiResponse (HttpStatusCode.OK, new LemonResults<Quote>("ok", quotes));

            return Task.FromResult ( response );
        }

        #endregion Get_ShouldReturn3Quotes_WhenAskForQuotesWith2IsinAndATimeRange

        #endregion methods

    }
}
