using System;
using System.Linq;
using Cobweb.Data.NHibernate.Caching;
using NHibernate.Linq;

namespace Cobweb.Data.NHibernate.Tests.Util {
    public class FakeCachingProvider : FakeQueryableOptionsProvider, ICachingProvider {
        public IQueryable<T> Cacheable<T>(IQueryable<T> query) {
            return query;
        }
    }
}
