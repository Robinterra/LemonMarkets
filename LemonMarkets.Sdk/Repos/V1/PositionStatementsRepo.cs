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

    public class PositionStatementsRepo : IPositionStatementsRepo
    {

        #region vars

        private readonly IApiClient tradingApi;

        #endregion vars

        #region ctor

        public PositionStatementsRepo ( IApiClient tradingApi )
        {
            this.tradingApi = tradingApi;
        }

        #endregion ctor

        #region methods

        public Task<LemonResults<Statement>> GetAsync(StatementSearchFilter? filter = null)
        {
            if ( filter is null ) return this.GetAsync("positions/statements");

            List<string> param = new List<string>();

            if (filter.Type != Models.Enums.PositionStatementType.All) param.Add($"type={filter.Type.ToString().ToLower()}");

            if (param.Count == 0) return this.GetAsync("positions/statements");

            StringBuilder buildParams = new ();
            buildParams.Append("?");
            buildParams.AppendJoin("&", param);

            return this.GetAsync("positions/statements", buildParams);
        }

        private async Task<LemonResults<Statement>> GetAsync(params object[] header)
        {
            LemonResultsInternal<Statement> result = (await this.tradingApi.GetAsync<LemonResultsInternal<Statement>> (header))!;

            return new LemonResults<Statement>(result, new PageLoader<Statement>(this.tradingApi));
        }

        #endregion methods

    }

}