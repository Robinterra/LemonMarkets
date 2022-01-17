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

    public class TransactionsRepo : ITransactionsRepo
    {

        #region vars

        private readonly IApiClient tradingApi;

        #endregion vars

        #region ctor

        public TransactionsRepo ( IApiClient tradingApi )
        {
            this.tradingApi = tradingApi;
        }
        
        #endregion ctor

        #region methods

        public Task<LemonResults<Transaction>?> GetAsync ( RequestGetTransactions? request = null )
        {
            if (request == null) return this.tradingApi.GetAsync<LemonResults<Transaction>> ("account/bankstatements");

            List<string> param = new List<string>();

            if (request.Isin != null) param.Add($"isin={request.Isin}");
            if (request.To != null) param.Add($"to={request.To}");
            if (request.From != null) param.Add($"from={request.From}");
            if (request.Type != TransactionType.None) param.Add($"type={request.Type.ToString()}");

            if (param.Count == 0) return this.tradingApi.GetAsync<LemonResults<Transaction>> ("account/bankstatements");

            StringBuilder buildParams = new ();
            buildParams.Append("?");
            buildParams.AppendJoin("&", param);

            return this.tradingApi.GetAsync<LemonResults<Transaction>> ("account/bankstatements", buildParams);
        }

        #endregion methods

    }

}