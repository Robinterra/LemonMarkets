using System.Net;
using lemon.LemonMarkets.Interfaces;

namespace WsApiCore
{

    public class FakeWsAPI : IApiClient
    {

        #region vars

        public ApiDelegate? DeleteDelegate;

        public ApiDelegate? GetDelegate;

        public ApiDelegate? PostDelegate;

        public ApiDelegate? PutDelegate;

        private string baseAdress;

        private string apiPath;

        #endregion vars

        #region ctor

        public FakeWsAPI(string baseadress, string apiPath)
        {
            this.baseAdress = baseadress;
            this.apiPath = apiPath;
        }

        #endregion ctor

        #region delegates

        public delegate Task<FakeWsApiResponse> ApiDelegate(FakeWsApiRequest request);

        #endregion delegates

        #region methods

        private TResponse SetToResponse<TResponse>(TResponse result, bool isSuccess, HttpStatusCode statusCode)
        {
            if (result is not IApiResponse apiResponse) return result;

            apiResponse.SetHttpCode((int)statusCode);

            apiResponse.SetIsSuccess(isSuccess);

            return result;
        }

        public async Task<TResponse?> DeleteAsync<TResponse>(params object[] header) where TResponse : new()
        {
            if (this.DeleteDelegate is null) return default;

            string fullUrl = $"{baseAdress}{apiPath}/{string.Join('/', header)}";

            FakeWsApiResponse response = await this.DeleteDelegate(new (baseAdress, apiPath, fullUrl, header));
            if (response.FromBody is null) return default;
            if (response.FromBody is not TResponse tr) throw new Exception("response Body is not expectet TResponse");

            return this.SetToResponse(tr, response.IsSuccessStatusCode, response.HttpCode);
        }

        public async Task<TResponse?> GetAsync<TResponse>(params object[] header) where TResponse : new()
        {
            if (this.GetDelegate is null) return default;

            string fullUrl = $"{baseAdress}{apiPath}/{string.Join('/', header)}";

            FakeWsApiResponse response = await this.GetDelegate(new (baseAdress, apiPath, fullUrl, header));
            if (response.FromBody is null) return default;
            if (response.FromBody is not TResponse tr) throw new Exception("response Body is not expectet TResponse");

            return this.SetToResponse(tr, response.IsSuccessStatusCode, response.HttpCode);
        }

        public async Task<TResponse?> PostAsync<TResponse, TRequest>(TRequest data, string route) where TResponse : new()
        {
            if (this.PostDelegate is null) return default;

            string fullUrl = $"{baseAdress}{apiPath}/{route}";

            FakeWsApiResponse response = await this.PostDelegate(new (baseAdress, apiPath, fullUrl, body: data));
            if (response.FromBody is null) return default;
            if (response.FromBody is not TResponse tr) throw new Exception("response Body is not expectet TResponse");

            return this.SetToResponse(tr, response.IsSuccessStatusCode, response.HttpCode);
        }

        public async Task<TResponse?> PostAsync<TResponse>(string route) where TResponse : new()
        {
            if (this.PostDelegate is null) return default;

            string fullUrl = $"{baseAdress}{apiPath}/{route}";

            FakeWsApiResponse response = await this.PostDelegate(new (baseAdress, apiPath, fullUrl));
            if (response.FromBody is null) return default;
            if (response.FromBody is not TResponse tr) throw new Exception("response Body is not expectet TResponse");

            return this.SetToResponse(tr, response.IsSuccessStatusCode, response.HttpCode);
        }

        public async Task<TResponse?> PutAsync<TResponse, TRequest>(TRequest data, string route) where TResponse : new()
        {
            if (this.PutDelegate is null) return default;

            string fullUrl = $"{baseAdress}{apiPath}/{route}";

            FakeWsApiResponse response = await this.PutDelegate(new (baseAdress, apiPath, fullUrl, body: data));
            if (response.FromBody is null) return default;
            if (response.FromBody is not TResponse tr) throw new Exception("response Body is not expectet TResponse");

            return this.SetToResponse(tr, response.IsSuccessStatusCode, response.HttpCode);
        }

        #endregion methods

    }

}