using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LemonMarkets.Models
{
    public class PageEnumerable<T> : IAsyncEnumerator<T>
    {
        private LemonResults<T> result;
        private LemonResults<T> original;
        private int currentPosition = 0;

        #region get/set

        public T Current
        {
            get
            {
                return this.result.Results![currentPosition];
            }
        }

        #endregion get/set

        #region ctor

        public PageEnumerable(LemonResults<T> result)
        {
            this.result = result;
            this.original = result;
        }

        #endregion ctor

        #region methods

        public async ValueTask<bool> MoveNextAsync()
        {
            if (!this.result.IsSuccess) return false;
            if (this.result.Results is null) return false;

            this.currentPosition++;
            if (this.currentPosition < this.result.Results.Count) return true;
            if (!result.HasNextPages) return false;

            this.currentPosition = 0;
            this.result = await result.NextPageAsync();
            if (!this.result.IsSuccess) return false;
            if (this.result.Results is null) return false;

            return true;
        }

        public ValueTask DisposeAsync()
        {
            this.currentPosition = 0;
            this.result = original;

            return ValueTask.CompletedTask;
        }


        #endregion methods

    }
}