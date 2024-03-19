using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.Caching.Distributed;

namespace ProjectName.Infrastructure.CacheService
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _cache;
        public CacheService(IDistributedCache cache)
        {
            _cache = cache;
        }
        public async Task<T> GetAsync<T>(string key)
        {
            var objectString = await _cache.GetStringAsync(key);

            if (string.IsNullOrWhiteSpace(objectString))
            {
                Console.WriteLine($"Cache key {key} not found");
                return default;
            }

            Console.WriteLine($"Get key {key} from Cache");
            return JsonSerializer.Deserialize<T>(objectString);
        }

        public async Task SetAsync<T>(string key, T data)
        {
            var memoryCacheEntryOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(3600),
                SlidingExpiration = TimeSpan.FromSeconds(1200)
            };

            var objectString = JsonSerializer.Serialize(data);
            await _cache.SetStringAsync(key, objectString, memoryCacheEntryOptions);
            Console.WriteLine($"Set key {key} on Cache");
        }
    }
}