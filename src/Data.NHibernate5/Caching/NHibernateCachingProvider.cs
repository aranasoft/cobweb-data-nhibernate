using System.Linq;
using NHibernate.Linq;

namespace Cobweb.Data.NHibernate.Caching {
    public class NHibernateCachingProvider : ICachingProvider {
        public ICacheRequest<T> Cacheable<T>(
            IQueryable<T> source) {
            var cachedQuery = source.WithOptions(opt => opt.SetCacheable(true));
            return new NHibernateCacheRequest<T>(cachedQuery);
        }
    }
}
