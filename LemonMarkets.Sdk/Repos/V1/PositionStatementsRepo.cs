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
            if ( filter is null ) return this.tradingApi.GetAsync<LemonResults<Statement>> ("positions/statements")!;

            List<string> param = new List<string>();

            if (filter.Type != Models.Enums.PositionStatementType.All) param.Add($"type={filter.Type.ToString().ToLower()}");

            if (param.Count == 0) return this.tradingApi.GetAsync<LemonResults<Statement>> ("positions/statements")!;

            StringBuilder buildParams = new ();
            buildParams.Append("?");
            buildParams.AppendJoin("&", param);

            return this.tradingApi.GetAsync<LemonResults<Statement>> ("positions/statements", buildParams)!;
        }

        #endregion methods

    }

}