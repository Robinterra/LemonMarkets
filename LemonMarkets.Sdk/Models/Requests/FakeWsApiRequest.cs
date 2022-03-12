namespace WsApiCore
{

    public class FakeWsApiRequest
    {

        #region get/set

        public string BaseAdress
        {
            get;
        }

        public string ApiPath
        {
            get;
        }

        public string FullUrl
        {
            get;
        }

        public object[]? Params
        {
            get;
        }

        public object? FromBody
        {
            get;
        }

        #endregion get/set

        #region ctor

        public FakeWsApiRequest(string baseAdress, string apiPath, string fullUrl, object[]? param = null, object? body = null)
        {
            this.ApiPath = apiPath;
            this.BaseAdress = baseAdress;
            this.FullUrl = fullUrl;
            this.Params = param;
            this.FromBody = body;
        }

        #endregion ctor


    }

}