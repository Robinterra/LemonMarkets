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

        [Obsolete("Please use GetLatestAsync, diese Methode wird noch bis zum 2022-04-01 funktionieren")]
        public Task<LemonResults<Trade>> GetAsync ( TradesSearchFilter request )
        {
            List<string> param = new List<string>();

            param.Add($"isin={string.Join(',', request.Isins)}");
            if (request.From != null) param.Add($"from={((DateTime)request.From).ToString("yyyy-MM-ddTHH:mm:ss")}");
            if (request.To != null) param.Add($"to={((DateTime)request.To).ToString("yyyy-MM-ddTHH:mm:ss")}");
            if (request.Mic != null) param.Add($"mic={request.Mic}");
            if (request.Sorting != Sorting.None) param.Add($"sorting={request.Sorting}");

            StringBuilder buildParams = new ();
            buildParams.Append("?");
            buildParams.AppendJoin("&", param);

            return this.marketApi.GetAsync<LemonResults<Trade>> ("trades", buildParams)!;
        }

        public Task<LemonResults<Trade>> GetLatestAsync ( TradesLatestSearchFilter request )
        {
            List<string> param = new List<string>();

            param.Add($"isin={string.Join(',', request.Isins)}");
            if (request.Mic != null) param.Add($"mic={request.Mic}");
            if (request.Sorting != Sorting.None) param.Add($"sorting={request.Sorting}");

            StringBuilder buildParams = new ();
            buildParams.Append("?");
            buildParams.AppendJoin("&", param);

            return this.marketApi.GetAsync<LemonResults<Trade>> ("trades/latest", buildParams)!;
        }

        #endregion methods

    }

}