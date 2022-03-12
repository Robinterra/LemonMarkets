using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ApiService;
using lemon.LemonMarkets.Interfaces;
using LemonMarkets.Models;
using LemonMarkets.Models.Responses;
using Xunit;
using LemonMarkets.Repos.V1;

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
            this.quotes.Add(new Quote () { Ask = 12, Bid = 11, AskVolume = 100, BidVolume = 200, Isin = "DE123456", Mic = "XMN", Time = DateTime.Now});
            this.quotes.Add(new Quote () { Ask = 12, Bid = 13, AskVolume = 100, BidVolume = 100, Isin = "DE123457", Mic = "MUNICH", Time = DateTime.Now});
            this.quotes.Add(new Quote () { Ask = 12, Bid = 14, AskVolume = 100, BidVolume = 100, Isin = "DE123456", Mic = "MUNICH", Time = DateTime.Now});
            this.quotes.Add(new Quote () { Ask = 12, Bid = 15, AskVolume = 100, BidVolume = 100, Isin = "DE123457", Mic = "XMN", Time = DateTime.Now});
            this.quotes.Add(new Quote () { Ask = 12, Bid = 16, AskVolume = 100, BidVolume = 100, Isin = "XMN", Mic = "DE123456", Time = DateTime.Now});
            this.quotes.Add(new Quote () { Ask = 13, Bid = 11, AskVolume = 100, BidVolume = 200, Isin = "DE123458", Mic = "XMN", Time = DateTime.Now});
        }

        #endregion ctor

        #region methods

        #region Get_ShouldReturn2Quotes_WhenAskForQuotesWith2Isin

        [Fact]
        public async Task Get_ShouldReturn2Quotes_WhenAskForQuotesWith2Isin()
        {
            // Arrange
            IApiClient apiClient = new FakeApiClient("https://data.lemon.markets", "v1", get: ApiClient_Get_ShouldReturn2Quotes_WhenAskForQuotesWith2Isin );
            IQuotesRepo quotesRepo = new QuotesRepo(apiClient);

            QuoteSearchFilter filter = new(new List<string> {"DE123456", "DE123458"}, mic: "XMN");

            // Act
            LemonResults<Quote> results = await quotesRepo.GetAsync ( filter );

            // Assert
            Assert.NotNull ( results );
            Assert.True(results.IsSuccess);
            Assert.Equal(200, results.HttpCode);
            Assert.Null(results.Exception);
            Assert.NotNull(results.Results);
            Assert.Equal(2, results.Results.Count);

            Quote quote = results.Results.Find ( t => t.Isin == "DE123456" );
            Assert.NotNull(quote);
            Assert.Equal("DE123456", quote.Isin);
            Assert.Equal("XMN", quote.Mic);
            Assert.Equal(12, quote.Ask);
            Assert.Equal(11, quote.Bid);
            Assert.Equal(100, quote.AskVolume);
            Assert.Equal(200, quote.BidVolume);

            quote = results.Results.Find ( t => t.Isin == "DE123458" );
            Assert.NotNull(quote);
            Assert.Equal("DE123458", quote.Isin);
            Assert.Equal("XMN", quote.Mic);
            Assert.Equal(13, quote.Ask);
            Assert.Equal(11, quote.Bid);
            Assert.Equal(100, quote.AskVolume);
            Assert.Equal(200, quote.BidVolume);
        }

        private Task<FakeApiResponse> ApiClient_Get_ShouldReturn2Quotes_WhenAskForQuotesWith2Isin ( FakeApiRequest request )
        {
            Regex regex = new Regex ( "(isin=(?<isin>[A-Z0-9,]+))|(mic=(?<mic>[A-Z]+))" );

            Assert.NotNull ( request.Params );
            Assert.Equal("quotes", request.Params[0]);

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

            FakeApiResponse response = new FakeApiResponse (HttpStatusCode.OK, new LemonResults<Quote>() { Results = quotes, Status = "ok"});

            return Task.FromResult ( response );
        }

        #endregion Get_ShouldReturnCorrectQuotes_WhenAskForAQuoteWithIsinAndMic

        #endregion methods

    }
}
