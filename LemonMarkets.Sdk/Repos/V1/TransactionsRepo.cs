using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using lemon.LemonMarkets.Interfaces;
using LemonMarkets.Models;
using LemonMarkets.Models.Enums;
using LemonMarkets.Models.Requests.Trading;
using LemonMarkets.Models.Responses;
using ApiService;
using System;

namespace LemonMarkets.Repos.V1
{

    public class BankstatementsRepo : IBankstatementsRepo
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

        public Task<LemonResults<BankStatement>> GetAsync(BankStatementsFilter? request = null)
        {
            if (request == null) return this.tradingApi.GetAsync<LemonResults<BankStatement>> ("account/bankstatements")!;

            List<string> param = new List<string>();

            if (request.Isin != null) param.Add($"isin={request.Isin}");
            if (request.To != null) param.Add($"to={((DateTime)request.To).ToString("yyyy-MM-ddTHH:mm:ss")}");
            if (request.From != null) param.Add($"from={((DateTime)request.From).ToString("yyyy-MM-ddTHH:mm:ss")}");
            if (request.Type != BankstatementType.None) param.Add($"type={request.Type.ToString()}");
            if (request.Sorting != Sorting.None) param.Add($"sorting={request.Sorting}");

            if (param.Count == 0) return this.tradingApi.GetAsync<LemonResults<BankStatement>> ("account/bankstatements")!;

            StringBuilder buildParams = new ();
            buildParams.Append("?");
            buildParams.AppendJoin("&", param);

            return this.tradingApi.GetAsync<LemonResults<BankStatement>> ("account/bankstatements", buildParams)!;
        }

        #endregion methods

    }

}