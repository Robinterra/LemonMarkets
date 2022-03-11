using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using lemon.LemonMarkets.Interfaces;
using LemonMarkets.Models;
using LemonMarkets.Models.Enums;
using LemonMarkets.Models.Requests.Trading;
using LemonMarkets.Models.Responses;
using ApiService;

namespace LemonMarkets.Repos.V1
{

    public class BankstatementsRepo : ITransactionsRepo, IBankstatementsRepo
    {

        #region vars

        private readonly IApiClient tradingApi;

        #endregion vars

        #region ctor

        public BankstatementsRepo ( IApiClient tradingApi )
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

        public Task<LemonResults<BankStatement>?> GetAsync(BankStatementsFilter? request = null)
        {
            if (request == null) return this.tradingApi.GetAsync<LemonResults<BankStatement>> ("account/bankstatements");

            List<string> param = new List<string>();

            if (request.Isin != null) param.Add($"isin={request.Isin}");
            if (request.To != null) param.Add($"to={request.To}");
            if (request.From != null) param.Add($"from={request.From}");
            if (request.Type != BankstatementType.None) param.Add($"type={request.Type.ToString()}");
            if (request.Sorting != Sorting.None) param.Add($"sorting={request.Sorting}");

            if (param.Count == 0) return this.tradingApi.GetAsync<LemonResults<BankStatement>> ("account/bankstatements");

            StringBuilder buildParams = new ();
            buildParams.Append("?");
            buildParams.AppendJoin("&", param);

            return this.tradingApi.GetAsync<LemonResults<BankStatement>> ("account/bankstatements", buildParams);
        }

        #endregion methods

    }

}