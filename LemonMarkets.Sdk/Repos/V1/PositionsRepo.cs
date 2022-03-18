using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using lemon.LemonMarkets.Interfaces;
using LemonMarkets.Models;
using LemonMarkets.Models.Enums;
using LemonMarkets.Models.Requests.Trading;
using LemonMarkets.Models.Responses;
using ApiService;
using LemonMarkets.Models.Filters;

namespace LemonMarkets.Repos.V1
{

    public class PositionsRepo : IPortfolioRepo, IPositionsRepo
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

        public Task<LemonResults<PortfolioEntry>> GetAsync ( RequestGetPortfolio? request = null )
        {
            if ( request == null ) return this.tradingApi.GetAsync<LemonResults<PortfolioEntry>> ("positions")!;

            List<string> param = new List<string>();

            if (request.Isins.Count != 0) param.Add($"isin={string.Join(',', request.Isins)}");

            if (param.Count == 0) return this.tradingApi.GetAsync<LemonResults<PortfolioEntry>> ("positions")!;

            StringBuilder buildParams = new ();
            buildParams.Append("?");
            buildParams.AppendJoin("&", param);

            return this.tradingApi.GetAsync<LemonResults<PortfolioEntry>> ("positions", buildParams)!;
        }

        public Task<LemonResults<PositionEntry>> GetAsync(PositionSearchFilter? filter = null)
        {
            if ( filter is null ) return this.tradingApi.GetAsync<LemonResults<PositionEntry>> ("positions")!;

            List<string> param = new List<string>();

            if (filter.Isins.Count != 0) param.Add($"isin={string.Join(',', filter.Isins)}");

            if (param.Count == 0) return this.tradingApi.GetAsync<LemonResults<PositionEntry>> ("positions")!;

            StringBuilder buildParams = new ();
            buildParams.Append("?");
            buildParams.AppendJoin("&", param);

            return this.tradingApi.GetAsync<LemonResults<PositionEntry>> ("positions", buildParams)!;
        }

        #endregion methods

    }

}