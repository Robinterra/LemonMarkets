using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using LemonMarkets.Models;
using LemonMarkets.Models.Responses;
using LemonMarkets.Repos.V1;

namespace LemonMarkets
{
    public class LemonResults<T> : LemonResult, IAsyncEnumerable<T>
    {
        private PageLoader<T> pageLoader;
        private string? next;
        private string? previous;

        #region get/set

        public override bool IsSuccess
        {
            get
            {
                if (this.Results is not null) return true;

                return base.IsSuccess;
            }
        }

        public List<T>? Results
        {
            get;
        }

        public bool HasPreviousPages
        {
            get
            {
                return this.previous is not null;
            }
        }

        public bool HasNextPages
        {
            get
            {
                return this.next is not null;
            }
        }

        #endregion get/set

        #region ctor

        public LemonResults(string status, List<T> results)
        {
            this.Status = status;
            this.Results = results;
            this.pageLoader = null!;
        }

        public LemonResults(LemonResultsInternal<T> resultInternal, PageLoader<T> pageLoader)
        {
            this.Error_code = resultInternal.Error_code;
            this.Error_message = resultInternal.Error_message;
            this.Exception = resultInternal.Exception;
            this.HttpCode = resultInternal.HttpCode;
            this.Mode = resultInternal.Mode;
            this.Results = resultInternal.Results;
            this.Status = resultInternal.Status;
            this.Time = resultInternal.Time;
            this.pageLoader = pageLoader;
            this.next = this.GetPath(resultInternal.Next);
            this.previous = this.GetPath(resultInternal.Previous);
        }

        #endregion ctor

        #region methods

        private string? GetPath(string? path)
        {
            if (path is null) return null;

            string apiIdentifier = "v1/";
            ReadOnlySpan<char> apiUrl = path.AsSpan();
            int location = apiUrl.IndexOf(apiIdentifier);
            if (location == -1) return null;

            apiUrl = apiUrl.Slice(location + apiIdentifier.Length);

            return new string(apiUrl.ToArray());
        }

        public Task<LemonResults<T>> NextPageAsync()
        {
            if (this.next is null) throw new System.Exception("Todo: specified exception or correct result return");

            return this.pageLoader.GetAsync(this.next);
        }

        public Task<LemonResults<T>> PreviousPage()
        {
            if (this.previous is null) throw new System.Exception("Todo: specified exception or correct result return");

            return this.pageLoader.GetAsync(this.previous);
        }

        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new PageEnumerable<T>(this);
        }

        #endregion methods
    }
}
