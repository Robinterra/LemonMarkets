using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using lemon.LemonMarkets.Interfaces;
using LemonMarkets.Models;
using LemonMarkets.Models.Enums;
using LemonMarkets.Models.Responses;
using ApiService;

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

        public Task<LemonResults<Trade>> GetAsync ( TradesSearchFilter request )
        {
            List<string> param = new List<string>();

            param.Add($"isin={string.Join(',', request.Isins)}");
            if (request.From != null) param.Add($"from={request.From}");
            if (request.To != null) param.Add($"to={request.To}");
            if (request.Mic != null) param.Add($"mic={request.Mic}");
            if (request.Sorting != Sorting.None) param.Add($"sorting={request.Sorting}");

            StringBuilder buildParams = new ();
            buildParams.Append("?");
            buildParams.AppendJoin("&", param);

            return this.marketApi.GetAsync<LemonResults<Trade>> ("trades", buildParams)!;
        }

        #endregion methods

    }

}