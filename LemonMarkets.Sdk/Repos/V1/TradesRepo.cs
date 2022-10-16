using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LemonMarkets.Models;
using LemonMarkets.Models.Enums;
using LemonMarkets.Models.Responses;
using ApiService;
using System;
using LemonMarkets.Interfaces;

namespace LemonMarkets.Repos.V1
{

    public class TradesRepo : ITradesRepo
    {

        #region vars

        private readonly IApiClient marketApi;

        #endregion vars

        #region ctor

        public TradesRepo(IApiClient marketApi)
        {
            this.marketApi = marketApi;
        }

        #endregion ctor

        #region methods

        public Task<LemonResults<Trade>> GetLatestAsync ( TradesLatestSearchFilter request )
        {
            List<string> param = new List<string>();

            param.Add($"isin={string.Join(',', request.Isins)}");
            param.Add("decimals=false");
            if (request.Mic != null) param.Add($"mic={request.Mic}");
            if (request.Sorting != Sorting.None) param.Add($"sorting={request.Sorting}");

            StringBuilder buildParams = new ();
            buildParams.Append("?");
            buildParams.AppendJoin("&", param);

            return this.GetAsync("trades/latest", buildParams);
        }

        private async Task<LemonResults<Trade>> GetAsync(params object[] header)
        {
            LemonResultsInternal<Trade> result = (await this.marketApi.GetAsync<LemonResultsInternal<Trade>> (header))!;

            return new LemonResults<Trade>(result, new PageLoader<Trade>(this.marketApi));
        }

        #endregion methods

    }

}