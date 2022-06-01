using Models;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RedisCacheService : ICacheService
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public RedisCacheService(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
        }

        public async Task<string> GetCacheValueAsync(string key)
        {
            var redisDB = _connectionMultiplexer.GetDatabase();
            return await redisDB.StringGetAsync(key);
        }

        public async Task SetCacheValueAsync(string key, string value)
        {
            var redisDB = _connectionMultiplexer.GetDatabase();
            await redisDB.StringSetAsync(key, value);
        }

        
        public async Task<Dictionary<string, IPInformation>> GetAllValuesCacheAsync()
        {
            var keys = GetAllKeysCacheAsync();
            var values = new Dictionary<string, IPInformation>();
            foreach (var key in keys)
            {
                values.Add(key, JsonConvert.DeserializeObject<IPInformation>(await GetCacheValueAsync(key)));
            }

            return values;
        }

        private IEnumerable<string> GetAllKeysCacheAsync()
        {
            var endpoints = _connectionMultiplexer.GetEndPoints();
            var server = _connectionMultiplexer.GetServer(endpoints[0]);
            var keys = server.Keys();

            return keys.Select(x => x.ToString());
        }
    }
}
