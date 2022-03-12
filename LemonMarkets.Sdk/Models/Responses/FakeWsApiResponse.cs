using System.Net;

namespace WsApiCore
{

    public class FakeWsApiResponse
    {

        #region get/set

        public HttpStatusCode HttpCode
        {
            get;
        }

        public object? FromBody
        {
            get;
        }

        public bool IsSuccessStatusCode
        {
            get { return ((int)this.HttpCode >= 200) && ((int)this.HttpCode <= 299); }
        }

        #endregion get/set

        #region ctor

        public FakeWsApiResponse(HttpStatusCode httpCode = HttpStatusCode.OK, object? body = null)
        {
            this.HttpCode = httpCode;
            this.FromBody = body;
        }

        #endregion ctor

    }

}