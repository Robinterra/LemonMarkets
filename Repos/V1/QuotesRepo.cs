using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using lemon.LemonMarkets.Interfaces;
using LemonMarkets.Models;
using LemonMarkets.Models.Enums;
using LemonMarkets.Models.Responses;
using WsApiCore;

namespace LemonMarkets.Repos.V1
{

    public class QuotesRepo : IQuotesRepo
    {

        #region vars

        private readonly WsAPICore marketApi;

        #endregion vars

        #region ctor

        public QuotesRepo(WsAPICore marketApi)
        {
            this.marketApi = marketApi;
        }

        #endregion ctor

        #region methods

        public Task<LemonResults<Quote>?> GetAsync ( QuoteSearchFilter request )
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

            return this.marketApi.GetAsync<LemonResults<Quote>> ("quotes", buildParams);
        }

        #endregion methods

    }

}