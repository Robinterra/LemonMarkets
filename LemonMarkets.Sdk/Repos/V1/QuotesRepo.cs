using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LemonMarkets.Interfaces;
using LemonMarkets.Models;
using LemonMarkets.Models.Enums;
using LemonMarkets.Models.Responses;
using ApiService;
using System;

namespace LemonMarkets.Repos.V1
{

    public class QuotesRepo : IQuotesRepo
    {

        #region vars

        private readonly IApiClient marketApi;

        #endregion vars

        #region ctor

        public QuotesRepo(IApiClient marketApi)
        {
            this.marketApi = marketApi;
        }

        #endregion ctor

        #region methods

        public Task<LemonResults<Quote>> GetLatestAsync ( QuoteLatestSearchFilter request )
        {
            List<string> param = new List<string>();

            param.Add($"isin={string.Join(',', request.Isins)}");
            param.Add("decimals=false");
            if (request.Mic != null) param.Add($"mic={request.Mic}");
            if (request.Sorting != Sorting.None) param.Add($"sorting={request.Sorting}");

            StringBuilder buildParams = new ();
            buildParams.Append("?");
            buildParams.AppendJoin("&", param);

            return this.GetAsync("quotes/latest", buildParams);
        }

        private async Task<LemonResults<Quote>> GetAsync(params object[] header)
        {
            LemonResultsInternal<Quote> result = (await this.marketApi.GetAsync<LemonResultsInternal<Quote>> (header))!;

            return new LemonResults<Quote>(result, new PageLoader<Quote>(this.marketApi));
        }

        #endregion methods

    }

}