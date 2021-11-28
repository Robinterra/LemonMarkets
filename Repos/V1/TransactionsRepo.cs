using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using lemon.LemonMarkets.Interfaces;
using LemonMarkets.Models;
using LemonMarkets.Models.Requests.Trading;
using LemonMarkets.Models.Responses;
using WsApiCore;

namespace LemonMarkets.Repos.V1
{

    public class TransactionsRepo : ITransactionsRepo
    {

        #region vars

        private readonly WsAPICore tradingApi;

        #endregion vars

        #region ctor

        public TransactionsRepo ( WsAPICore tradingApi )
        {
            this.tradingApi = tradingApi;
        }
        
        #endregion ctor

        #region methods

        public Task<LemonResults<Transaction>?> GetAsync ( RequestGetTransactions? request = null )
        {
            if (request == null) return this.tradingApi.GetAsync<LemonResults<Transaction>> ("transactions");

            List<string> param = new List<string>();

            if (request.Isin != null) param.Add($"isin={request.Isin}");
            if (request.Space_id != null) param.Add($"space_id={request.Space_id}");
            if (request.To != null) param.Add($"to={request.To}");
            if (request.From != null) param.Add($"to={request.From}");

            if (param.Count == 0) return this.tradingApi.GetAsync<LemonResults<Transaction>> ("transactions");

            StringBuilder buildParams = new ();
            buildParams.Append("?");
            buildParams.AppendJoin("&", param);

            return this.tradingApi.GetAsync<LemonResults<Transaction>> ("transactions", buildParams);
        }

        public Task<LemonResult<Transaction>?> GetAsync ( string id )
        {
            return this.tradingApi.GetAsync<LemonResult<Transaction>> ("transactions", id);
        }

        #endregion methods

    }

}