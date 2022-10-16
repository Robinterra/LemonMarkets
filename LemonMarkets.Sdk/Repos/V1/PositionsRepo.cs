using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using LemonMarkets.Models;
using LemonMarkets.Models.Responses;

using ApiService;

using LemonMarkets.Models.Filters;
using LemonMarkets.Interfaces;

namespace LemonMarkets.Repos.V1
{

    public class PositionsRepo : IPositionsRepo
    {

        #region vars

        private readonly IApiClient tradingApi;

        #endregion vars

        #region ctor

        public PositionsRepo ( IApiClient tradingApi )
        {
            this.tradingApi = tradingApi;
        }

        #endregion ctor

        #region methods

        public Task<LemonResults<PositionEntry>> GetAsync(PositionSearchFilter? filter = null)
        {
            if ( filter is null ) return this.GetAsync("positions");

            List<string> param = new List<string>();

            if (filter.Isins.Count != 0) param.Add($"isin={string.Join(',', filter.Isins)}");

            if (param.Count == 0) return this.GetAsync("positions");

            StringBuilder buildParams = new ();
            buildParams.Append("?");
            buildParams.AppendJoin("&", param);

            return this.GetAsync("positions", buildParams);
        }

        private async Task<LemonResults<PositionEntry>> GetAsync(params object[] header)
        {
            LemonResultsInternal<PositionEntry> result = (await this.tradingApi.GetAsync<LemonResultsInternal<PositionEntry>> (header))!;

            return new LemonResults<PositionEntry>(result, new PageLoader<PositionEntry>(this.tradingApi));
        }

        #endregion methods

    }

}