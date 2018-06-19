using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using NuGet.Frameworks;

namespace WebCore.Services
{

    public interface IMemoryCacheService
    {
        string GetName();
    }


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
