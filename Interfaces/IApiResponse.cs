namespace lemon.LemonMarkets.Interfaces
{

    public interface IApiResponse
    {

        #region methods

        string? Status
        {
            get;
            set;
        }

        int HttpCode
        {
            get;
            set;
        }

        bool IsSuccess
        {
            get;
            set;
        }

        #endregion methods

        #region methods

        bool SetHttpCode(int httpCode);

        bool SetIsSuccess(bool isSuccess);

        bool SetStatus(string status);

        #endregion methods

    }

}