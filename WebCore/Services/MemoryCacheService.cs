using Microsoft.Extensions.Caching.Memory;

namespace WebCore.Services
{
    public class MemoryCacheService : IMemoryCacheService
    {
        private readonly IMemoryCache MemoryCache;

        public MemoryCacheService(IMemoryCache cache)
        {
            this.MemoryCache = cache;
        }

        public string GetName()
        {
            if (MemoryCache.TryGetValue("Name", out string nome))
            {
                return nome;
            }

            MemoryCache.Set("Name", "Cristiano Motta");
            return MemoryCache.Get<string>("Name");
        }
    }
}
