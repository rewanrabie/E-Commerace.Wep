using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface ICacheServices
    {
        Task<string?> GetAsync(string CacheKey);

        Task SetAsync(string CacheKey, object CacheValue, TimeSpan TimeToLive);
    }
}
