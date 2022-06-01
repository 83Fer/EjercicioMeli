using Models;

namespace Services
{
    public interface ICacheService
    {
        Task<string> GetCacheValueAsync(string key);

        Task SetCacheValueAsync(string key, string value);
        
        Task<Dictionary<string, IPInformation>> GetAllValuesCacheAsync();

    }
}