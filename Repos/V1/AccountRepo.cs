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

    public class AccountRepo : IAccountRepo
    {

        #region vars

        private readonly WsAPICore marketApi;

        #endregion vars

        #region ctor

        public AccountRepo(WsAPICore marketApi)
        {
            this.marketApi = marketApi;
        }

        #endregion ctor

        #region methods

        public Task<LemonResult<Account>?> GetAsync (  )
        {
            return this.marketApi.GetAsync<LemonResult<Account>> ("account");
        }

        #endregion methods

    }

}