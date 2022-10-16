using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LemonMarkets.Models;
using LemonMarkets.Models.Enums;
using LemonMarkets.Models.Responses;
using ApiService;
using LemonMarkets.Interfaces;
using System;

namespace LemonMarkets.Repos.V1
{

    public class PageLoader<T>
    {

        #region vars

        private readonly IApiClient api;

        #endregion vars

        #region ctor

        public PageLoader(IApiClient api)
        {
            this.api = api;
        }

        #endregion ctor

        #region methods

        public async Task<LemonResults<T>> GetAsync (string url)
        {
            LemonResultsInternal<T> result = (await this.api.GetAsync<LemonResultsInternal<T>>(url))!;

            return new LemonResults<T>(result, this);
        }

        #endregion methods

    }

}