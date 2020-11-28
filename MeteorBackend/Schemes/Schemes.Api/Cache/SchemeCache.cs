using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Schemes.Domain.Results;

namespace Schemes.Api
{
    public class SchemeCache
    {
        private readonly IDistributedCache cache;
        private readonly DistributedCacheEntryOptions coShort;
        private readonly DistributedCacheEntryOptions coLong;

        private readonly int maxLimit = Constants.MaxLimit;

        public SchemeCache(IDistributedCache cache) {
            this.cache = cache;

            coShort = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(1));
            coLong = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
        }

        #region single
        public async Task<SchemeResult> TryGetSingle(int todoItemId) {
            var valueString = await cache.GetStringAsync(getSingleCacheKey(todoItemId.ToString()));
            if (valueString == null)
                return null;
            else
                return JsonSerializer.Deserialize<SchemeResult>(valueString);
        }

        public Task Invalidate(int schemeId) => cache.RemoveAsync(getSingleCacheKey(schemeId.ToString()));
                
        public async Task SetSingle(SchemeResult value) {
            var valueString = JsonSerializer.Serialize(value);
            await cache.SetStringAsync(key: getSingleCacheKey(value.Id.ToString()), value: valueString, options: coShort);
        }
        #endregion

        #region lists
        public async Task SetLatestList(List<SchemeResult> list) {
            var valueString = JsonSerializer.Serialize(list);
            await cache.SetStringAsync(key: getListCacheKey(list.Count), value: valueString, options: coLong);
        }

        public async Task<List<SchemeResult>> TryGetLatestList(int limit) {
            var valueString = await cache.GetStringAsync(getListCacheKey(limit));
            if (valueString == null)
                return null;
            else
                return JsonSerializer.Deserialize<List<SchemeResult>>(valueString);
        }

        public async Task ShiftLists(SchemeResult newItem) {
            for (int i = maxLimit-1; i > 0; i--) {
                var list = await TryGetLatestList(i);
                if (list != null) {
                    list.Insert(0, newItem);
                    await SetLatestList(list);
                    if (i == Constants.DefaultLimit) {
                        list.RemoveAt(Constants.DefaultLimit);
                        await SetLatestList(list);
                    }
                    await cache.RemoveAsync(getListCacheKey(i));
                }
            }
        }

        public async Task InvalidateLists() {
            for (int i = 0; i < maxLimit; i++)
                await cache.RemoveAsync(getListCacheKey(i));
        }
        #endregion

        #region private functions
        private string getSingleCacheKey(string schemeId) => $"schemes-{schemeId}";

        private string getListCacheKey(int listLength) => $"schemes-l-{listLength}";
        #endregion

    }
}
