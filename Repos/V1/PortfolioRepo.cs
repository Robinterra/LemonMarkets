using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using lemon.LemonMarkets.Interfaces;
using LemonMarkets.Models;
using LemonMarkets.Models.Enums;
using LemonMarkets.Models.Requests.Trading;
using LemonMarkets.Models.Responses;
using WsApiCore;

namespace LemonMarkets.Repos.V1
{

    public class PortfolioRepo : IPortfolioRepo
    {

        #region vars

        private readonly IApiClient tradingApi;

        #endregion vars

        #region ctor

        public PortfolioRepo ( IApiClient tradingApi )
        {
            this.tradingApi = tradingApi;
        }

        #endregion ctor

        #region methods

        public Task<LemonResults<PortfolioEntry>?> GetAsync ( RequestGetPortfolio? request = null )
        {
            if ( request == null ) return this.tradingApi.GetAsync<LemonResults<PortfolioEntry>> ("portfolio");

            List<string> param = new List<string>();

            if (request.Isins.Count != 0) param.Add($"isin={string.Join(',', request.Isins)}");
            if (request.Space_id != null) param.Add($"space_id={request.Space_id}");

            if (param.Count == 0) return this.tradingApi.GetAsync<LemonResults<PortfolioEntry>> ("portfolio");

            StringBuilder buildParams = new ();
            buildParams.Append("?");
            buildParams.AppendJoin("&", param);

            return this.tradingApi.GetAsync<LemonResults<PortfolioEntry>> ("portfolio", buildParams);
        }

        #endregion methods

    }

}